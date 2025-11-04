using WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Newtonsoft.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace WebSite.Controllers
{
    public class RankCheckerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Check([FromBody]RankCheckerViewModel InputSteamID)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            string SteamID = InputSteamID.SteamID;
            string URL = $"https://api-public.cs-prod.leetify.com/v3/profile?steam64_id={SteamID}";
            bool HaveAccess = false;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpResponseMessage response = await client.GetAsync(URL);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    using JsonDocument document = JsonDocument.Parse(jsonResponse);
                    JsonElement root = document.RootElement;


                    // В строку какие категории будем дергать из JSON'а:
                    JsonElement rating = root.GetProperty("rating");


                    // Здесь прописываются критерии отбора
                    if (rating.GetProperty("aim").GetDouble() >= 0.5)
                    {
                        Console.WriteLine("Может участвовать");
                        HaveAccess = true;
                    }
                    else
                    {
                        Console.WriteLine("Не может участвовать");
                        HaveAccess = false;
                    }


                    // Объект со значениями JSON (которые можем записывать в бд, или еще куда-то)
                    var playerStats = new
                    {
                        Aim = rating.GetProperty("aim").GetDouble(),
                        Positioning = rating.GetProperty("positioning").GetDouble(),
                        Utility = rating.GetProperty("utility").GetDouble(),
                        Clutch = rating.GetProperty("clutch").GetDouble(),
                        Opening = rating.GetProperty("opening").GetDouble(),
                        CTLeetify = rating.GetProperty("ct_leetify").GetDouble(),
                        TLeetify = rating.GetProperty("t_leetify").GetDouble(),
                        Access = HaveAccess  // Может ли юзер участвовать в турнире?
                    };

                    return Json(playerStats);
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
