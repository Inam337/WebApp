using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain.Entities;
namespace Application.Commands
{
    public record CreateUserCommand(string Name, string Email) : IRequest<User>;

    //kk
}
