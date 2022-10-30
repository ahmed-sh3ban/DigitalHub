using LicenceTask.DTO;

namespace LicenceTask.Services.Abstract
{
    public interface ISigning
    {
        public UserLicenseDTO Sign(string plainText);
        public bool Validate(string signature, string data,IFormFile pubKeyFile);
    }
}
