using System.Threading.Tasks;
using TestSequence.Api.Model;
using TestSequence.Api.Model.Gateway;

namespace TestSequence.Api.Data.Repository
{
    public class CreditRepository : ICreditRepository
    {
        private readonly CreditsContext _creditsContext;

        public CreditRepository(CreditsContext creditsContext)
        {
            _creditsContext = creditsContext;
        }

        public async Task AddAsync(Credit credit)
        {
            await _creditsContext.AddAsync(credit);

            await _creditsContext.SaveChangesAsync();
        }

        public void Add(Credit credit)
        {
            _creditsContext.Add(credit);

            _creditsContext.SaveChanges();
        }
    }
}