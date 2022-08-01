using Holidays.Data;
using Holidays.Data.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Holidays.IntegrationTests.Helpers;
using NUnit.Framework;

namespace Holidays.IntegrationTests
{
    [SetUpFixture]
    public class IntegrationTestSetup
    {
        [OneTimeSetUp]
        public async Task Setup()
        {
            var appFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureServices(
                        services =>
                        {
                            services.RemoveAll(typeof(HolidayContext));
                            services.AddDbContext<HolidayContext>(
                                options => options.UseInMemoryDatabase(databaseName: "CompanyHoliday"));
                        });
                });

            IntegrationTestContext.TestClient = appFactory.CreateClient();

            await SeedDbAsync(appFactory.Services);
        }

        private async Task SeedDbAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<HolidayContext>();
            context.Holidays.Add(new Holiday { Id = Guid.NewGuid(), Name = HolidayNames.InceptionDay, Date = new DateTime(1992, 04, 05) });
            context.Holidays.Add(new Holiday { Id = Guid.NewGuid(), Name = HolidayNames.JeffsBirthday, Date = new DateTime(1992, 03, 26) });
            context.Holidays.Add(new Holiday { Id = Guid.NewGuid(), Name = HolidayNames.MsIgniteDay, Date = new DateTime(1992, 10, 13) });
            context.Holidays.Add(new Holiday { Id = Guid.NewGuid(), Name = HolidayNames.PolkadotDay, Date = new DateTime(1992, 08, 08) });
            context.Holidays.Add(new Holiday { Id = Guid.NewGuid(), Name = HolidayNames.CreativeDay, Date = new DateTime(1992, 01, 18) });
            context.Holidays.Add(new Holiday { Id = Guid.NewGuid(), Name = HolidayNames.SpaceDay, Date = new DateTime(1992, 07, 19) });
            context.Holidays.Add(new Holiday { Id = Guid.NewGuid(), Name = HolidayNames.BmwDay, Date = new DateTime(1992, 04, 25) });
            await context.SaveChangesAsync();
        }
    }

    public static class IntegrationTestContext
    {
        public static HttpClient TestClient;
    }
}