using System.ComponentModel;

namespace RecruitmentManagementApp.Models.Enums
{
    public enum RecruitmentStatus
    {
        [Description("1. Pendiente")]
        Pending = 0,

        [Description("2. A entrevistar")]
        Interview = 1,

        [Description("3. Prueba")]
        Test = 2,

        [Description("4. RR. HH.")]
        HR = 3,

        [Description("5. USA")]
        USA = 4,

        [Description("6. Contratado")]
        Hired = 5,

        [Description("7. Descartado")]
        Discarded = 6,
    }
}