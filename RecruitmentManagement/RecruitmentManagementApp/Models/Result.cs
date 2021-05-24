using System.Collections.Generic;

namespace RecruitmentManagementApp.Models
{
    public class Result<T>
    {
        public T Response { get; set; }

        public IEnumerable<string> ValidationErrors { get; set; } = new List<string>();

        public string Error { get; set; }
    }
}
