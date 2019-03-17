﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using EasyMoolah.Model;
using EasyMoolah.Model.Database.Fincheck;
using EasyMoolah.Model.Fincheck;
using Newtonsoft.Json;

namespace Fincheck.Integration
{
    public class Lead : Base
    {
        private Result result = new Result();
        private Apilog apiLog = new Apilog();
        private string JsonBody = "";

        public Result CreateLead(LeadRequest leadRequest)
        {
            var apiUrl = "https://engine.fincheck.co.za/api/v1/lead/";

            //result
            result.input = "";
            //apiLog
            apiLog.SessionId = leadRequest.sessionId;
            apiLog.Token = "6aezFnDAcPO5vKoma8eW";
            apiLog.Method = "lead";
            apiLog.Http = "Post";
            apiLog.Endpoint = apiUrl;
            apiLog.Request = "";
            apiLog.StartTimeStamp = DateTime.Now;

            if (leadRequest != null)
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri(apiUrl);
                        //Headers
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        httpClient.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", "6aezFnDAcPO5vKoma8eW");
                        httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

                        JsonBody = JsonConvert.SerializeObject(leadRequest);
                        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonBody);
                        var content = new FormUrlEncodedContent(dictionary);

                        var asyncResult = httpClient.PostAsync(apiUrl, content).Result;

                        //result
                        result.resultCode = 0;
                        result.output = asyncResult.Content.ReadAsStringAsync().Result;
                        result.result = result.output;
                        //apiLog
                        apiLog.Response = result.output;
                        apiLog.EndTimeStamp = DateTime.Now;
                        AddApiLog(apiLog);
                    }
                }
                catch (Exception ex)
                {
                    result.resultCode = 101;
                    result.error = ex.InnerException.ToString();
                    result.errorFriendly = "Error 101 occurred in Fincheck API - api/v1/lead/";
                }
            }
            else
            {
                result.resultCode = 201;
                result.error = "parameter is null";
                result.errorFriendly = "Error 201 occurred in Fincheck API - api/v1/lead/";
            }

            return result;
        }
    }
}