﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using EasyMoolah.Model;
using EasyMoolah.Repository;
using EasyMoolah.Model.Fincheck;
using Newtonsoft.Json;

namespace Fincheck.Integration
{
    public class Intent : Base
    {

        private static EasyMoolah.Model.Result result = new EasyMoolah.Model.Result();
        public static ApiLog apiLog = new ApiLog();
        private static string JsonBody = "";
        private static string fincheckAPI = "";
        private static string apiUrl = "";

        public static EasyMoolah.Model.Result GetIntentById(IntentRequest _intentRequest)
        {
            apiUrl = System.Configuration.ConfigurationSettings.AppSettings["Fincheck"].ToString() + "intent";
            fincheckAPI = System.Configuration.ConfigurationSettings.AppSettings["FincheckAPI"].ToString();

            //result
            result.Input = _intentRequest.id.ToString();
            //apiLog
            apiLog.ApplicationKey = _intentRequest.applicationKey;
            apiLog.ApiToken = fincheckAPI; 
            apiLog.Method = "intent";
            apiLog.Http = "Get";
            apiLog.Endpoint = apiUrl;
            apiLog.Request = _intentRequest.id.ToString();
            apiLog.StartDateTime = DateTime.Now;

            if (_intentRequest.id != 0 && _intentRequest.id != null)
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri(apiUrl);
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        httpClient.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", fincheckAPI);
                        httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

                        var asyncResult = httpClient.GetAsync(apiUrl).Result;
                        //result
                        result.result = ResultEnum.OK;
                        result.Output = asyncResult.Content.ReadAsStringAsync().Result;
                        result.result = result.Output;
                        //apiLog
                        apiLog.Response = result.output;
                        apiLog.EndDateTime = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    result.result = ResultEnum.API;
                    result.Error = ex.InnerException.ToString();
                    result.ErrorFriendly = "Error 101 occurred in Fincheck API - api/v1/intent/" + _intentRequest.id;
                }
            }
            else
            {
                result.result = 201;
                result.error = "parameter is null";
                result.errorFriendly = "Error 201 occurred in Fincheck API - api/v1/intent/" + _intentRequest.id;
            }

            return result;
        }

        public static EasyMoolah.Model.Result GetIntent()
        {
            apiUrl = System.Configuration.ConfigurationSettings.AppSettings["Fincheck"].ToString() + "intent";
            fincheckAPI = System.Configuration.ConfigurationSettings.AppSettings["FincheckAPI"].ToString();

            result.input = "";

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(apiUrl);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", fincheckAPI);
                    httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

                    var asyncResult = httpClient.GetAsync(apiUrl).Result;
                    var jsonBody = asyncResult.Content.ReadAsStringAsync().Result;
                    EasyMoolah.Model.Fincheck.Intent intentRepsonse = JsonConvert.DeserializeObject<EasyMoolah.Model.Fincheck.Intent>(jsonBody);

                    result.resultCode = 0;                    
                    result.output = asyncResult.Content.ReadAsStringAsync().Result;
                    result.result = result.output;
                }
            }
            catch (Exception ex)
            {
                result.resultCode = 1;
                result.error = ex.InnerException.ToString();
                result.errorFriendly = "Error 101 occurred in Fincheck API - api/v1/intent/";
            }

            return result;
        }
    }
}
