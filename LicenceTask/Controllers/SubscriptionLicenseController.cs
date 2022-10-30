using LicenceTask.Domain;
using LicenceTask.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LicenceTask.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SubscriptionLicenseController : Controller
    {
        private readonly ISubscriptionLicenseService _subscriptionLicenseService;
        
        public SubscriptionLicenseController(ISubscriptionLicenseService subscriptionLicenseService)
        {
            _subscriptionLicenseService = subscriptionLicenseService;
        }
        [HttpPost("GenerateLicence")]
        public  IActionResult GenerateLicence( )
        {
           
            var result =  _subscriptionLicenseService.GenerateLicense();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult ValidateLicense(string signature, string license , IFormFile file)
        {
            var result = _subscriptionLicenseService.ValidateLicense(signature,license ,file);

            return Ok(result);
        }
    }
}
