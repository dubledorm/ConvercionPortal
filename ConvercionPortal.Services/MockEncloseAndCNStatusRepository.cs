using ConvercionPortal.Models;
using Microsoft.Extensions.Logging;

namespace ConvercionPortal.Services
{
    public class MockEncloseAndCNStatusRepository : ScopedRepository<EncloseAndCNStatus>,  IEncloseAndCNStatusRepository
    {
        private List<EncloseAndCNStatus> _encloseAndCNStatuses;

        public MockEncloseAndCNStatusRepository(ILogger<MongoEncloseAndCNStatusRepository> logger): base(logger)
        {
            _encloseAndCNStatuses = new List<EncloseAndCNStatus>()
             {
                new EncloseAndCNStatus()
                  { EncloseId = 1, EncloseOwnerId = 1, TroubleFlag = false, FinishedFlag = true, 
                    StatusHistory = new List<CainiaoStatusEvent>() { } 
                },
                new EncloseAndCNStatus()
                  { EncloseId = 2, EncloseOwnerId = 1, TroubleFlag = false, FinishedFlag = false,
                    StatusHistory = new List<CainiaoStatusEvent>() { }
                },
                new EncloseAndCNStatus()
                  { EncloseId = 3, EncloseOwnerId = 1, TroubleFlag = true, FinishedFlag = false,
                    StatusHistory = new List<CainiaoStatusEvent>() { }
                }
            };
        }

        public void AddScopeRalation(string ScopeName, Func<string> PropertyGetter)
        {
            throw new NotImplementedException();
        }

        public EncloseAndCNStatus? Delete(int id, int ownerId)
        {
            EncloseAndCNStatus? itemForDelete = GetById(id, ownerId);

            if (itemForDelete == null)
                return null;


            _encloseAndCNStatuses.Remove(itemForDelete);
            return itemForDelete;
        }

        public IEnumerable<EncloseAndCNStatus> GetAll()
        {
            //if (filter.Count == 0)
            //    return _encloseAndCNStatuses;

            //MockConvercionTypeFiler mockConvercionTypeFiler = new(filter);

            //var result = from encloseAndCNStatus in _encloseAndCNStatuses
            //             where mockConvercionTypeFiler.Compare(encloseAndCNStatus) //фильтрация по критерию
            //             select encloseAndCNStatus; // выбираем объект
            //return new Task<IEnumerable<EncloseAndCNStatus>>(() => _encloseAndCNStatuses);
            return _encloseAndCNStatuses;
        }

        public EncloseAndCNStatus? GetById(int id, int ownerId)
        {
            return _encloseAndCNStatuses.FirstOrDefault(encloseAndCNStatus =>
            {
                return encloseAndCNStatus.EncloseId == id &&
                encloseAndCNStatus.EncloseOwnerId == ownerId;
            });
        }

        public EncloseAndCNStatus? Insert(EncloseAndCNStatus encloseAndCNStatus)
        {
            int maxId = _encloseAndCNStatuses.Max<EncloseAndCNStatus>(itemEncloseAndCNStatus => itemEncloseAndCNStatus.EncloseId);

            encloseAndCNStatus.EncloseId = maxId + 1;
            encloseAndCNStatus.EncloseOwnerId = 1;
            _encloseAndCNStatuses.Add(encloseAndCNStatus);
            return encloseAndCNStatus;
        }

       

        public EncloseAndCNStatus? Update(EncloseAndCNStatus encloseAndCNStatus)
        {
            EncloseAndCNStatus? encloseAndCNStatusForUpdate = GetById(encloseAndCNStatus.EncloseId, 
                encloseAndCNStatus.EncloseOwnerId);

            if (encloseAndCNStatusForUpdate == null)
                return null;

            // Копирование
            encloseAndCNStatusForUpdate.TroubleFlag = encloseAndCNStatus.TroubleFlag;
            encloseAndCNStatusForUpdate.FinishedFlag = encloseAndCNStatus.FinishedFlag;

            return encloseAndCNStatusForUpdate;
        }
     }
}
