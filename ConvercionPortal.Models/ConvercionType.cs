using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvercionPortal.Models
{
    public class ConvercionType
    {
        public int Id { get; set; }
        public string Name { get; set; }          // Название
        public string ServiceUrl { get; set; }    // Ссылка на сервис конвертации
        public string? Description { get; set; }  // Описание
    }
}
