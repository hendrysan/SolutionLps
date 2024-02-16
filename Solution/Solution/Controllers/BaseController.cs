using Microsoft.AspNetCore.Mvc;
using Solution.Models;
using Solution.Services.Repositories;
using System.Security.Claims;
using System.Text.Json;

namespace Solution.Controllers
{
    public abstract class BaseController : Controller
    {
        public static string ServiceName = "Solution";
        public IHttpContextAccessor _httpContextAccessor2 { get; set; }
        public BaseController()
        {

        }


        //public async Task<MasterUserModel?> GetCurrentUser(IHttpContextAccessor _httpContextAccessor, IUserRepository _userRepository)
        //{

        //    var id = _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        //    if (id == null) return null;

        //    var user = await _userRepository.(Guid.Parse(id));

        //    if (user.StatusCode != System.Net.HttpStatusCode.OK)
        //        return null;

        //    return user.MasterUser;
        //}

        public async Task GetAlert()
        {
            try
            {

                var alert = HttpContext.Session.GetString("Alert");
                if (!string.IsNullOrEmpty(alert))
                {
                    ViewBag.Alert = JsonSerializer.Deserialize<AlertModel>(alert);
                    HttpContext.Session.SetString("Alert", "");
                }

            }
            catch (Exception ex)
            {
                
            }
        }

        public void SetAlert(string message, AlertType type)
        {
            var alert = new AlertModel
            {
                Message = message,
                Type = type
            };
            HttpContext.Session.SetString("Alert", JsonSerializer.Serialize(alert));
        }

        public string? GetUserCode()
        {
            var code = User.Claims.FirstOrDefault(c => c.Type.Contains("Code"))?.Value;
            return code;
        }

        public string? GetUserFullName()
        {
            var fullName = User.Claims.FirstOrDefault(c => c.Type.Contains("FullName"))?.Value;
            return fullName;
        }

        public string? GetUserName()
        {
            var userName = User.Claims.FirstOrDefault(c => c.Type.Contains("UserName"))?.Value;
            return userName;
        }

        public string? GetUserRoleId()
        {
            var roleId = User.Claims.FirstOrDefault(c => c.Type.Contains("RoleId"))?.Value;
            return roleId;
        }

        public string? GetUserRoleName()
        {
            var roleName = User.Claims.FirstOrDefault(c => c.Type.Contains("RoleName"))?.Value;
            return roleName;
        }

        public string? GetUserId()
        {
            var id = User.Claims.FirstOrDefault(c => c.Type.Contains("Id"))?.Value;
            return id;
        }
    }
}
