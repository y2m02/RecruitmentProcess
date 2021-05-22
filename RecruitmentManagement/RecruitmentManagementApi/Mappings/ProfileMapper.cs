using System.Linq;
using AutoMapper;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Request;
using RecruitmentManagementApi.Models.Request.Statuses;
using RecruitmentManagementApi.Models.Responses;

namespace RecruitmentManagementApi.Mappings
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Status, StatusResponse>()
                .ForMember(
                    destination => destination.Id,
                    member => member.MapFrom(field => field.StatusId)
                )
                .ForMember(
                    destination => destination.InUse,
                    member => member.MapFrom(
                        field =>
                            field.Candidates.Any() ||
                            field.Recruitments.Any() ||
                            field.RecruitmentUpdateHistories.Any()
                    )
                );

            CreateMap<StatusRequest, Status>();

            //CreateMap<StatusRequest, Status>()
            //    .ForMember(
            //        destination => destination.StatusId,
            //        member => member.MapFrom(field => field.Id)
            //    );
        }
    }
}