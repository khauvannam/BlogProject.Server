using Domain.Entity.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class UserDbContext : IdentityDbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> opt)
        : base(opt) { }
}
