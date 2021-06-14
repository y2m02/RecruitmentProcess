namespace RecruitmentManagementApi.Models
{
    public class ConsumerMessages
    {
        public const string FieldRequired = "The field {0} is required.";

        public const string SuccessResponse = "{0} of {1} record/s {2} successfully.";

        public const string Created = "created";

        public const string Updated = "updated";

        public const string Deleted = "removed";

        public const string AllowedValues = "The field {0} does not match with any of its allowed values: {1}";
       
        public const string ApiKeyRequired = "None API key was sent";

        public const string InvalidApiKey = "The key '{0}' is invalid for accessing this API";

        public const string NotAllowedForApiKey = "The key '{0}' has no permissions for this action";

        public const string InvalidKeyLength = "The provided key MUST contain 32 characteres";
    }
}