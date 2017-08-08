namespace W1001_ABP_With_Zero.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly W1001_ABP_With_ZeroDbContext _context;

        public InitialHostDbBuilder(W1001_ABP_With_ZeroDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
