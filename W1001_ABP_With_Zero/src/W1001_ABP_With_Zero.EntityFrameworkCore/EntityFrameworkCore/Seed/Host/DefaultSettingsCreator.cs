using System.Linq;
using Abp.Configuration;
using Abp.Localization;
using Abp.Net.Mail;

namespace W1001_ABP_With_Zero.EntityFrameworkCore.Seed.Host
{
    public class DefaultSettingsCreator
    {
        private readonly W1001_ABP_With_ZeroDbContext _context;

        public DefaultSettingsCreator(W1001_ABP_With_ZeroDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            //Emailing
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "admin@mydomain.com");
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "mydomain.com mailer");

            //Languages
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "en");
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}