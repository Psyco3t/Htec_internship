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
namespace Project.Services
{
    public class RolesServices
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public RolesServices(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<RolesDTO>>> GetRolesDTO()
        {
            return await _context.Roles
                .Select(r => _mapper
                .Map<Role, RolesDTO>(r))
                .ToListAsync();
        }
        public async Task<ActionResult<RolesDTO>> GetRoleDTO(int id)
        {
            var role = await _context.Roles
                .FindAsync(id);
            return _mapper
                .Map<Role, RolesDTO>(role);
        }
        public async Task<ActionResult<RolesDTO>> PutRoleDTO(RolesDTO roleDTO)
        {
            var role = await _context.Roles.FindAsync(roleDTO.RoleId);
            role.RoleName = roleDTO.RoleName;
            return _mapper.Map<Role, RolesDTO>(role);
        }
        public async Task<ActionResult<PostRoleDTO>> PostRoleDTO(PostRoleDTO role)
        {
            var model = new Role();
            model.RoleName = role.RoleName;
            _context.Roles.Add(model);
            await _context.SaveChangesAsync();
            return _mapper.Map<Role, PostRoleDTO>(model);
        }
        public void DeleteById(int id)
        {
            var roleToDelete = _context.Roles.FindAsync(id);
            if (roleToDelete.Result is not null)
            {
                _context.Roles.Remove(roleToDelete.Result);
                _context.SaveChanges();
            }
        }
        public bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.RoleId == id);
        }
        public async void SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
