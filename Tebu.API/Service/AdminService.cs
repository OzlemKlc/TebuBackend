using System.Data.Entity;
using Tebu.API.Controllers.DTO;
using Tebu.API.Repository;

namespace Tebu.API.Service
{
    public class AdminService
    {

        private OrderRepository orderRepository;

        public AdminService(OrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }


        public StatisticsResponse GetStatistics()
        {
            var now = DateTime.UtcNow;


            var avgOrderTicks = orderRepository.FindBy(s => s.DeliveredDate != null).ToList()
                .Select(s => new
                {
                    delivery = s.DeliveredDate.Value.Ticks,
                    creation = s.CreationDate.Ticks
                })
                .Select(s => s.delivery - s.creation).DefaultIfEmpty()
                .Average();


            var avgOrderHours = TimeSpan.FromTicks((long)avgOrderTicks).TotalHours;


            DateTime startOfToday = now.Date;

            System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            DayOfWeek fdow = ci.DateTimeFormat.FirstDayOfWeek;
            DayOfWeek today = now.DayOfWeek;
            DateTime startOfWeek = now.AddDays(-(today - fdow)).Date;


            DateTime startOfMonth = new DateTime(now.Year, now.Month, 1).ToUniversalTime();

            var dailySuccess = orderRepository.FindBy(s => s.DeliveredDate != null && s.CreationDate > startOfToday).Count();
            var weeklySuccess = orderRepository.FindBy(s => s.DeliveredDate != null && s.CreationDate > startOfWeek).Count();
            var monthlySuccess = orderRepository.FindBy(s => s.DeliveredDate != null && s.CreationDate > startOfMonth).Count();

            var all = orderRepository.GetAll().Include(s => s.Vehicle).ToList();

            var brands = all.GroupBy(s => s.Vehicle.Brand).Select(s => new ThingCounts
            {
                Count = s.Count(),
                Name = s.Key
            }).ToList();

            var modelYears = all.GroupBy(s => s.Vehicle.Year).Select(s => new ThingCounts
            {
                Count = s.Count(),
                Name = s.Key.ToString()
            }).ToList();

            return new StatisticsResponse
            {
                AvarageDeliveryInHours = avgOrderHours,
                BrandCounts = brands,
                ModelYearCounts = modelYears,
                MonthlySuccessfulOrders = monthlySuccess,
                TodaySuccessfulOrders = dailySuccess,
                WeeklySuccessfulOrders = weeklySuccess,
            };
        }
    }
}
