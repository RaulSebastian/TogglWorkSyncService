using System;
using System.IO;
using TogglWorkSyncService.ApiJObjects;
using static TogglWorkSyncService.Constants;
using static TogglWorkSyncService.RuntimeDefaults;

namespace TogglWorkSyncService.Helpers
{
    public static class LogFile
    {
        private static void AddLineBreak()
        {
            File.AppendAllText(LogFileDestination, LogLineBreak);
        }

        private static string AddTimeStamp(string message)
        {
            return $"{DateTime.Now}{LogDelimiter}{message}";
        }

        public static void AddLogMessage(string message)
        {
            File.AppendAllText(LogFileDestination, AddTimeStamp(message));
            AddLineBreak();
        }

        public static void StartLog()
        {
            AddLogMessage(LogFileStartDescription);
        }

        public static void TerminateLog()
        {
            AddLogMessage(LogFileEndDescription);
        }

        public static void AddTimeEntryOperationLogMessage(Project project, Workspace workspace, Operation operation)
        {
            AddLogMessage($"{operation} time entry for project [{project.Id}|{project.Name}] on workspace [{workspace.Id}|{workspace.Name}] successfull.");
        }

        public static void AddLogErrorMessage(string exMessage)
        {
            AddLogMessage($"{LogErrorDescription}! {exMessage}");
        }
    }
}