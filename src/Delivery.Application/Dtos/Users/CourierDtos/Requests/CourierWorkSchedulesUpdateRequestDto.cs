namespace Delivery.Application.Dtos.Users.CourierDtos.Requests;

public class CourierWorkSchedulesUpdateRequestDto
{
    public List<WorkScheduleUpdateRequestDto> Schedules { get; set; } = new();
}
