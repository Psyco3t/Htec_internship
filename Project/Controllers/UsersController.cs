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
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        UserServices _userService;
        private readonly IMapper _mapper;

        public UsersController(DataContext context,UserServices userServices, IMapper mapper)
        {
            _context = context;
            _userService = userServices;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return await _userService.GetUsersDTO();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<UserAllDTO>>> GetUsersAll()
        {
            return await _userService.GetUsersAllDTO();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = _userService.GetUserDTO(id);

            if (user == null)
            {
                return NotFound();
            }

            return await user;
        }

        // PUT: api/Users/5
        [HttpPut]
        public async Task<ActionResult<UserPutDTO>> PutUser(UserPutDTO userDTO)
        {
            var user = await _userService.PutUserDTO(userDTO);
            var model =await _userService.GetUserDTO(user.Value.Id);
            if (model.Value.Id != userDTO.Id)
            {
                return BadRequest();
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_userService.UserExists(userDTO.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPut("PutRole")]
        public async Task<ActionResult<PutUserRoleDTO>> PutUserRole(PutUserRoleDTO userDTO)
        {
            var user = await _userService.PutUserRoleDTO(userDTO);
            var model = await _userService.GetUserDTO(user.Value.Id);
            if (model.Value.Id != userDTO.Id)
            {
                return BadRequest();
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_userService.UserExists(userDTO.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserPostDTO>> PostUser(UserPostDTO userDTO)
        {
            var model = await _userService.PostUserDTO(userDTO);
            await _context.SaveChangesAsync();
            return Created();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserDTO(id);
            if (user is not null)
            {
                _userService.DeleteById(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
