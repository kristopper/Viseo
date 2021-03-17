using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Viseo.Application.Common.Interfaces;

namespace Viseo.Application.Request.UserRequest.Queries.GetAllUser
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public GetAllUserQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var vm = new List<UserDto>();
            
            /*
            var userlist = await _context.AppUsers 
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            */
    
            vm = await _context.AppUsers
                .Include(s => s.CreatedBy)
                .Include(s => s.LastModifiedBy)                
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

           return vm;
        }
    }
}