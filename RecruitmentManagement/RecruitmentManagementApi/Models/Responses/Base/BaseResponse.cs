using System.Collections.Generic;
using RecruitmentManagementApi.Models.Enums;

namespace RecruitmentManagementApi.Models.Responses.Base
{
    public abstract class BaseResponse : ResponseType
    {
        public int Id { get; set; }

        public UpsertActionType UpsertActionType => SetAction();

        public bool InUse { get; set; }

        private UpsertActionType SetAction()
        {
            return Id > 0
                ? UpsertActionType.Update
                : UpsertActionType.Create;
        }

        public abstract IEnumerable<string> Validate();
    }
}
