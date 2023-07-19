
namespace TT.Deliveries.Data.Dto
{
    using System;

    public class AccessWindow
    {
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; } = DateTime.Now.AddHours(1);
    }


}