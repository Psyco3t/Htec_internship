using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Project.Context;
using Project.Models;
using Project.Models.DTO;
using Project.Services;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RolesServices _roleService;
        private readonly IMapper _mapper;

        public RolesController(DataContext context, RolesServices roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolesDTO>>> GetRoles()
        {
            return await _roleService.GetRolesDTO();
        }
        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RolesDTO>> GetRole(int id)
        {
            var user = _roleService.GetRoleDTO(id);

            if (user == null)
            {
                return NotFound();
            }

            return await user;
        }

        // PUT: api/Users/5
        [HttpPut]
        public async Task<ActionResult<RolesDTO>> PutRole(RolesDTO userDTO)
        {
            var user = await _roleService.PutRoleDTO(userDTO);
            var model = await _roleService.GetRoleDTO(user.Value.RoleId);
            if (model.Value.RoleId != userDTO.RoleId)
            {
                return BadRequest();
            }
            if (!_roleService.RoleExists(userDTO.RoleId))
            {
                return NotFound();
            }
            _roleService.SaveChanges();
            return NoContent();
        }
        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<PostRoleDTO>> PostRole(PostRoleDTO userDTO)
        {
            var model = await _roleService.PostRoleDTO(userDTO);
            return Created();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var user = await _roleService.GetRoleDTO(id);
            if (user is not null)
            {
                _roleService.DeleteById(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
