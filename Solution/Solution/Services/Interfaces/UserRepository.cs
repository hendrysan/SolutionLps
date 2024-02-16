using Microsoft.EntityFrameworkCore;
using System.Net;
using Solution.Contexts;
using Solution.Helpers;
using Solution.Models;
using Solution.Services.Repositories;

namespace Solution.Services.Interfaces
{
    public class UserRepository(DatabaseContext context) : IUserRepository
    {
        private readonly DatabaseContext _context = context;
        public async Task<LoginResponse> Login(string email, string password)
        {
            LoginResponse response = new LoginResponse();
            try
            {
                string passwordHash = await SecureHelper.AesEncryptAsync(value: password ?? string.Empty);

                var user = await _context.MasterUsers
                    .Where(i => i.Email == email && i.PasswordHash == passwordHash)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid email or password";
                    return response;
                }

                var update = await _context.MasterUsers.FindAsync(user.Id);

                _context.MasterUsers.Update(update);
                await _context.SaveChangesAsync();

                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Login successful";


            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;
        }

        private string CheckReqruimentPassword(string password)
        {
            string message = string.Empty;


            if (password.Length < 8)
            {
                message = "Password must be at least 8 characters long";
            }
            else if (!password.Any(char.IsUpper))
            {
                message = "Password must contain at least one uppercase letter";
            }
            else if (!password.Any(char.IsLower))
            {
                message = "Password must contain at least one lowercase letter";
            }
            else if (!password.Any(char.IsDigit))
            {
                message = "Password must contain at least one digit";
            }
            else if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                message = "Password must contain at least one special character";
            }
            return message;
        }

        public async Task<DefaultResponse> Register(string email, string password)
        {
            DefaultResponse response = new DefaultResponse();
            try
            {
                var checkPassword = CheckReqruimentPassword(password);
                if (!string.IsNullOrEmpty(checkPassword))
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = checkPassword;
                    return response;
                }

                string passwordHash = await SecureHelper.AesEncryptAsync(value: password ?? string.Empty);

                var user = await _context.MasterUsers
                    .Where(i => i.Email == email && i.PasswordHash == passwordHash)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "User already exists";
                }
                else
                {
                    user = new MasterUserModel
                    {
                        Email = email,
                        PasswordHash = passwordHash,
                        IsActive = true,
                    };
                    await _context.MasterUsers.AddAsync(user);
                    await _context.SaveChangesAsync();
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = "User registered successfully";
                }

                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;

            }
            return response;

        }
    }
}
