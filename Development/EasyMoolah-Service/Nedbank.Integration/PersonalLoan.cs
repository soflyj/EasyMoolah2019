﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EasyMoolah.Model;
using Newtonsoft.Json;

namespace Nedbank.Integration
{
    public class PersonalLoan
    {
        public Result AcceptPersonalLoan(EasyMoolah.Model.NedbankAPI.PersonLoanRequest.RootObject personalLoanRequest, string lightToken)
        {
            Result result = new EasyMoolah.Model.Result();
            EasyMoolah.Model.Logs.ApiLog apiLog = new EasyMoolah.Model.Logs.ApiLog();
            string client_id = System.Configuration.ConfigurationSettings.AppSettings["client_id"];
            string client_secret = System.Configuration.ConfigurationSettings.AppSettings["client_secret"];
            string apiUrl = $"	https://api.nedbank.co.za/apimarket/sandbox/open-banking/v1/personal-loan";

            result.Input = Newtonsoft.Json.JsonConvert.SerializeObject(personalLoanRequest);

            apiLog.ApplicationKey = 0; // From FE
            apiLog.ApiToken = lightToken; // If a token is used
            apiLog.Method = "pen-banking/v1/personal-loan";
            apiLog.Http = "Post";
            apiLog.Endpoint = apiUrl;
            apiLog.Request = ""; // TODO: Add json
            apiLog.StartDateTime = DateTime.Now;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(apiUrl);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", lightToken);
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("x-fapi-financial-id", "OB/2017/001");
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("x-ibm-client-id", client_id);
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("x-nb-tpp-id", client_id);
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("x-ibm-client-secret", client_secret);

                    var JsonBody = JsonConvert.SerializeObject(personalLoanRequest);                    
                    var content = new StringContent(JsonBody.ToString(), Encoding.UTF8, "application/json");
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var asyncResult = httpClient.PostAsync(apiUrl, content).Result;

                    //apiLog
                    apiLog.Request = Newtonsoft.Json.JsonConvert.SerializeObject(JsonBody);
                    apiLog.Response = asyncResult.Content.ReadAsStringAsync().Result;
                    apiLog.EndDateTime = DateTime.Now;

                    //result
                    result.result = ResultEnum.OK;
                    result.Output = asyncResult.Content.ReadAsStringAsync().Result;
                    result.ApiLog = apiLog;
                }
            }
            catch (Exception ex)
            {
                result.result = ResultEnum.API;
                result.Error = ex.InnerException.ToString();
                result.Error = "Error occurred in Nedbank API - " + apiLog.Method;
            }

            return result;
        }
    }
}
