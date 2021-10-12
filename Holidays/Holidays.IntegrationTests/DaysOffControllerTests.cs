using System;
using Holidays.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Holidays.IntegrationTests.Setup;

namespace Holidays.IntegrationTests
{
    [TestFixture]
    public class DaysOffControllerTests : IntegrationTest
    {
        [TestCase(2021)]
        [TestCase(2020)]
        [TestCase(2019)]
        public async Task Get_DaysOff_For_Year_Called_Successfully(int year)
        {
            var expectedWorkHolidaysDays = GetExpectedResults(year);

            var response = await TestClient.GetAsync($"api/DaysOff/{year}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var holidays = await ConvertResponse(response);

            Assert.AreEqual(19, holidays.Count);
            foreach (var (holiday, index) in expectedWorkHolidaysDays)
            {
                Assert.AreEqual(
                    holiday.Name, holidays[index].Name,
                    $"Expected Holiday name to be '{holiday.Name}' but was '{holidays[index].Name}'");
                Assert.AreEqual(
                    holiday.Date, holidays[index].Date,
                    $"Expected Holiday '{holiday.Name}' to be on date '{holiday.Date:dd/MM/yyyy} {holiday.Date.DayOfWeek}' but was on date '{holidays[index].Date:dd/MM/yyyy} {holidays[index].Date.DayOfWeek}'");
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