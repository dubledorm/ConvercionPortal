using ConvercionPortal.Models;

namespace ConvercionPortal.Services
{
    public class MockEncloseAndCNStatusRepository: IEncloseAndCNStatusRepository
    {
        private List<EncloseAndCNStatus> _encloseAndCNStatuses;

        public MockEncloseAndCNStatusRepository()
        {
            _encloseAndCNStatuses = new List<EncloseAndCNStatus>()
             {
                new EncloseAndCNStatus()
                  { Id = 1, OwnerId = 1, TroubleFlag = false, FinishedFlag = true, 
                    Statuses = new List<CainiaoStatusEvent>() { } 
                },
                new EncloseAndCNStatus()
                  { Id = 2, OwnerId = 1, TroubleFlag = false, FinishedFlag = false,
                    Statuses = new List<CainiaoStatusEvent>() { }
                },
                new EncloseAndCNStatus()
                  { Id = 3, OwnerId = 1, TroubleFlag = true, FinishedFlag = false,
                    Statuses = new List<CainiaoStatusEvent>() { }
                }
            };
        }

        public EncloseAndCNStatus? Delete(int id, int ownerId)
        {
            EncloseAndCNStatus? itemForDelete = GetById(id, ownerId);

            if (itemForDelete == null)
                return null;


            _encloseAndCNStatuses.Remove(itemForDelete);
            return itemForDelete;
        }

        public async Task<IEnumerable<EncloseAndCNStatus>> GetAll(Dictionary<string, string> filter)
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
                return encloseAndCNStatus.Id == id &&
                encloseAndCNStatus.OwnerId == ownerId;
            });
        }

        public EncloseAndCNStatus? Insert(EncloseAndCNStatus encloseAndCNStatus)
        {
            int maxId = _encloseAndCNStatuses.Max<EncloseAndCNStatus>(itemEncloseAndCNStatus => itemEncloseAndCNStatus.Id);

            encloseAndCNStatus.Id = maxId + 1;
            encloseAndCNStatus.OwnerId = 1;
            _encloseAndCNStatuses.Add(encloseAndCNStatus);
            return encloseAndCNStatus;
        }

       

        public EncloseAndCNStatus? Update(EncloseAndCNStatus encloseAndCNStatus)
        {
            EncloseAndCNStatus? encloseAndCNStatusForUpdate = GetById(encloseAndCNStatus.Id, 
                encloseAndCNStatus.OwnerId);

            if (encloseAndCNStatusForUpdate == null)
                return null;

            // Копирование
            encloseAndCNStatusForUpdate.TroubleFlag = encloseAndCNStatus.TroubleFlag;
            encloseAndCNStatusForUpdate.FinishedFlag = encloseAndCNStatus.FinishedFlag;

            return encloseAndCNStatusForUpdate;
        }
     }
}
