using System;
using TogglWorkSyncService.ApiJObjects;
using static System.Convert;
using static TogglWorkSyncService.Constants;
using static System.Configuration.ConfigurationManager;

namespace TogglWorkSyncService
{
    public static class RuntimeDefaults
    {
        public static readonly string ClientName = AppDomain.CurrentDomain.FriendlyName;
        public static readonly string ClientVersion = AppSettings["ClientVersion"];
        
        public static string ApiProjectsEndpoint(Workspace workspace)
            => $"{ApiWorkspacesEndpoint}/{workspace.Id}/projects";

        public static string ApiSingleTimeEntryEndpoint(TimeEntry entry)
            => $"{ApiUrl}/{ApiVersion}/time_entries/{entry.Id}";

        public static readonly string LogFileDestination =
            $"{Environment.GetEnvironmentVariable("LocalAppData")}\\{AppSettings["LogFileDestination"]}";
        public static readonly string LogFileStartDescription = $"Service {ClientVersion} started";
        public static readonly string LogFileEndDescription = $"Service {ClientVersion}  terminated";

        public static readonly string DomainName = AppSettings["DomainName"];
        public static readonly string DomainProjectName = AppSettings["DomainProjectName"];

        public static readonly int DefaultTimeEntryDurationMinutes = ToInt32(AppSettings["DefaultTimeEntryDurationMinutes"]);

        public static readonly string ApiAuthorizationKey = AppSettings["AuthorizationKey"];
        public static readonly string ApiWorkspacesEndpoint = $"{ApiUrl}/{ApiVersion}/workspaces";
        public static readonly string ApiTimeEntriesEndpoint = $"{ApiUrl}/{ApiVersion}/time_entries";
    }
}