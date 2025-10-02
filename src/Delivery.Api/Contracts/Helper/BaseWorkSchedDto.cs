namespace Delivery.Api.Contracts.Helper
{
    public class BaseWorkSchedDto
    {
        public Guid Id { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public TimeSpan WorkDayStart { get; set; }
        public TimeSpan WorkDayEnd { get; set; }
        public TimeSpan? WeekendStart { get; set; }
        public TimeSpan? WeekendEnd { get; set; }
    }
}
