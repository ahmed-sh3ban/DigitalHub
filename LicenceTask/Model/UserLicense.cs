using System.ComponentModel.DataAnnotations;

namespace LicenceTask.model
{
    public class UserLicense
    {
        [Key]
        public int Id { get; set; }
        public string LicenseData { get; set; }
        public string PublicKeyPath { get; set; }
        public string PrivateKeyPath { get; set; }
        
    }
}
