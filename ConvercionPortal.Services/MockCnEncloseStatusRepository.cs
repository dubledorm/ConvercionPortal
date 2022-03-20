using ConvercionPortal.Models;
using Microsoft.Extensions.Logging;

namespace ConvercionPortal.Services
{
    public class MockCnEncloseStatusRepository : ScopedRepository<CnEncloseStatus>,  ICnEncloseStatusRepository
    {
        private List<CnEncloseStatus> _encloseAndCNStatuses;

        public MockCnEncloseStatusRepository(ILogger<MongoCnEncloseStatusRepository> logger): base(logger)
        {
            _encloseAndCNStatuses = new List<CnEncloseStatus>()
             {
                new CnEncloseStatus()
                  { EncloseId = 1, EncloseOwnerId = 1, TroubleFlag = false, FinishedFlag = true, 
                    StatusHistory = new List<CnStatusEvent>() { } 
                },
                new CnEncloseStatus()
                  { EncloseId = 2, EncloseOwnerId = 1, TroubleFlag = false, FinishedFlag = false,
                    StatusHistory = new List<CnStatusEvent>() { }
                },
                new CnEncloseStatus()
                  { EncloseId = 3, EncloseOwnerId = 1, TroubleFlag = true, FinishedFlag = false,
                    StatusHistory = new List<CnStatusEvent>() { }
                }
            };
        }

        public void AddScopeRalation(string ScopeName, Func<string> PropertyGetter)
        {
            throw new NotImplementedException();
        }

        public CnEncloseStatus? Delete(int id, int ownerId)
        {
            CnEncloseStatus? itemForDelete = GetById(id, ownerId);

            if (itemForDelete == null)
                return null;


            _encloseAndCNStatuses.Remove(itemForDelete);
            return itemForDelete;
        }

        public IEnumerable<CnEncloseStatus> GetAll()
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

        public CnEncloseStatus? GetById(int id, int ownerId)
        {
            return _encloseAndCNStatuses.FirstOrDefault(encloseAndCNStatus =>
            {
                return encloseAndCNStatus.EncloseId == id &&
                encloseAndCNStatus.EncloseOwnerId == ownerId;
            });
        }

        public CnEncloseStatus? Insert(CnEncloseStatus encloseAndCNStatus)
        {
            int maxId = _encloseAndCNStatuses.Max<CnEncloseStatus>(itemEncloseAndCNStatus => itemEncloseAndCNStatus.EncloseId);

            encloseAndCNStatus.EncloseId = maxId + 1;
            encloseAndCNStatus.EncloseOwnerId = 1;
            _encloseAndCNStatuses.Add(encloseAndCNStatus);
            return encloseAndCNStatus;
        }

       

        public CnEncloseStatus? Update(CnEncloseStatus encloseAndCNStatus)
        {
            CnEncloseStatus? encloseAndCNStatusForUpdate = GetById(encloseAndCNStatus.EncloseId, 
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
