using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Order;
using Newtonsoft.Json;

namespace EShop.Core.ServiceApi
{
    public class ApiCaller : IServiceCaller
    {
        private readonly string _serviceAddress;

        public ApiCaller(string serviceAddress)
        {
            _serviceAddress = serviceAddress;
        }

        public object Call(HttpMethod httpMethod, params object[] parameters)
        {
            try
            {
                HttpResponseMessage response;

                 using (var httpClient = new HttpClient())
                {
                

                    HttpRequestMessage request = new HttpRequestMessage();
                    request.RequestUri = new Uri(_serviceAddress);
                    if (parameters != null && parameters.Length != 0)
                    {
                        if (parameters[0].Equals("login"))
                        {
                             
                            FormUrlEncodedContent content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
                            {
                                new KeyValuePair<string, string>("username", parameters[1].ToString()),
                                new KeyValuePair<string, string>("password", parameters[2].ToString()),
                                new KeyValuePair<string, string>("grant_type", parameters[3].ToString()),
                            });

                            request.Content = content;

                        }
                        else if (parameters[0].Equals("token"))
                        {
                           
                            FormUrlEncodedContent content = parameters[2] as FormUrlEncodedContent;

                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", parameters[1].ToString());//.Add("Bearer", parameters[1].ToString());
                            request.Content = content;

                         
                        }

                    }


 

                    request.Headers.Host = "80.210.29.217";
                    request.Method = httpMethod;
                    response = httpClient.SendAsync(request).Result;
                }
                if (response.IsSuccessStatusCode)
                {
                    var responseDto = response.Content.ReadAsAsync<object>().Result;


                    return responseDto;
                }
                else
                {
                    var responseDto = response.Content.ReadAsAsync<object>().Result;
                    throw new Exception(responseDto.ToString());

                }

                throw new Exception("خطا در انجام عملیات");

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private int GetValueIndex(object[] parameters, string value)
        {
            var index = parameters.ToList().FindIndex(e => e.Equals(value));
            return index;
        }
    }
}