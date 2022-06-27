using System;
using Holidays.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Holidays.IntegrationTests.Setup;

namespace Holidays.IntegrationTests
{
    [TestFixture]
    public class DaysOffControllerTests : IntegrationTest
    {
        [Test]
        public async Task _1_2_Correct_Endpoint_Exposed()
        {
            var response = await TestClient.GetAsync($"api/DaysOff/2020");
            Assert.AreNotEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
        
        [TestCase(2021)]
        [TestCase(2020)]
        [TestCase(2019)]
        public async Task _1_3_Correct_Number_Of_Holidays_Returned_Successfully(int year)
        {
            var expectedWorkHolidaysDays = GetExpectedResults(year);

            var response = await TestClient.GetAsync($"api/DaysOff/{year}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var holidays = await ConvertResponse(response);
            Assert.AreEqual(19, holidays.Count);
        }
        
        [TestCase(2021)]
        [TestCase(2020)]
        [TestCase(2019)]
        public async Task _1_4_Company_Holidays_Are_Moved_To_Next_Available_Work_Day_Successfully(int year)
        {
            var expectedWorkHolidaysDays = GetExpectedResults(year);

            var response = await TestClient.GetAsync($"api/DaysOff/{year}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var holidays = await ConvertResponse(response);

            foreach (var holiday in holidays)
            {
                var expectedHoliday = expectedWorkHolidaysDays.FirstOrDefault(day => day.holiday.Name == holiday.Name).holiday;
                
                if(expectedHoliday is null)
                    continue;
                
                Assert.AreEqual(
                    expectedHoliday.Date, holiday.Date,
                    $"Expected Holiday '{holiday.Name}' to be on date '{expectedHoliday.Date:dd/MM/yyyy} {expectedHoliday.Date.DayOfWeek}' but was on date '{holiday.Date:dd/MM/yyyy} {holiday.Date.DayOfWeek}'");
            }
        }
        
        [TestCase(2021)]
        [TestCase(2020)]
        [TestCase(2019)]
        public async Task _1_5_Holidays_Are_Ordered_Ascending_Date_Successfully(int year)
        {
            var expectedWorkHolidaysDays = GetExpectedResults(year);

            var response = await TestClient.GetAsync($"api/DaysOff/{year}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var holidays = await ConvertResponse(response);
            
            foreach (var (holiday, index) in expectedWorkHolidaysDays)
            {
                Assert.AreEqual(
                    holiday.Name, holidays[index].Name,
                    $"Expected Holiday '{holiday.Name}' to be at index {index} in the holiday collection");
            }
        }

        private static List<(HolidayDto holiday, int index)> GetExpectedResults(int year)
        {
            var resultsByYear
                = new Dictionary<int, List<(HolidayDto holiday, int index)>>
                {
                    [2021] = new()
                    {
                        (new HolidayDto(new DateTime(2021, 04, 06), HolidayNames.InceptionDay), 6),
                        (new HolidayDto(new DateTime(2021, 03, 26), HolidayNames.JeffsBirthday ), 3),
                        (new HolidayDto(new DateTime(2021, 10, 13), HolidayNames.MsIgniteDay), 15),
                        (new HolidayDto(new DateTime(2021, 08, 10), HolidayNames.PolkadotDay), 13),
                        (new HolidayDto(new DateTime(2021, 01, 18), HolidayNames.CreativeDay), 1),
                        (new HolidayDto(new DateTime(2021, 07, 19), HolidayNames.SpaceDay), 11),
                        (new HolidayDto(new DateTime(2021, 04, 26), HolidayNames.BmwDay), 7)
                    },
                    [2020] = new()
                    {
                        (new HolidayDto(new DateTime(2020, 04, 06), HolidayNames.InceptionDay), 4),
                        (new HolidayDto(new DateTime(2020, 03, 26), HolidayNames.JeffsBirthday), 3),
                        (new HolidayDto(new DateTime(2020, 10, 13), HolidayNames.MsIgniteDay), 15),
                        (new HolidayDto(new DateTime(2020, 08, 10), HolidayNames.PolkadotDay), 13),
                        (new HolidayDto(new DateTime(2020, 01, 20), HolidayNames.CreativeDay), 1),
                        (new HolidayDto(new DateTime(2020, 07, 20), HolidayNames.SpaceDay), 11),
                        (new HolidayDto(new DateTime(2020, 04, 28), HolidayNames.BmwDay), 8)
                    },
                    [2019] = new()
                    {
                        (new HolidayDto(new DateTime(2019, 04, 05), HolidayNames.InceptionDay), 4),
                        (new HolidayDto(new DateTime(2019, 03, 26), HolidayNames.JeffsBirthday), 3),
                        (new HolidayDto(new DateTime(2019, 10, 14), HolidayNames.MsIgniteDay), 15),
                        (new HolidayDto(new DateTime(2019, 08, 08), HolidayNames.PolkadotDay), 12),
                        (new HolidayDto(new DateTime(2019, 01, 18), HolidayNames.CreativeDay), 1),
                        (new HolidayDto(new DateTime(2019, 07, 19), HolidayNames.SpaceDay), 11),
                        (new HolidayDto(new DateTime(2019, 04, 25), HolidayNames.BmwDay), 7)
                    }
                };

            return resultsByYear[year];
        }
    }
}