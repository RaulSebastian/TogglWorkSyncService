using Newtonsoft.Json;

namespace TogglWorkSyncService.ApiJObjects
{
    public class Project
    {
        [JsonProperty("id")]
        public long Id;
        [JsonProperty("name")]
        public string Name;
    }
}