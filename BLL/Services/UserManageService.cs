using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
   public class UserManageService:IUserManageService
    {
        IUnitOfWork Database { get; set; }
        public UserManageService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public IEnumerable<ClientProfileDTO> GetUsers()
        {
            var Users = Database.ClientManager.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientProfile, ClientProfileDTO>()).CreateMapper();
            var UsersDTO = mapper.Map<IEnumerable<ClientProfile>, IEnumerable<ClientProfileDTO>>(Users);
            return UsersDTO;
        }
    }
}
