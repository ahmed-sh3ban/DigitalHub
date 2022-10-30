using LicenceTask.DTO;
using LicenceTask.Repository;
using LicenceTask.Services.Abstract;
using System.Security.Cryptography;

namespace LicenceTask.Domain
{
    public interface ISubscriptionLicenseService
    {
       public UserLicenseDTO GenerateLicense();
       public bool ValidateLicense(string signature, string data, IFormFile pubKeyFile);
    }
    public class SubscriptionLicenseService : ISubscriptionLicenseService       
    {
        private readonly ISigning _signing;
        //private readonly ISubscriptionLicenseRepository _subscriptionLicenseRepository;
        public SubscriptionLicenseService(ISubscriptionLicenseRepository subscriptionLicenseRepository, ISigning signing)
        {
          //  this._subscriptionLicenseRepository = subscriptionLicenseRepository;
            this._signing = signing;
        }
        
        public UserLicenseDTO GenerateLicense()
        {
            string newLicense = Guid.NewGuid().ToString();
            var result = _signing.Sign(newLicense);
            return result;
        }

        public bool ValidateLicense(string signature, string data,IFormFile pubKeyFile)
        {
            var result = _signing.Validate(signature,data,pubKeyFile);
            return result;
        }


    }
}
