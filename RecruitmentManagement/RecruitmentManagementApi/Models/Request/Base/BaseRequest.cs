using System.Collections.Generic;
using RecruitmentManagementApi.Models.Enums;

namespace RecruitmentManagementApi.Models.Request.Base
{
    public abstract class BaseRequest
    {
        public int Id { get; set; }

        public UpsertActionType UpsertActionType => SetAction();

        private UpsertActionType SetAction()
        {
            return Id > 0
                ? UpsertActionType.Update
                : UpsertActionType.Create;
        }

        public abstract IEnumerable<string> Validate();
    }
}