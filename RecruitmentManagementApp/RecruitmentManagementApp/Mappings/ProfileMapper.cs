using System.Linq;
using AutoMapper;
using RecruitmentManagementApp.Models.Entities;
using RecruitmentManagementApp.Services;

namespace RecruitmentManagementApp.Mappings
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Status, StatusViewModel>()
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
