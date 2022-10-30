using Microsoft.AspNetCore.Mvc;

namespace LicenceTask.DTO
{
    public class UserLicenseDTO
    {
        public string Signature { get; set; }
        public string pubKeyFilePath { get; set; }
        public string License { get; set; }

    }
}
