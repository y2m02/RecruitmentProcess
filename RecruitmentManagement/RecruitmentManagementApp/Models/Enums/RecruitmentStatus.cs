using System.ComponentModel;

namespace RecruitmentManagementApp.Models.Enums
{
    public enum RecruitmentStatus
    {
        [Description("Paso 1 - Pendiente")]
        Pending = 0,

        [Description("Paso 2 - A entrevistar")]
        Interview = 1,

        [Description("Paso 3 - Prueba")]
        Test = 2,

        [Description("Paso 4 - RR. HH.")]
        HR = 3,

        [Description("Paso 5 - USA")]
        USA = 4,

        [Description("Paso 6 - Contratado")]
        Hired = 5,

        [Description("Descartado")]
        Discarded = 6,
    }
}