using Microsoft.AspNetCore.Mvc;
using Solution.Models;
using System.Text.Json;

namespace Solution.ViewComponents
{
    public class AlertViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            try
            {
                AlertModel? model = default;
                var alert = HttpContext.Session.GetString("Alert");
                if (!string.IsNullOrEmpty(alert))
                {
                    model = JsonSerializer.Deserialize<AlertModel>(alert);
                    HttpContext.Session.SetString("Alert", "");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
