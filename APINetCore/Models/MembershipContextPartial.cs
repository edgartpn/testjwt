#nullable disable
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Configuration;
namespace APINetCore.Models;

public partial class MembershipContext : DbContext
{


    private readonly IConfiguration _configuration;
    private readonly DbContextOptions<MembershipContext> _dbContextOptions;

    public MembershipContext(DbContextOptions<MembershipContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
        _dbContextOptions = options;
    }

    public MembershipContext() {

  
    }


}