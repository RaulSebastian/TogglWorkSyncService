using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using RestSharp;
using TogglWorkSyncService.ApiJObjects;
using static TogglWorkSyncService.Constants;
using static TogglWorkSyncService.RuntimeDefaults;

namespace TogglWorkSyncService.Helpers
{
    public static class TogglApiWrapper
    {
        private static void AuthorizationHeader(IRestRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            request.AddHeader("authorization", ApiAuthorizationKey);
        }

        private static RestRequest BasicGetRequest()
        {
            var request = new RestRequest(Method.GET);
            AuthorizationHeader(request);
            request.AddHeader("cache-control", "no-cache");
            return request;
        }

        private static RestRequest BasicPostRequest()
        {
            var request = new RestRequest(Method.POST);
            AuthorizationHeader(request);
            request.AddHeader("cache-control", "no-cache");
            request.AddQueryParameter("Content-Type", "application/json");
            return request;
        }

        private static RestRequest BasicPutRequest()
        {
            var request = new RestRequest(Method.PUT);
            AuthorizationHeader(request);
            request.AddHeader("cache-control", "no-cache");
            request.AddQueryParameter("Content-Type", "application/json");
            return request;
        }

        public static IEnumerable<Workspace> GetWorkspaces()
        {
            var client = new RestClient(ApiWorkspacesEndpoint);
            var response = client.Execute(BasicGetRequest());
            if (response.StatusCode != HttpStatusCode.OK)
                throw new HttpRequestException($"{response.StatusDescription}");
            return JsonConvert.DeserializeObject<IEnumerable<Workspace>>(response.Content);
        }

        public static IEnumerable<Project> GetProjects(Workspace workspace)
        {
            var client = new RestClient(ApiProjectsEndpoint(workspace));
            var response = client.Execute(BasicGetRequest());
            if (response.StatusCode != HttpStatusCode.OK)
                throw new HttpRequestException($"{response.StatusDescription}");
            return JsonConvert.DeserializeObject<IEnumerable<Project>>(response.Content);
        }

        public static IEnumerable<TimeEntry> GetTimeEntriesAfter(DateTime startDate)
        {
            var client = new RestClient(ApiTimeEntriesEndpoint);
            var request = BasicGetRequest();
            request.AddQueryParameter("start_date", Iso8601DateFormat.Convert(startDate));
            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new HttpRequestException($"{response.StatusDescription}");
            return JsonConvert.DeserializeObject<IEnumerable<TimeEntry>>(response.Content);
        }

        public static void CreateOrUpdateTimeEntry(TimeEntry timeEntry, Operation operation)
        {
            string endpoint;
            RestRequest request;

            switch (operation)
            {
                case Operation.Create:
                        endpoint = ApiTimeEntriesEndpoint;
                        request = BasicPostRequest();
                    break;
                case Operation.Update:
                    endpoint = ApiSingleTimeEntryEndpoint(timeEntry);
                    request = BasicPutRequest();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, ErrorInvalidOperationDescription);
            }

            var client = new RestClient(endpoint);
            request.AddParameter("application/json"
                , "{ \"time_entry\": " + JsonConvert.SerializeObject(timeEntry) + " }"
                , ParameterType.RequestBody);
            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new HttpRequestException($"{response.StatusDescription}. {response.Content}");
        }
    }
}