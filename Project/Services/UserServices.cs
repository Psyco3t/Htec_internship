using Project.Context;
using Project.Models;
using Project.Services;
using Project.Models.DTO;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NuGet.Versioning;
using Microsoft.AspNetCore.Identity;
using Project.Migrations;
namespace Project.Services
{
    public class UserServices
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserServices(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersDTO()
        {
            return await _context.Users
                .Select(m => _mapper
                .Map<User,UserDTO>(m))
                .ToListAsync();
        }
        public async Task<ActionResult<IEnumerable<UserAllDTO>>> GetUsersAllDTO()
        {
            return await _context.Users
                .Select(m=>_mapper
                .Map<User,UserAllDTO>(m))
                .ToListAsync();
        }
        public async Task<ActionResult<UserDTO>> GetUserDTO(int id)
        {
            var user = await _context.Users.FindAsync(id);

            return _mapper
                .Map<User, UserDTO>(user);
        }
        public async Task<ActionResult<UserPutDTO>> PutUserDTO(UserPutDTO userDTO)
        {
            var user = await _context.Users.FindAsync(userDTO.Id);
            user.UserName = userDTO.UserName;
            user.Password = userDTO.Password;
            user.Email = userDTO.Email;
            return _mapper.Map<User, UserPutDTO>(user);
        }
        public async Task<ActionResult<UserPostDTO>> PostUserDTO(UserPostDTO user)
        {
            var model = new User();
            model.UserName = user.UserName;
            model.Password = user.Password;
            model.Email = user.Email;
            _context.Users.Add(model);
            await _context.SaveChangesAsync();
            return _mapper.Map<User,UserPostDTO>(model);
        }
        public void DeleteById(int id)
        {
            var userToDelete = _context.Users.FindAsync(id);
            if (userToDelete.Result is not null)
            {
                _context.Users.Remove(userToDelete.Result);
                _context.SaveChanges();
            }
        }
        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
