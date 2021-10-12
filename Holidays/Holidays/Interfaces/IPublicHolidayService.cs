using Holidays.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holidays.Interfaces
{
    public interface IPublicHolidayService
    {
        Task<IList<PublicHoliday>> GetByYearAsync(int year);
        Task<IList<PublicHoliday>> GetByMonthAsync(int year, int month);
    }
}
