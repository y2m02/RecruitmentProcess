using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HelpersLibrary.Extensions;
using RecruitmentManagementApi.Models;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Models.Responses;
using RecruitmentManagementApi.Models.Responses.Base;
using RecruitmentManagementApi.Repositories;
using RecruitmentManagementApi.Services.Base;

namespace RecruitmentManagementApi.Services
{
    public interface ICandidateService :
        IBaseService,
        ICanUpdateService,
        ICanDeleteService { }

    public class CandidateService :
        BaseService<Candidate, CandidateResponse>,
        ICandidateService
    {
        public CandidateService(
            IMapper mapper,
            ICandidateRepository repository
        ) : base(mapper)
        {
            Repository = repository;
        }

        protected override async Task<Candidate> CreateEntity(IRequest entity)
        {
            var candidate = Mapper
                .Map<Candidate>(entity)
                .Tap(
                    x => x.Recruitment = new()
                    {
                        Date = x.Date,
                        Status = RecruitmentStatus.Pending,
                        RecruitmentUpdateHistories = new List<RecruitmentUpdateHistory>
                        {
                            new()
                            {
                                Date = x.Date, Status = RecruitmentStatus.Pending,
                            },
                        },
                    }
                );

            return await Repository.Create(candidate).ConfigureAwait(false);
        }
    }
}