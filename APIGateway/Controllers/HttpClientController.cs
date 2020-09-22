using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace APIGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpClientController : ControllerBase
    {
        IConfiguration _iconfiguration;
        public HttpClientController(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }
        [HttpGet]
        public dynamic Get(string service , int param )
        {
            try
            {
                var ClientURL = string.Empty;
                string queryString = string.Empty;
                using (var client = new HttpClient())
                {
                    
                    if(service =="User")
                    {
                       ClientURL = _iconfiguration["UserService:GetAllUser"];
                      
                    }      
                    //client.DefaultRequestHeaders.Accept.Clear();
                    //client.DefaultRequestHeaders.Add("REFSSessionID", refSessionId);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    client.BaseAddress = new Uri(_iconfiguration["ServerUrl"]);
                    var response = client.GetAsync(ClientURL).Result;

                    var result = response.Content.ReadAsStringAsync().Result;
                    dynamic dynamicObject = JsonConvert.DeserializeObject<dynamic>(result);
                    return dynamicObject;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
