namespace Tebu.API.Controllers.DTO
{
    public class StatisticsResponse
    {
        public int TodaySuccessfulOrders { get; set; }
        public int WeeklySuccessfulOrders { get; set; }
        public int MonthlySuccessfulOrders { get; set; }
        public List<ThingCounts> BrandCounts { get; set; }
        public List<ThingCounts> ModelYearCounts { get; set; }
        public double AvarageDeliveryInHours { get; set; }
    }

    public class ThingCounts
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
