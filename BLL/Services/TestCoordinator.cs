using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System.Linq;


namespace BLL.Services
{
    public class TestCoordinator : ITestCoordinator
    {
        IUnitOfWork Database { get; set; }

        public TestCoordinator(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void Dispose()
        {
            Database.Dispose();
        }

        public int GetTest(int Discipline_id, int Class_id)
        {
            return Database.Disciplines_classes.Find(key => key.Class_ID == Class_id && key.Discipline_ID == Discipline_id).Select(id => id.Test_Id).First();
        }
    }
}
