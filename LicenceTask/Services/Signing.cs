using LicenceTask.Model;
using LicenceTask.Services.Abstract;
using Microsoft.Extensions.Options;
using PemUtils;
using System.Security.Cryptography;
using System.Text;
using LicenceTask.DTO;

namespace LicenceTask.Services
{
    public class Signing : ISigning
    {
        private IOptions<AesKeys> _aes;
        private IWebHostEnvironment appEnvironment;

        public Signing(IOptions<AesKeys> aes,IWebHostEnvironment appEnvironment)
        {
            this._aes = aes;
            this.appEnvironment = appEnvironment;
        }
        private void SaveKeysInFiles(RSA rsa, string fileName)
        {
            if (string.IsNullOrWhiteSpace(appEnvironment.WebRootPath))
            {
                appEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            string pubPath = Path.Combine(appEnvironment.WebRootPath, $"pu_{fileName}.pem");
            string prPath = Path.Combine(appEnvironment.WebRootPath, $"pr_{fileName}.pem");

            using (var fs = File.Create(prPath))
            {
                using (var pem = new PemWriter(fs))
                {
                    pem.WritePrivateKey(rsa);
                }
            }
            using (var fs = File.Create(pubPath))
            {
                using (var pem = new PemWriter(fs))
                {
                    pem.WritePublicKey(rsa);
                }
            }
        }

        public UserLicenseDTO Sign(string userLicense)
        {
            var rsa = RSA.Create(2048);
            var plainTextBytes = Encoding.Unicode.GetBytes(userLicense);
                byte[] signature = rsa.SignData(plainTextBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                var signatureText = Convert.ToBase64String(signature);
                SaveKeysInFiles( rsa,  userLicense);
                return new UserLicenseDTO{pubKeyFilePath =  Path.Combine(appEnvironment.WebRootPath, $"pu_{userLicense}.pem").ToString() ,Signature= signatureText,License = userLicense};

            
        }
        public bool Validate(string signature, string data, IFormFile pubKeyFile)
        {
            var rsa = ReadPublicKeyFromStream(pubKeyFile);
          
                byte[] dataInBytes = Encoding.Unicode.GetBytes(data);
                byte[] signatureInBytes = Convert.FromBase64String(signature);
                bool isValid = rsa.VerifyData(dataInBytes,
                    signatureInBytes,
                    HashAlgorithmName.SHA256,
                    RSASignaturePadding.Pkcs1);
                return isValid;
            
        }

        private RSA ReadPublicKeyFromStream(IFormFile file)
        {
            var rsa = RSA.Create();
            var pem = new PemReader(file.OpenReadStream());
            var rsaParameters = pem.ReadRsaKey();
            rsa.ImportParameters(rsaParameters);
                    
            return rsa;
        }
     

        
    }
}
