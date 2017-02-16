namespace TogglWorkSyncService
{
    public static class Constants
    {
        public enum Operation
        {
            Create,
            Update
        };

        public const string ApiUrl = "https://www.toggl.com/api";
        public const string ApiVersion = "v8";

        public const string LogLineBreak = "\r\n";
        public const string LogDelimiter = " - ";
        public const string LogDispensableDescription = "No work needed";
        public const string LogErrorDescription = "Error occurred.";

        public const string ErrorInvalidOperationDescription = "Invalid operation.";
    }
}