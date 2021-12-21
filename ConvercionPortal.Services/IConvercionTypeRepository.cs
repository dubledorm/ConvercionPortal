using ConvercionPortal.Models;

namespace ConvercionPortal.Services
{
    public interface IConvercionTypeRepository
    {
        IEnumerable<ConvercionType> GetAll(Dictionary<string, string> filter);

        ConvercionType? GetById(int id);

        ConvercionType? Update(ConvercionType convercionType);

        ConvercionType? Delete(int id);

        ConvercionType? Insert(ConvercionType convercionType);
    }
}
