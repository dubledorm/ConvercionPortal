﻿using Data.Models.Cainiao;


namespace Data.Stores.Cainiao
{
    public interface IEncloseEventsStore
    {
        Task<EncloseEvent> CreateAsync(EncloseEvent encloseEvent);
        Task<EncloseEvent?> GetAsync(int encloseId, int encloseOwnerID);
        Task<List<EncloseEvent>> GetAsync();
        Task UpdateAsync(EncloseEvent encloseEvent);
        Task<EncloseEvent> Delete(int encloseId, int encloseOwnerID);


        void AddScopeRelation(string ScopeName, Func<string> PropertyGetter);
    }
}
