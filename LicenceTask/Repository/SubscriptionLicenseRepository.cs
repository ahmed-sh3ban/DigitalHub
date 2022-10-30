using System.Xml.Linq;
using LicenceTask.model;
using LicenceTask.Model;

namespace LicenceTask.Repository
{
    public interface ISubscriptionLicenseRepository
    {
        public Task AddSubscription(UserLicense license);
        public Task UpdateSubscription();
        string? GetPublicKey(string license);
    }
    public class SubscriptionLicenseRepository : ISubscriptionLicenseRepository
    {
        private readonly DataContext _context;
        public SubscriptionLicenseRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddSubscription(UserLicense license)
        {
            await _context.AddAsync(license);
        }

        public Task UpdateSubscription()
        {
            throw new NotImplementedException();
        }

        public string? GetPublicKey(string license)
        {
            var pubKey = _context.UserLicences.Where(att => att.LicenseData == license).Select(att=>att.PublicKeyPath).FirstOrDefault();
            return pubKey;
        }
    }
}
