namespace Northwind.Application.Account.Commands
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using FluentValidation;
  using MediatR;
  using System.Collections.Generic;
  using System.Linq;
  using System.Security.Claims;
  using System.Security.Principal;
  using AutoMapper;
  using Common.Interfaces;
  using Microsoft.AspNet.Identity;
  using Microsoft.AspNetCore.Authentication.Cookies;

  public class Login
  {
    public class Command : IRequest<IPrincipal>
    {
      public string Username { get; set; }
      public string Password { get; set; }
    }
  
    public class Validator : AbstractValidator<Command>
    {
      
    }

    public class Handler : IRequestHandler<Command, IPrincipal>
    {
      private readonly INorthwindDbContext _db;
      private readonly IMapper _mapper;
      private readonly IPasswordHasher _passwordHasher;

      public Handler(INorthwindDbContext db, IMapper mapper, IPasswordHasher passwordHasher)
      {
        _db = db;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
      }

      public Task<IPrincipal> Handle(Command command, CancellationToken cancellationToken)
      {
        var any = _db.Users.Any(u => u.Username == command.Username);
        
        if (!any)
        {
          throw new Exception();
        }

        var user = _db.Users.Single(u => u.Username == command.Username);

        // if (!_passwordHasher.VerifyHashedPassword(user.Password, command.Password))
        // {
        //   throw new Exception();
        // }
        
        if (user.Password != command.Password)
        {
          throw new Exception();
        }

        const string admins = "Admins";
        const string superUsers = "SuperUsers";
        
        var claims = new List<Claim>
        {
          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
          new Claim(ClaimTypes.Name, user.Username),
        };

        var isAdmin = user.UserRoles.Any(ug => ug.Role.Name == admins);

        if (isAdmin)
        {
          claims.Add(new Claim(ClaimTypes.Role, admins));
        }
        
        var isSuperUser = user.UserRoles.Any(ug => ug.Role.Name == superUsers);

        if (isSuperUser)
        {
          claims.Add(new Claim(ClaimTypes.Role, superUsers));
        }

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        
        IPrincipal principal = new ClaimsPrincipal(identity);

        return Task.FromResult(principal);

      }
      
    }
    
  }
  
}