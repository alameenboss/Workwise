﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Workwise.ServiceAgent
{
    public class HttpClientWrapper : IHttpClient
    {
        public T Get<T>(string requestUri)
        {
            T obj = default(T);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/");
                //HTTP GET
                var responseTask = client.GetAsync(requestUri);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<T>();
                    readTask.Wait();

                    obj = readTask.Result;
                }
                //else //web api sent error response 
                //{
                //    //log response status here..

                //    students = Enumerable.Empty<StudentViewModel>();

                //    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                //}
            }
            return obj;
        }

        public T PostData<T>(string requestUri, T value)
        {
            T obj = default(T);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/");
                //HTTP GET
                //var responseTask = await client.PostAsJsonAsync(requestUri,value);
                //responseTask.Wait();

                var response = client.PostAsync(requestUri, new StringContent(
                new JavaScriptSerializer().Serialize(value), Encoding.UTF8, "application/json")).Result;

                //var result = responseTask.Content;
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<T>();
                    readTask.Wait();

                    obj = readTask.Result;
                }
                //else //web api sent error response 
                //{
                //    //log response status here..

                //    students = Enumerable.Empty<StudentViewModel>();

                //    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                //}
            }
            return obj;
        }
        
        public async Task<U> PostDataAsync<T, U>(string requestUri, T value)
        {
            U result = default(U);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/");
                //HTTP GET
                var responseTask = await client.PostAsJsonAsync(requestUri, value);
                // responseTask.Wait();

                //var result = responseTask.Content;
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = await responseTask.Content.ReadAsAsync<U>();
                    //readTask.Wait();

                    result = readTask;
                }
                //else //web api sent error response 
                //{
                //    //log response status here..

                //    students = Enumerable.Empty<StudentViewModel>();

                //    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                //}
            }
            return result;
        }
        
    }
}
