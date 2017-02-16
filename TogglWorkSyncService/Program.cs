using System;
using System.Linq;
using TogglWorkSyncService.ApiJObjects;
using TogglWorkSyncService.Helpers;
using static TogglWorkSyncService.Constants;
using static TogglWorkSyncService.RuntimeDefaults;

// ReSharper disable PossibleMultipleEnumeration

namespace TogglWorkSyncService
{
    internal class Program
    {
        private static void Main()
        {
            LogFile.StartLog();
            var requestStartDateTime = DateTime.Now;
            try
            {
                var domainWorkspace = TogglApiWrapper.GetWorkspaces()
                    .First(x => x.Name == DomainName);

                var domainProject = TogglApiWrapper.GetProjects(domainWorkspace)
                    .First(x => x.Name == DomainProjectName);

                var todaysEntries = TogglApiWrapper.GetTimeEntriesAfter(DateTime.Today)
                    .Where(entry => entry.WorkspaceId == domainWorkspace.Id
                                    && entry.ProjectId == domainProject.Id)
                    .ToList();

                if (todaysEntries.Any())
                {
                    var entriesBeforeRequestTime = todaysEntries
                        .Where(x => x.StartDate.AddSeconds(x.Duration) <= requestStartDateTime)
                        .ToList();

                    if (entriesBeforeRequestTime.Any())
                    {
                        var latestEntry = entriesBeforeRequestTime
                           .OrderByDescending(x => x.StartDate.AddSeconds(x.Duration))
                           .First();
                        latestEntry.CreatedWith = ClientName;
                        latestEntry.StopDate = requestStartDateTime;

                        TogglApiWrapper.CreateOrUpdateTimeEntry(latestEntry, Operation.Update);
                        LogFile.AddTimeEntryOperationLogMessage(domainProject, domainWorkspace, Operation.Update);
                    }
                    else
                    {
                        LogFile.AddLogMessage(LogDispensableDescription);
                    }
                }
                else
                {
                    var timeEntry = new TimeEntry()
                    {
                        WorkspaceId = domainWorkspace.Id,
                        ProjectId = domainProject.Id,
                        StartDate = requestStartDateTime,
                        Duration = DefaultTimeEntryDurationMinutes * 60,
                        CreatedWith = ClientName
                    };
                    TogglApiWrapper.CreateOrUpdateTimeEntry(timeEntry, Operation.Create);
                    LogFile.AddTimeEntryOperationLogMessage(domainProject, domainWorkspace, Operation.Create);
                }
            }
            catch (Exception ex)
            {
                LogFile.AddLogErrorMessage(ex.Message);
            }
            finally
            {
                LogFile.TerminateLog();
            }
        }
    }
}
