using System.Linq;
using AutoMapper;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Extensions;
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

            CreateMap<UpdateStatusRequest, Status>()
                .ForMember(
                    destination => destination.StatusId,
                    member => member.MapFrom(field => field.Id)
                );

            CreateMap<DeleteStatusRequest, Status>()
                .ForMember(
                    destination => destination.StatusId,
                    member => member.MapFrom(field => field.Id)
                );


            CreateMap<Candidate, CandidateResponse>()
                .ForMember(
                    destination => destination.Id,
                    member => member.MapFrom(field => field.CandidateId)
                )
                .ForMember(
                    destination => destination.InUse,
                    member => member.MapFrom(
                        field =>
                            field.Recruitment.HasValue() ||
                            field.RecruitmentUpdateHistories.Any()
                    )
                )
                .ForMember(
                    destination => destination.StatusDescription,
                    member => member.MapFrom(field => field.Status.Description)
                );
        }
    }
}