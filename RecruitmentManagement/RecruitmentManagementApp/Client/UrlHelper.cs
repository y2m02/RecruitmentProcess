using System.Linq;

namespace RecruitmentManagementApp.Client
{
    public static class UrlHelper
    {
        /// <summary>
        ///     Combines URI parts, taking care of trailing and starting slashes. See http://stackoverflow.com/a/6704287.
        /// </summary>
        public static string Combine(params string?[] uriParts)
        {
            var uri = string.Empty;

            if (uriParts == null || uriParts.Length == 0) return uri;

            uriParts = uriParts.Where(part => !string.IsNullOrWhiteSpace(part)).ToArray();

            var charactersToTrim = new[] { '\\', '/' };

            uri = (uriParts[0] ?? string.Empty).TrimEnd(charactersToTrim);

            foreach (var part in uriParts.Skip(1))
            {
                uri = $"{uri.TrimEnd(charactersToTrim)}/{(part ?? string.Empty).TrimStart(charactersToTrim)}";
            }

            return uri;
        }
    }
}
