using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RealmDigital.Interview.Service.DTO;

namespace RealmDigital.Service
{
    public class InterviewService : IInterviewService
    {      
        private IConfiguration _configuration { get; }

        public InterviewService(IConfiguration configuration)
        {            
            _configuration = configuration;
        }

        public string GetApiResponseProducts(string productid)
        {
            string response = "";

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                response = client.UploadString("http://192.168.0.241/eanlist?type=Web", "POST", "{ \"id\": \"" + productid + "\" }");
            }

            return response;
        }

        public string GetApiResponseProductsByProductName(string productname)
        {
            string response = "";

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                response = client.UploadString("http://192.168.0.241/eanlist?type=Web", "POST", "{ \"names\": \"" + productname + "\" }");
            }

            return response;
        }

        public List<ApiResponseProduct> GetResponseProductsById(string id)
        {
            string parth = $@"{_configuration["GeneralSettings:ItemUploadDirectory"]}";

            string response = System.IO.File.ReadAllText(parth + "many-products.json");

            List<ApiResponseProduct> reponseObject = JsonConvert.DeserializeObject<List<ApiResponseProduct>>(response);

            reponseObject = reponseObject.Where(i => i.BarCode == id).ToList();

            return reponseObject;
        }


        public List<ApiResponseProduct> GetResponseProductsByProductName(string productname)
        {
            string parth = $@"{_configuration["GeneralSettings:ItemUploadDirectory"]}";

            string response = System.IO.File.ReadAllText(parth + "many-products.json");

            List<ApiResponseProduct> reponseObject = JsonConvert.DeserializeObject<List<ApiResponseProduct>>(response);

            reponseObject = reponseObject.Where(i => i.ItemName == productname).ToList();

            return reponseObject;
        }
    }
}
