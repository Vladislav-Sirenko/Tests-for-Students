using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITestCoordinator
    {
        int GetTest(int Discipline_id,int Class_id);
        void Dispose();
    }
}
