using System;
using System.Collections.Generic;
using System.Text;

namespace RealmDigital.Interview.Service.DTO
{
    public class ApiResponseProduct
    {
        public string BarCode { get; set; }
        public string ItemName { get; set; }
        public List<ApiResponsePrice> PriceRecords { get; set; }
    }
}
