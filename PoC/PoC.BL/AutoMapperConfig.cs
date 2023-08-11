using AutoMapper;
using PoC.DomainEntities.Jobs;
using PoC.Dtos;
using PoC.Dtos.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.BL
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Job, StatusObjectResponseDto>()
              .ForMember(p => p.Status, pp => pp.MapFrom(p => p.StatusEnum.ToString()))
              .ForMember(p => p.Progress, pp => pp.MapFrom(p => p.Progress))
              .ForMember(p => p.Result, pp => pp.MapFrom(p => p.Result));

            CreateMap<Job, JobResponseDto>()
             .ForMember(p => p.Status, pp => pp.MapFrom(p => p.StatusEnum.ToString()))
             .ForMember(p => p.JobId, pp => pp.MapFrom(p => p.JobId))
             .ForMember(p => p.StartedAt, pp => pp.MapFrom(p => p.StartedAt))
             .ForMember(p => p.FirstName, pp => pp.MapFrom(p => p.FirstName))
             .ForMember(p => p.LastName, pp => pp.MapFrom(p => p.LastName))
             .ForMember(p => p.CompletedAt, pp => pp.MapFrom(p => p.CompletedAt))
             .ForMember(p => p.Progress, pp => pp.MapFrom(p => p.Progress));
        }        
    }
}
