using System.ComponentModel;

namespace RecruitmentManagementApp.Models.Enums
{
    public enum RecruitmentStatus
    {
        [Description("Pendiente")]
        Pending = 0,

        [Description("A entrevistar")]
        Interview = 1,

        [Description("Prueba")]
        Test = 2,

        [Description("RR. HH.")]
        HR = 3,

        [Description("USA")]
        USA = 4,

        [Description("Contratado")]
        Hired = 5,

        [Description("Descartado")]
        Discarded = 6,
    }
}