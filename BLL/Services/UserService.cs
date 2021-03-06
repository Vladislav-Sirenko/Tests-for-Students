﻿using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }
        public IEnumerable<UserDTO> GetUsers()
        {
            var Users = Database.UserManager.Users.ToList();
            int i = 0;
            int[] n = new int[Users.Capacity];
            foreach(var user in Users)
            {
                n[i] = user.ClientProfile.Score;
                i++;
            }
            i = 0;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser,UserDTO>()).CreateMapper();
            var UsersDTO = mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserDTO>>(Users);
           foreach(var user in UsersDTO)
            {
                user.Score = n[i];
                     i++;
            }
            return UsersDTO;
        }
        public void DeleteUser(string id)
        {
            var User = Database.UserManager.FindById(id);
            Database.ClientManager.Delete(id);
            Database.UserManager.Delete(User);
        }
        public UserDTO GetUser(string id)
        {
            var User = Database.UserManager.FindById(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, UserDTO>()).CreateMapper();
            var UsersDTO = mapper.Map<ApplicationUser, UserDTO>(User);
            UsersDTO.Score = User.ClientProfile.Score;

            return UsersDTO;
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}