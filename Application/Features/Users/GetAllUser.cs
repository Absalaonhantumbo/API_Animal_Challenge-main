using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Users
{
    public class GetAllUser
    {
        public class GetAllUserQuery : IRequest<List<UserDto>>
        {
        }
        
        public class GetAllUserHandler : IRequestHandler<GetAllUserQuery, List<UserDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public GetAllUserHandler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
            {
                var users = await _context.Users.ToListAsync();
                return _mapper.Map<List<AppUser>, List<UserDto>>(users);
            }
        }
    }
}