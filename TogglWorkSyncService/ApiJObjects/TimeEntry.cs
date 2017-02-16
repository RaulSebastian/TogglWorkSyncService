using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TogglWorkSyncService.ApiJObjects
{
    public class TimeEntry
    {
        [JsonProperty("id")]
        public long Id;
        [JsonProperty("wid")]
        public long WorkspaceId;
        [JsonProperty("pid")]
        public long ProjectId;
        [JsonProperty("start")]
        public DateTime StartDate;
        [JsonProperty("stop")]
        public DateTime StopDate;
        [JsonProperty("duration")]
        public long Duration;
        [JsonProperty("description")]
        public string Description;
        [JsonProperty("created_with")]
        public string CreatedWith;
        [JsonProperty("tags")]
        public List<string> Tags;
    }
}