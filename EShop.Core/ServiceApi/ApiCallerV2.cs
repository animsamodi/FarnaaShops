using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using EShop.Core.ViewModels;
using Newtonsoft.Json;

namespace EShop.Core.ServiceApi
{
    public class ApiCallerV2 : IServiceCallerV2
    {
        private readonly string _serviceAddress;

        public ApiCallerV2(string serviceAddress)
        {
            _serviceAddress = serviceAddress;
        }
        public ResponseApiCaller Call(HttpMethod httpMethod, KeyValuePair<string, object>[] headers, KeyValuePair<string, object>[] parameters, object contentData)
        {
            try
            {
                ResponseApiCaller responseApiCaller;
                HttpResponseMessage response;

                using (var httpClient = new HttpClient())
                {


                    HttpRequestMessage request = new HttpRequestMessage();
                    request.RequestUri = new Uri(_serviceAddress);
                    if (headers != null && headers.Length != 0)
                    {
                        foreach (var header in headers)
                        {
                            request.Headers.Add(header.Key, header.Value.ToString());

                        }
                    }

                    if (parameters != null && parameters.Length != 0)
                    {

                        var data = new List<KeyValuePair<string, string>>();
                        foreach (var parameter in parameters)
                        {
                            data.Add(new KeyValuePair<string, string>(parameter.Key, parameter.Value.ToString()));

                        }

                        FormUrlEncodedContent content = new FormUrlEncodedContent(data);
                        request.Content = content;


                    }

                    if (contentData != null)
                    {
                        var content = JsonConvert.SerializeObject(contentData);
                        var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
                        request.Content = stringContent;

                    }
                    request.Method = httpMethod;
                    response = httpClient.SendAsync(request).Result;
                }
                var responseDto = response.Content.ReadAsAsync<object>().Result;
                responseApiCaller = new ResponseApiCaller
                {
                    Data = responseDto,
                    IsSuccess = true
                };
                if (!response.IsSuccessStatusCode)
                {
                    responseApiCaller.IsSuccess = false;
                }

                return responseApiCaller;


                 

            }
            catch (Exception ex)
            {
                throw new Exception("اطلاعات یافت نشد");

            }
        }

        private int GetValueIndex(object[] parameters, string value)
        {
            var index = parameters.ToList().FindIndex(e => e.Equals(value));
            return index;
        }
    }
}