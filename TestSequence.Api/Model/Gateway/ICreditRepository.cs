using System.Threading.Tasks;
using TestSequence.Api.Data;

namespace TestSequence.Api.Model.Gateway
{
    public interface ICreditRepository
    {
        void Add(Credit credit);
    }
}