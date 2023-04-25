using System.Net;
using System.Threading.Tasks;
using Holidays.IntegrationTests.Helpers;
using NUnit.Framework;

namespace Holidays.IntegrationTests.Tests
{
    [TestFixture]
    public class HolidaysControllerTests
    {
        [Test]
        public async Task Get_Holidays_By_Year_Called_Successfully()
        {
            var response = await IntegrationTestContext.TestClient.GetAsync("api/holidays/2021");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var holidays = await response.ToHolidaysAsync();

            Assert.AreEqual(19, holidays.Count);
        }

        [Test]
        public async Task Get_Holidays_By_Month_Called_Successfully()
        {
            var response = await IntegrationTestContext.TestClient.GetAsync("api/holidays/2021/04");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var holidays = await response.ToHolidaysAsync();

            Assert.AreEqual(5, holidays.Count);
        }
    }
}
