using ProjectMars_Playwright.Config;
using ProjectMars_Playwright.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectMars_Playwright.Helpers
{
    public static class EnvironmentSettingsLoader
    {
        public static EnvironmentSettings Load()
        {
            string json = File.ReadAllText("settings.json");

            using JsonDocument doc = JsonDocument.Parse(json);

            string baseUrl = doc.RootElement
                                .GetProperty("Environment")
                                .GetProperty("BaseUrl")
                                .GetString();

            return new EnvironmentSettings
            {
                BaseUrl = baseUrl
            };
        }
    }
}
