﻿using System.Collections.Generic;
using RecruitmentManagementApp.Models.Enums;

namespace RecruitmentManagementApp.Models.ViewModels.Base
{
    public abstract class BaseViewModel : BaseReturnViewModel
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
