using Holidays.Data;
using Holidays.Interfaces;
using Holidays.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.Controllers
{
    [Route("api/Holidays")]
    public class HolidaysController : Controller
    {
        private readonly IPublicHolidayService _publicHolidayService;
        private readonly HolidayContext _holidayContext;

        public HolidaysController(IPublicHolidayService publicHolidayService, HolidayContext holidayContext)
        {
            _publicHolidayService = publicHolidayService;
            _holidayContext = holidayContext;
        }

        [HttpGet("{year}")]
        public async Task<IActionResult> GetByYear(int year)
        {
            var holidays = new List<HolidayDto>();
            var publicHolidays = await _publicHolidayService.GetByYearAsync(year);
            var workHolidays = await _holidayContext.Holidays.ToListAsync();

            holidays.AddRange(publicHolidays.Select(holiday => new HolidayDto(holiday.Date, holiday.Name)));
            holidays.AddRange(workHolidays.Select(holiday => new HolidayDto(
                new DateTime(year, holiday.Date.Month, holiday.Date.Day),
                holiday.Name)));

            return Ok(holidays);
        }

        [HttpGet("{month}")]
        public async Task<IActionResult> GetByMonth(int month)
        {
            var year = 2021;
            var holidays = new List<HolidayDto>();
            var publicHolidays = await _publicHolidayService.GetByMonthAsync(year, month);
            var workHolidays = (await _holidayContext.Holidays.ToListAsync()).Where(holiday => holiday.Date.Month == month);

            holidays.AddRange(publicHolidays.Select(holiday => new HolidayDto(holiday.Date, holiday.Name)));
            holidays.AddRange(workHolidays.Select(holiday => new HolidayDto(
                new DateTime(year, holiday.Date.Month, holiday.Date.Day),
                holiday.Name)));

            return Ok(holidays);
        }
    }
}
