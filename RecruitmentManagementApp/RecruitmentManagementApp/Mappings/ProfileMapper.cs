using System;
using AutoMapper;

namespace TaskManagerApi.Mappings
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            //CreateMap<Store, StoreResponse>()
            //    .ForMember(
            //        destination => destination.Used,
            //        member => member.MapFrom(field => field.Assignments.Count > 0)
            //    );

            //CreateMap<AssignmentRequest, Assignment>();
        }
    }
}
