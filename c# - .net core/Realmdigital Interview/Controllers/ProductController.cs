using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using RealmDigital.Interview.Service.DTO;
using RealmDigital.Service;

namespace Realmdigital_Interview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {             
        private IInterviewService _interviewService { get; set; }

        public ProductController(IInterviewService interviewService)
        {            
            _interviewService = interviewService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "welcome" };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(string id)
        {
            try
            {
                List<ApiResponseProduct> reponseObject = _interviewService.GetResponseProductsById(id);

                var product = new List<object>();
                for (int i = 0; i < reponseObject.Count; i++)
                {
                    var prices = new List<object>();
                    for (int j = 0; j < reponseObject[i].PriceRecords.Count; j++)
                    {
                        if (reponseObject[i].PriceRecords[j].CurrencyCode == "ZAR")
                        {
                            prices.Add(new
                            {
                                Price = reponseObject[i].PriceRecords[j].SellingPrice,
                                Currency = reponseObject[i].PriceRecords[j].CurrencyCode
                            });
                        }
                    }
                    product.Add(new
                    {
                        Id = reponseObject[i].BarCode,
                        Name = reponseObject[i].ItemName,
                        Prices = prices
                    });
                }

                return await Task.FromResult(new JsonResult(new
                {
                    acknowledged = true,
                    result = product
                }));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(BadRequest(new
                {
                    acknowledged = false,
                    Exception = ex
                }));
            }
        }

        [HttpGet("search/{productName}")]
        public async Task<ActionResult> GetProductsByName(string productName)
        {
            try
            {
                List<ApiResponseProduct> reponseObject = _interviewService.GetResponseProductsByProductName(productName);

                var product = new List<object>();
                for (int i = 0; i < reponseObject.Count; i++)
                {
                    var prices = new List<object>();
                    for (int j = 0; j < reponseObject[i].PriceRecords.Count; j++)
                    {
                        if (reponseObject[i].PriceRecords[j].CurrencyCode == "ZAR")
                        {
                            prices.Add(new
                            {
                                Price = reponseObject[i].PriceRecords[j].SellingPrice,
                                Currency = reponseObject[i].PriceRecords[j].CurrencyCode
                            });
                        }
                    }
                    product.Add(new
                    {
                        Id = reponseObject[i].BarCode,
                        Name = reponseObject[i].ItemName,
                        Prices = prices
                    });
                }

                return await Task.FromResult(new JsonResult(new
                {
                    acknowledged = true,
                    result = product
                }));

            }
            catch (Exception ex)
            {
                return await Task.FromResult(BadRequest(new
                {
                    acknowledged = false,
                    Exception = ex
                }));
            }
        }
    }
}