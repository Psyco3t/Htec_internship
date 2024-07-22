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
using Project.Interfaces;
using Project.Services;

namespace Project.Controllers
{
 [Route("api/[controller]")]
 [ApiController]
 public class UsersController : ControllerBase
 {
  UserServices _userService;
  private readonly IEmailSender _emailSender;
  private readonly IMapper _mapper;

  public UsersController(DataContext context, UserServices userServices,IEmailSender emailSender ,IMapper mapper)
  {
   _userService = userServices;
   _mapper = mapper;
   _emailSender = emailSender;
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
   var model = await _userService.GetUserDTO(user.Value.Id);
   if (model.Value.Id != userDTO.Id)
   {
    return BadRequest();
   }

   if (!_userService.UserExists(userDTO.Id))
   {
    return NotFound();
   }

   _userService.SaveChanges();
   return NoContent();
  }
  [HttpPut("PutRole")]
  public async Task<ActionResult<PutUserRoleDTO>> PutUserRole(PutUserRoleDTO userDTO)
  {
   var roleOld = await _userService.GetUserDTO(userDTO.Id);
   var user = await _userService.PutUserRoleDTO(userDTO);
   var model = await _userService.GetUserDTO(user.Value.Id);
   string subject = "Role Assignment";
   if (model.Value.Id != userDTO.Id)
   {
    return BadRequest();
   }

   if (!_userService.UserExists(userDTO.Id))
   {
    return NotFound();
   }
   _userService.SaveChanges();
   var roleNew = await _userService.GetUserDTO(userDTO.Id); //use something like this but maybe with less calls
   await _emailSender.SendEmailAsync(
    "alternativeuser7@gmail.com"
    , subject, "Your role has been changed from " +
    roleOld.Value.RoleName.ToString() + " to " 
    + roleNew.Value.RoleName.ToString()
    );
   return NoContent();
  }
  // POST: api/Users
  [HttpPost]
  public async Task<ActionResult<UserPostDTO>> PostUser(UserPostDTO userDTO)
  {
   var model = await _userService.PostUserDTO(userDTO);
   if (model.Value.RoleId == 0)
   {
    return Created();
   }
   else
   {
    return BadRequest();
   }
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
