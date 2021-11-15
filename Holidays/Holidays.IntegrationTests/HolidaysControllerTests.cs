using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;
using Holidays.IntegrationTests.Setup;

namespace Holidays.IntegrationTests
{
    [TestFixture]
    public class HolidaysControllerTests : IntegrationTest
    {
        [Test]
        public async Task _2_Get_Holidays_By_Year_Called_Successfully()
        {
            var response = await TestClient.GetAsync("api/holidays/2021");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var holidays = await ConvertResponse(response);

            Assert.AreEqual(19, holidays.Count);
        }

        [Test]
        public async Task _2_Get_Holidays_By_Month_Called_Successfully()
        {
            var response = await TestClient.GetAsync("api/holidays/month/04");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var holidays = await ConvertResponse(response);

            Assert.AreEqual(5, holidays.Count);
        }
    }
}
