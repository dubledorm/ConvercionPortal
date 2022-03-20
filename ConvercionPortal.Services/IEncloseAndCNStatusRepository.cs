using ConvercionPortal.Models;

namespace ConvercionPortal.Services
{
    public interface IEncloseAndCNStatusRepository
    {
        IEnumerable<EncloseAndCNStatus> GetAll();

        EncloseAndCNStatus? GetById(int id, int ownerId);
        EncloseAndCNStatus? Update(EncloseAndCNStatus encloseAndCNStatus);

        EncloseAndCNStatus? Delete(int id, int ownerId);

        EncloseAndCNStatus? Insert(EncloseAndCNStatus encloseAndCNStatus);

          void AddScopeRalation(string ScopeName, Func<string> PropertyGetter);

    }
}
