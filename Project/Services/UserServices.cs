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
using Microsoft.EntityFrameworkCore.Diagnostics;
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
                .Include(r=>r.Role)
                .Select(r => _mapper
                .Map<User,UserDTO>(r))
                .ToListAsync();
        }
        public async Task<ActionResult<IEnumerable<UserAllDTO>>> GetUsersAllDTO()
        {
            return await _context.Users
                .Include(r=>r.Role)
                .Select(r=>_mapper
                .Map<User,UserAllDTO>(r))
                .ToListAsync();
        }
        public async Task<ActionResult<UserDTO>> GetUserDTO(int id)
        {
            var user = await _context.Users
                .Include(r=>r.Role)
                .SingleOrDefaultAsync(u=>u.Id==id);

            return _mapper
                .Map<User, UserDTO>(user);
        }
        public async Task<ActionResult<UserPutDTO>> PutUserDTO(UserPutDTO userDTO)
        {
            var user = await _context.Users.FindAsync(userDTO.Id);
            user.UserName = userDTO.UserName;
            user.Password = userDTO.Password;
            user.Email = userDTO.Email;
            await _context.SaveChangesAsync();
            return _mapper.Map<User, UserPutDTO>(user);
        }

        public async Task<ActionResult<PutUserRoleDTO>> PutUserRoleDTO(PutUserRoleDTO userDTO)
        {
            var user = await _context.Users.FindAsync(userDTO.Id);
            user.RoleId= userDTO.RoleId;
            return _mapper.Map<User, PutUserRoleDTO>(user);
        }
        public async Task<ActionResult<UserPostDTO>> PostUserDTO(UserPostDTO user)
        {
            var model = new User();
            model.UserName = user.UserName;
            model.Password = user.Password;
            model.Email = user.Email;
            model.RoleId = user.RoleId;
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

        public async void SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
