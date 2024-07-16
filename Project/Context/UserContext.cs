using Microsoft.EntityFrameworkCore;
using Project.Models;
using System;
using System.Collections.Generic;
namespace Project.Context
{
 /// <summary>
 /// User context is an extension of DBContext using base options and referencing models
 /// </summary>
 public class UserContext : DbContext
 {
  public UserContext(DbContextOptions<UserContext> options) : base(options)
  {

  }
  public virtual DbSet<User> Users => Set<User>();
  public virtual DbSet<Role> Roles => Set<Role>();
 }
}
