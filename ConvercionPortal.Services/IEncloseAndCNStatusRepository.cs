using ConvercionPortal.Models;

namespace ConvercionPortal.Services
{
    public interface IEncloseAndCNStatusRepository
    {
        IEnumerable<EncloseAndCNStatus> GetAll(Dictionary<string, string> filter);

        EncloseAndCNStatus? GetById(int id, int ownerId);

        EncloseAndCNStatus? Update(EncloseAndCNStatus encloseAndCNStatus);

        EncloseAndCNStatus? Delete(int id, int ownerId);

        EncloseAndCNStatus? Insert(EncloseAndCNStatus encloseAndCNStatus);

    }
}
