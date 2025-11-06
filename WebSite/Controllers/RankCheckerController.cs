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

            string SteamID64 = InputSteamID.SteamID;
            var SteamID32 = long.Parse(SteamID64) - 76561197960265728;
            string LeetifyURL = $"https://api-public.cs-prod.leetify.com/v3/profile?steam64_id={SteamID64}";
            string OpenDotaURL = $"https://api.opendota.com/api/players/{SteamID32}";
            int CS2SkillLevel = 0;
            int Dota2SkillLevel = 0;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    // Запрос к Leetify API
                    HttpResponseMessage LeetifyResponse = await client.GetAsync(LeetifyURL);
                    LeetifyResponse.EnsureSuccessStatusCode();
                    string CS2JsonResponse = await LeetifyResponse.Content.ReadAsStringAsync();
                    using JsonDocument CS2document = JsonDocument.Parse(CS2JsonResponse);
                    JsonElement CS2root = CS2document.RootElement;

                    // Запрос к OpenDota API
                    HttpResponseMessage OpenDotaResponse = await client.GetAsync(OpenDotaURL);
                    OpenDotaResponse.EnsureSuccessStatusCode();
                    string OpenDotaJsonResponse = await OpenDotaResponse.Content.ReadAsStringAsync();
                    using JsonDocument OpenDotadocument = JsonDocument.Parse(OpenDotaJsonResponse);
                    JsonElement OpenDotaRoot = OpenDotadocument.RootElement;

                    // В строку какие категории будем вытаскивать из JSON'а:
                    JsonElement CS2Rating = CS2root.GetProperty("ranks");
                    JsonElement Dota2Rating = OpenDotaRoot.GetProperty("profile");

                    // Здесь прописываются критерии отбора (CS2)
                    switch (CS2Rating.GetProperty("faceit").GetDouble()) 
                    {
                        case 0: // Игрок не может участвовать
                            CS2SkillLevel = 0;
                            break;

                        case > 0 and <= 2.5: // Слабый игрок
                            CS2SkillLevel = 1;
                            break;

                        case > 2.5 and <= 5: // Средний игрок
                            CS2SkillLevel = 2;
                            break;

                        case > 5 and <= 7.5: // Хороший игрок
                            CS2SkillLevel = 3;
                            break;

                        case > 7.5: // Отличный игрок
                            CS2SkillLevel = 4;
                            break;
                    }

                    // Здесь прописываются критерии отбора (Dota2)
                    switch (Dota2Rating.GetProperty("rank_tier").GetDouble() / 100)
                    {
                        case 0: // Игрок не может участвовать
                            Dota2SkillLevel = 0;
                            break;

                        case > 0 and <= 2.5: // Слабый игрок
                            Dota2SkillLevel = 1;
                            break;

                        case > 2.5 and <= 5: // Средний игрок
                            Dota2SkillLevel = 2;
                            break;

                        case > 5 and <= 7.5: // Хороший игрок
                            Dota2SkillLevel = 3;
                            break;

                        case > 7.5: // Отличный игрок
                            Dota2SkillLevel = 4;
                            break;
                    }

                    // Объект со значениями JSON (которые можем записывать в бд, или еще куда-то)
                    var playerStats = new
                    {
                        leetify = CS2Rating.GetProperty("leetify").GetDouble(),
                        premier = CS2Rating.GetProperty("premier").GetDouble(),
                        faceit = CS2Rating.GetProperty("faceit").GetDouble(),
                        faceit_elo = CS2Rating.GetProperty("faceit_elo").GetDouble(),
                        wingman = CS2Rating.GetProperty("wingman").GetDouble(),
                        personaname = Dota2Rating.GetProperty("personaname").GetString(),
                        rank_tier = OpenDotaRoot.GetProperty("rank_tier").GetDouble(), // Если у пользователя нет ранга (по каким то причинам), может выдать исключение
                        CS2_Tourniment_Access_Level = CS2SkillLevel,  // Может ли юзер участвовать в турнире?
                        Dota2_Tourniment_Access_Level = Dota2Rating  // Может ли юзер участвовать в турнире?
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
