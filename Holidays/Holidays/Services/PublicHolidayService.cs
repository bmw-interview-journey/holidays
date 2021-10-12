using Holidays.Interfaces;
using Holidays.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Holidays.Services
{
    public class PublicHolidayService : IPublicHolidayService
    {
        private const string Url = "https://date.nager.at/api/v2/publicholidays/";
        private readonly HttpClient _client;

        public PublicHolidayService()
        {
            _client = new HttpClient {BaseAddress = new Uri(Url)};
        }

        public async Task<IList<PublicHoliday>> GetByYearAsync(int year)
        {
            var response = await _client.GetAsync($"{year}/ZA");
            return await ConvertResponse(response);
        }

        public async Task<IList<PublicHoliday>> GetByMonthAsync(int year, int month)
        {
            var response = await _client.GetAsync($"{year}/ZA");
            var holidays = await ConvertResponse(response);
            return holidays.Where(holiday => holiday.Date.Month == month).ToList();
        }

        private async Task<IList<PublicHoliday>> ConvertResponse(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<PublicHoliday>>(content);
        }
    }
}