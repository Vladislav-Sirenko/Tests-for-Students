using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.DTO
{
    public class StudentDTO
    {
        public int ID { get; set; }
        public int ID_User { get; set; }
        public string FIO { get; set; }
        public ICollection<Result> results { get; set; }
    }
}