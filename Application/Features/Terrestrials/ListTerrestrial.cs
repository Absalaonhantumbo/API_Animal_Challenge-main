using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Terrestrials
{
    public class ListTerrestrial
    {
        public class ListTerrestrialQuery : IRequest<IReadOnlyList<Terrestrial>>
        {
        }
        
        public class ListTerrestrialHandler : IRequestHandler<ListTerrestrialQuery, IReadOnlyList<Terrestrial>>
        {
            private readonly DataContext _context;

            public ListTerrestrialHandler(DataContext context)
            {
                _context = context;
            }
            public async Task<IReadOnlyList<Terrestrial>> Handle(ListTerrestrialQuery request, CancellationToken cancellationToken)
            {
                return await _context.Terrestrials
                    .Include(x => x.TerrestrialType).Include(x => x.AnimalType)
                    .ToListAsync();
            }
        }
    }
}