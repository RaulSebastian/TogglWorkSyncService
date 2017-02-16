using Newtonsoft.Json;

namespace TogglWorkSyncService.ApiJObjects
{
    public class Workspace
    {
        [JsonProperty("id")]
        public long Id;
        [JsonProperty("name")]
        public string Name;
    }
}