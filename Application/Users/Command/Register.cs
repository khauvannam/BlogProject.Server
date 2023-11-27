using Domain.Entity.User;
using Domain.Models;
using MediatR;

namespace Application.Users.Command;

public class Register : RegisterUserDto, IRequest { }
