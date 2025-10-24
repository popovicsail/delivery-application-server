using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Delivery.Application.Dtos.CommonDtos.WorkScheduleDto;
using Delivery.Domain.Entities.CommonEntities;

namespace Delivery.Application.Mappings
{
    public class WorkScheduleProfile : Profile
    {
        public WorkScheduleProfile()
        {
            CreateMap<WorkSchedule, WorkScheduleDto>()
                .ForMember(dest => dest.WorkStart, opt => opt.MapFrom(src => src.WorkStart.ToString(@"hh\:mm")))
                .ForMember(dest => dest.WorkEnd, opt => opt.MapFrom(src => src.WorkEnd.ToString(@"hh\:mm")));
        }
    }
}
