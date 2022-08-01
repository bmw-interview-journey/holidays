using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Holidays.Model;
using Newtonsoft.Json;

namespace Holidays.IntegrationTests.Helpers
{
    public static class HttpExtensions
    {
        public static async Task<IList<HolidayDto>> ToHolidaysAsync(this HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<HolidayDto>>(content);
        }
    }
}