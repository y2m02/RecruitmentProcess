using System.Collections.Generic;
using System.Net.Http.Headers;

namespace RecruitmentManagementApp.Client
{
    public class RequestHeader
    {
        /// <param name="validate">
        ///     Whether the header should be validated when added to the request.
        /// </param>
        public RequestHeader(string name, IEnumerable<string> values, bool validate = true)
        {
            Name = name;
            Values = values;
            Validate = validate;
        }

        /// <summary>
        ///     The header name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The header values.
        /// </summary>
        public IEnumerable<string> Values { get; set; }

        /// <summary>
        ///     Whether the header should be validated when added to the request.
        /// </summary>
        public bool Validate { get; set; } = true;

        /// <summary>
        ///     Adds self to <paramref name="headers" />.
        /// </summary>
        public void Attach(HttpRequestHeaders headers)
        {
            if (Validate)
            {
                headers.Add(Name, Values);
            }
            else
            {
                headers.TryAddWithoutValidation(Name, Values);
            }
        }
    }
}
