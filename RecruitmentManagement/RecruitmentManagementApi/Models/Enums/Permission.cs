using System.ComponentModel;

namespace RecruitmentManagementApi.Models.Enums
{
    public enum Permission
    {
        [Description("Admin")]
        FullAccess,

        [Description("Read")]
        Read,

        [Description("Create/Update")]
        Write,

        [Description("Delete")]
        Delete,
    }
}