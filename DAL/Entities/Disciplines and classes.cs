using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
   public class Disciplines_classes
    {
        [Key]
        public int Test_Id { get; set; }
        public int Discipline_ID { get; set; }
        public int Class_ID { get; set; }
    }
}
