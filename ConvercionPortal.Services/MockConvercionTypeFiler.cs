using ConvercionPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvercionPortal.Services
{
    internal class MockConvercionTypeFiler
    {
        private delegate bool CompareField(ConvercionType customer);
        private List<CompareField> _delegateFncList { get; set; }
        private Dictionary<string, string> _filters { get; set; }

        public MockConvercionTypeFiler(Dictionary<string, string> filters)
        {
            _delegateFncList = new List<CompareField>();
            _filters = filters;

            foreach (var filter in filters)
            {
                if (filter.Key == "Name")
                    _delegateFncList.Add(CompareName);
                if (filter.Key == "Description")
                    _delegateFncList.Add(CompareDescription);
                if (filter.Key == "ServiceUrl")
                    _delegateFncList.Add(CompareUrl);
            }
        }

        public bool Compare(ConvercionType convercionType)
        {
            bool result = true;

            _delegateFncList.ForEach(fnc => result &= fnc.Invoke(convercionType));

            return result;
        }

        private bool CompareName(ConvercionType convercionType)
        {
            return convercionType.Name.ToLower().Contains(_filters["Name"].ToLower());
        }

        private bool CompareUrl(ConvercionType convercionType)
        {
            return convercionType.ServiceUrl.ToLower().Contains(_filters["ServiceUrl"].ToLower());
        }

        private bool CompareDescription(ConvercionType convercionType)
        {
            if (convercionType.Description == null)
                if (_filters["Description"] == "")
                    return true;
                else
                    return false;
            return convercionType.Description.ToLower().Contains(_filters["Description"].ToLower());
        }
    }
}
