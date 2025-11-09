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
            string SteamID32 = CalculateSteamID32(SteamID64);
            string LeetifyURL = $"https://api-public.cs-prod.leetify.com/v3/profile?steam64_id={SteamID64}";
            string OpenDotaURL = $"https://api.opendota.com/api/players/{SteamID32}";
            int CS2SkillLevel = 0;
            int Dota2SkillLevel = 0;

            // Запрос к Leetify
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

                    // В строку какие категории будем вытаскивать из JSON'а:
                    JsonElement CS2Rating = CS2root.GetProperty("ranks");

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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            // Запрос к OpenDota
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    // Запрос к OpenDota API
                    HttpResponseMessage OpenDotaResponse = await client.GetAsync(OpenDotaURL);
                    OpenDotaResponse.EnsureSuccessStatusCode();
                    string OpenDotaJsonResponse = await OpenDotaResponse.Content.ReadAsStringAsync();
                    using JsonDocument OpenDotadocument = JsonDocument.Parse(OpenDotaJsonResponse);
                    JsonElement OpenDotaRoot = OpenDotadocument.RootElement;

                    // В строку какие категории будем вытаскивать из JSON'а:
                    //JsonElement Dota2Rating = OpenDotaRoot.GetProperty("profile");

                    // Здесь прописываются критерии отбора (Dota2)
                    switch (OpenDotaRoot.GetProperty("rank_tier").GetDouble())
                    {
                        case 0: // Игрок не может участвовать
                            Dota2SkillLevel = 0;
                            break;

                        case > 0 and <= 21: // Слабый игрок
                            Dota2SkillLevel = 1;
                            break;

                        case > 21 and <= 43: // Средний игрок
                            Dota2SkillLevel = 2;
                            break;

                        case > 43 and <= 65: // Хороший игрок
                            Dota2SkillLevel = 3;
                            break;

                        case > 65: // Отличный игрок
                            Dota2SkillLevel = 4;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            // Ответ
            var result = new
            {
                CS2SkillLevel = CS2SkillLevel,
                Dota2SkillLevel = Dota2SkillLevel
            };
            return Json(result);
        }

        public static string CalculateSteamID32(string steamID64)
        {
            long steamID64Long = long.Parse(steamID64);
            long steamID32Long = steamID64Long - 76561197960265728;
            return steamID32Long.ToString();
        }

    }
}
