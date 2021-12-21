using ConvercionPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvercionPortal.Services
{
    public class MockConvercionTypeRepository : IConvercionTypeRepository
    {
        private List<ConvercionType> _convercionTypes;

        public MockConvercionTypeRepository()
        {
            _convercionTypes = new List<ConvercionType>()
             {
                new ConvercionType()
                  { Id = 1, Name = "Общий конвертер", ServiceUrl = "https://common.client.domain.ru"},
                new ConvercionType()
                  { Id = 2, Name = "Для важного клиента", ServiceUrl = "https://important.client.domain.ru" },
                new ConvercionType()
                  { Id = 3, Name = "Для очень важного клиента", ServiceUrl = "https://very.important.client.domain.ru"}
             };
        }

        public ConvercionType? Delete(int id)
        {
            ConvercionType? сonvercionTypeForDelete = GetById(id);

            if (сonvercionTypeForDelete == null)
                return null;


            _convercionTypes.Remove(сonvercionTypeForDelete);
            return сonvercionTypeForDelete;
        }

        public IEnumerable<ConvercionType> GetAll(Dictionary<string, string> filter)
        {
            if (filter.Count == 0)
                return _convercionTypes;

            MockConvercionTypeFiler mockConvercionTypeFiler = new(filter);

            var result = from convercionType in _convercionTypes
                         where mockConvercionTypeFiler.Compare(convercionType) //фильтрация по критерию
                         select convercionType; // выбираем объект
            return result;
        }

        public ConvercionType? GetById(int id)
        {
            return _convercionTypes.FirstOrDefault(convercionType => convercionType.Id == id);
        }

        public ConvercionType? Insert(ConvercionType convercionType)
        {
            int maxId = _convercionTypes.Max<ConvercionType>(itemConvercionTipe => itemConvercionTipe.Id);

            convercionType.Id = maxId + 1;
            _convercionTypes.Add(convercionType);
            return convercionType;
        }

        public ConvercionType? Update(ConvercionType convercionType)
        {
            ConvercionType? conversionTypeForUpdate = GetById(convercionType.Id);

            if (conversionTypeForUpdate == null)
                return null;

            // Копирование
            conversionTypeForUpdate.Name = convercionType.Name;
            conversionTypeForUpdate.Description = convercionType.Description;

            return conversionTypeForUpdate;
        }

    }
}
