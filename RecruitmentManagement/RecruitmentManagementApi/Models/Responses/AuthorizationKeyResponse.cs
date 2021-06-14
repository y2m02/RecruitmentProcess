using System.Collections.Generic;
using HelpersLibrary.Extensions;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Responses.Base;

namespace RecruitmentManagementApi.Models.Responses
{
    public class AuthorizationKeyResponse : BaseResponse
    {
        public string Key { get; set; }

        public string Permissions { get; set; }

        public List<Permission> PermissionList
        {
            get
            {
                return Permissions.IsEmpty()
                    ? new List<Permission>()
                    : Permissions.Split("-").EagerSelect(x => x.ToEnum<Permission>());
            }
        }

        public bool IsActive { get; set; }

        public bool HasFullAccess()
        {
            return PermissionList.Contains(Permission.FullAccess);
        }

        public bool CanRead()
        {
            return HasFullAccess() || PermissionList.Contains(Permission.Read);
        }

        public bool CanWrite()
        {
            return HasFullAccess() || PermissionList.Contains(Permission.Write);
        }

        public bool CanDelete()
        {
            return HasFullAccess() || PermissionList.Contains(Permission.Delete);
        }
    }
}