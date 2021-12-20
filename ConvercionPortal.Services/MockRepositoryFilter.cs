using ConvercionPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvercionPortal.Services
{
    internal class MockRepositoryFilter
    {
        private delegate bool CompareField(Customer customer);
        private List<CompareField> _delegateFncList { get; set; }
        private Dictionary<string, string> _filters { get; set; }

        public MockRepositoryFilter(Dictionary<string, string> filters)
        {
            _delegateFncList = new List<CompareField>();
            _filters = filters;

            foreach ( var filter in filters)
            {
                if (filter.Key == "Name")
                    _delegateFncList.Add(CompareName);
                if (filter.Key == "Description")
                    _delegateFncList.Add(CompareDescription);
            }
        }

        public bool Compare(Customer customer)
        {
            bool result = true;

            _delegateFncList.ForEach( fnc => result &= fnc.Invoke(customer));

            return result;
        }

        private bool CompareName(Customer customer)
        {
            return customer.Name.ToLower().Contains(_filters["Name"].ToLower());
        }

        private bool CompareDescription(Customer customer)
        {
            if (customer.Description == null)
                if (_filters["Description"] == "")
                    return true;
                else
                    return false;
            return customer.Description.ToLower().Contains(_filters["Description"].ToLower());
        }
    }
}
