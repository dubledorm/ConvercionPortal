using Data.Models.Cainiao;


namespace Data.Stores.Cainiao
{
    public interface IEncloseEventsStore
    {
        Task<EncloseEvent> CreateAsync(EncloseEvent encloseEvent);
        Task<EncloseEvent?> GetByIdAsync(int encloseId, int encloseOwnerID);
        Task<List<EncloseEvent>> GetAsync(int skip = 0);
        Task<long> CountAsync();
        Task UpdateAsync(EncloseEvent encloseEvent);
        Task<EncloseEvent> Delete(int encloseId, int encloseOwnerID);


        void AddScopeRelation(string ScopeName, Func<string> PropertyGetter);
    }
}
