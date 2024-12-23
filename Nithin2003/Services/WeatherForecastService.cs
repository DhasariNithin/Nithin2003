﻿using Nithin2003.Helpers;
using Nithin2003.Models;
using Nithin2003.Services.Interfaces;

namespace Nithin2003.Services
{

    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/api/find";

        public WeatherForecastService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<WeatherForecastModel>> Find()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAsync<List<WeatherForecastModel>>();
        }
    }
}
    

