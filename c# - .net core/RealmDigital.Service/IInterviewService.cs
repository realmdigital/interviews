using RealmDigital.Interview.Service.DTO;
using System;
using System.Collections.Generic;

namespace RealmDigital.Service
{
    public interface IInterviewService
    {
        string GetApiResponseProducts(string productid);

        string GetApiResponseProductsByProductName(string productname);

        List<ApiResponseProduct> GetResponseProductsById(string id);

        List<ApiResponseProduct> GetResponseProductsByProductName(string productname);

    }
}
