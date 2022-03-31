using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Users
{
    public class UpdateUsers
    {
        public class UpdateUsersCommand : IRequest<UserDto>
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Password { get; set; }
        }
        
        public class UpdateUsersValidator : AbstractValidator<UpdateUsersCommand>
        {
            public UpdateUsersValidator()
            {
                RuleFor(x => x.FullName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.PhoneNumber).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
        
        public class UpdateUsersHandler : IRequestHandler<UpdateUsersCommand, UserDto>
        {
            public UserManager<AppUser> UserManager { get; }
            public IUserAccessor UserAccessor { get; }
            private readonly IMapper _mapper;

            public UpdateUsersHandler(UserManager<AppUser> userManager,IUserAccessor userAccessor, IMapper mapper)
            {
                UserManager = userManager;
                UserAccessor = userAccessor;
                _mapper = mapper;
            }

            public async Task<UserDto> Handle(UpdateUsersCommand request, CancellationToken cancellationToken)
            {
                var user = await UserManager.Users.Where(x => x.Email == request.Email)
                    .FirstOrDefaultAsync();
                if (user is null)
                {
                    throw new Exception("The user is not found");
                }

                user.FullName = request.FullName;
                user.PhoneNumber = request.PhoneNumber;
                var result = await UserManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception("fail to update");
                }

                return _mapper.Map<AppUser, UserDto>(user);
            }
        }
    }
}