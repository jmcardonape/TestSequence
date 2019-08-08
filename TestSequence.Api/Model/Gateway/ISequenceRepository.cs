using System.Threading.Tasks;

namespace TestSequence.Api.Model.Gateway
{
    public interface ISequenceRepository
    {
        long GetNextSequenceEF(string storeId, string type);

        long GetNextSequenceSQL(string storeId, string type);
    }
}