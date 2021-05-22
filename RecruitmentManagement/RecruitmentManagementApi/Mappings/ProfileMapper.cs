using System.Linq;
using AutoMapper;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Services;

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
        }
    }
}
