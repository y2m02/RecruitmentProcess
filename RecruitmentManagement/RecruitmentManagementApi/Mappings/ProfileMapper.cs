﻿using System;
using System.Linq;
using AutoMapper;
using HelpersLibrary.Extensions;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Request.AuthorizationKey;
using RecruitmentManagementApi.Models.Request.Candidates;
using RecruitmentManagementApi.Models.Request.Logs;
using RecruitmentManagementApi.Models.Request.Recruitments;
using RecruitmentManagementApi.Models.Responses;

namespace RecruitmentManagementApi.Mappings
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
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
                    destination => destination.RecruitmentId,
                    member => member.MapFrom(field => field.Recruitment.RecruitmentId)
                )
                .ForMember(
                    destination => destination.RecruitmentStatus,
                    member => member.MapFrom(field => field.Recruitment.Status)
                )
                .ForMember(
                    destination => destination.PhoneNumber,
                    member => member.MapFrom(
                        field => field.PhoneNumber.IsEmpty()
                            ? string.Empty
                            : long.Parse(field.PhoneNumber).ToString("000-000-0000")
                    )
                )
                .ForMember(
                    destination => destination.Email,
                    member => member.MapFrom(
                        field => field.Email.IsEmpty()
                            ? string.Empty
                            : field.Email
                    )
                )
                .ForMember(
                    destination => destination.Curriculum,
                    member => member.MapFrom(
                        field => field.Curriculum.IsEmpty()
                            ? string.Empty
                            : field.Curriculum
                    )
                )
                .ForMember(
                    destination => destination.GitHub,
                    member => member.MapFrom(
                        field => field.GitHub.IsEmpty()
                            ? string.Empty
                            : field.GitHub
                    )
                );

            CreateMap<CandidateRequest, Candidate>()
                .ForMember(
                    destination => destination.Date,
                    member => member.MapFrom(field => DateTime.Now)
                );

            CreateMap<UpdateCandidateRequest, Candidate>()
                .ForMember(
                    destination => destination.CandidateId,
                    member => member.MapFrom(field => field.Id)
                );

            CreateMap<Recruitment, RecruitmentResponse>()
                .ForMember(
                    destination => destination.Id,
                    member => member.MapFrom(field => field.RecruitmentId)
                )
                .ForMember(
                    destination => destination.InUse,
                    member => member.MapFrom(
                        field => field.Candidate.HasValue() ||
                                 field.RecruitmentUpdateHistories.Any()
                    )
                )
                .ForMember(
                    destination => destination.CandidateName,
                    member => member.MapFrom(field => field.Candidate.Name)
                )
                .ForMember(
                    destination => destination.RecruitmentUpdateHistories,
                    member => member.MapFrom(
                        field => field.RecruitmentUpdateHistories.OrderByDescending(r => r.Date).Skip(1)
                    )
                );

            CreateMap<UpdateRecruitmentRequest, Recruitment>()
                .ForMember(
                    destination => destination.RecruitmentId,
                    member => member.MapFrom(field => field.Id)
                );

            CreateMap<RecruitmentUpdateHistory, RecruitmentUpdateHistoryResponse>()
                .ForMember(
                    destination => destination.Id,
                    member => member.MapFrom(field => field.RecruitmentUpdateHistoryId)
                )
                .ForMember(
                    destination => destination.InUse,
                    member => member.MapFrom(field => field.Recruitment.HasValue())
                )
                .ForMember(
                    destination => destination.CandidateId,
                    member => member.MapFrom(field => field.Recruitment.Candidate.CandidateId)
                )
                .ForMember(
                    destination => destination.CandidateName,
                    member => member.MapFrom(field => field.Recruitment.Candidate.Name)
                );

            CreateMap<AuthorizationKey, AuthorizationKeyResponse>()
                .ForMember(
                    destination => destination.Id,
                    member => member.MapFrom(field => field.AuthorizationKeyId)
                )
                .ForMember(
                    destination => destination.InUse,
                    member => member.MapFrom(field => field.IsActive)
                );

            CreateMap<AuthorizationKeyRequest, AuthorizationKey>()
                .ForMember(
                    destination => destination.Permissions,
                    member => member.MapFrom(field => field.Permissions.Join("-"))
                );


            CreateMap<Log, LogResponse>()
                .ForMember(
                    destination => destination.Id,
                    member => member.MapFrom(field => field.LogId)
                )
                .ForMember(
                    destination => destination.Api,
                    member => member.MapFrom(field => field.Api.EnumToString())
                );

            CreateMap<LogRequest, Log>();
        }
    }
}