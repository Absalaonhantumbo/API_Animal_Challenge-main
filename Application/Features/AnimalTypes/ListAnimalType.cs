using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.AnimalTypes
{
    public class ListAnimalType
    {
        public class ListAnimalTypeQuery : IRequest<IReadOnlyList<AnimalType>>
        {
        }
        
        public class ListAnimalTypeHandler: IRequestHandler<ListAnimalTypeQuery, IReadOnlyList<AnimalType>>
        {
            private readonly DataContext _context;
            private readonly UserManager<AppUser> _userManager;

            public ListAnimalTypeHandler(DataContext context, UserManager<AppUser> userManager)
            {
                _context = context;
                _userManager = userManager;
            }
            
            public async Task<IReadOnlyList<AnimalType>> Handle(ListAnimalTypeQuery request, CancellationToken cancellationToken)
            {
                return await _context.AnimalTypes.ToListAsync();
            }
        }
    }
}