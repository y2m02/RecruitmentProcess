using System;
using System.Collections.Generic;
using AutoMapper;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Extensions;
using RecruitmentManagementApi.Models.Request.Candidates;
using RecruitmentManagementApi.Models.Request.Recruitments;
using RecruitmentManagementApi.Models.Request.RecruitmentUpdateHistories;
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
                );

            //.ForMember(
            //    destination => destination.InUse,
            //    member => member.MapFrom(
            //        field =>
            //            field.Candidates.Any() ||
            //            field.Recruitments.Any() ||
            //            field.RecruitmentUpdateHistories.Any()
            //    )
            //);

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
                        field => field.Recruitment.HasValue()
                    )
                )
                .ForMember(
                    destination => destination.RecruitmentStatus,
                    member => member.MapFrom(field => field.Recruitment.Status)
                );

            CreateMap<CandidateRequest, Candidate>()
                .ForMember(
                    destination => destination.Date,
                    member => member.MapFrom(field => DateTime.Now)
                );

            CreateMap<RecruitmentRequest, Recruitment>()
                .ForMember(
                    destination => destination.Date,
                    member => member.MapFrom(field => DateTime.Now)
                )
                .ForMember(
                    destination => destination.Status,
                    member => member.MapFrom(field => RecruitmentStatus.Pending)
                );

            CreateMap<RecruitmentUpdateHistoryRequest, RecruitmentUpdateHistory>()
                .ForMember(
                    destination => destination.Date,
                    member => member.MapFrom(field => DateTime.Now)
                );
        }
    }
}