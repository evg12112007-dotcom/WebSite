using WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebSite.Controllers
{
    public class RankCheckerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Check(RankCheckerViewModel InputSteamID)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            string SteamID = InputSteamID.SteamID;
            string URL = $"https://api-public.cs-prod.leetify.com/v3/profile?steam64_id={SteamID}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpResponseMessage response = await client.GetAsync(URL);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);


                    //Пока вывожу рейтинг, потом можно поменять на другие статы (сейчас не работает, потом починю)
                    var playerStats = new
                    {
                        Aim = data.rating.aim,
                        Positioning = data.rating.positioning,
                        Utility = data.rating.utility,
                        Clutch = data.rating.clutch,
                        Opening = data.rating.opening,
                        CTLeetify = data.rating.ct_leetify,
                        TLeetify = data.rating.t_leetify
                    };

                    return Content(jsonResponse, "application/json");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return StatusCode(500, "Произошла ошибка при обработке запроса.");
            }
        }
    }
}
