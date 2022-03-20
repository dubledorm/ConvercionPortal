using ConvercionPortal.Models;

namespace ConvercionPortal.Services
{
    public interface ICnEncloseStatusRepository
    {
        IEnumerable<CnEncloseStatus> GetAll();

        CnEncloseStatus? GetById(int id, int ownerId);
        CnEncloseStatus? Update(CnEncloseStatus encloseAndCNStatus);

        CnEncloseStatus? Delete(int id, int ownerId);

        CnEncloseStatus? Insert(CnEncloseStatus encloseAndCNStatus);

          void AddScopeRalation(string ScopeName, Func<string> PropertyGetter);

    }
}
