using System;

namespace Holidays.Data.Entities
{
    public class Holiday
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }
}
