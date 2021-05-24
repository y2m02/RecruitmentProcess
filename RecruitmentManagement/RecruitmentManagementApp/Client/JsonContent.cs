using System.Net.Http;
using System.Text;

namespace RecruitmentManagementApp.Client
{
    public class JsonContent : ByteArrayContent
    {
        private const string MEDIA_TYPE = "application/json";

        public JsonContent(string json) : base(Encoding.UTF8.GetBytes(json))
        {
            Headers.ContentType = new(MEDIA_TYPE);
        }

        public static JsonContent Make(string json)
        {
            return new(json);
        }
    }
}
