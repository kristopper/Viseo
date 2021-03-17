using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Viseo.Application.Common.Exceptions;
using Viseo.Application.Common.Interfaces;
using Viseo.Domain.Common.Enums;

namespace Viseo.Application.Request.UserRequest.Queries.GetAllUserByType
{
    public class GetAllUserByTypeQueryHandler : IRequestHandler<GetAllUserByTypeQuery, UserVm>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllUserByTypeQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<UserVm> Handle(GetAllUserByTypeQuery request, CancellationToken cancellationToken)
        {
            var tokenrole = _currentUserService.Role;
            var query = _context.AppUsers.Where(q => q.Status != Status.Deleted).AsQueryable();

            if (tokenrole == Role.user)
            {
                query = query.Where(q => q.Role == Role.user);
            }

            if (tokenrole == Role.Admin)
            {
                query = request.UserType switch
                {
                    Displayrole.All => query,
                    Displayrole.Admin => query = query.Where(q => q.Role == Role.Admin),
                    Displayrole.user => query = query.Where(q => q.Role == Role.user),
                    _ => throw new OutOfRangeException("UserType", (int)request.UserType),
                };
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(q =>
                    q.FirstName.ToLower().Contains(request.Search.ToLower()) ||
                    q.LastName.ToLower().Contains(request.Search.ToLower()) ||
                    q.Email.ToLower().Contains(request.Search.ToLower()));
            }

            var total = await query.CountAsync();

            query = query.Skip(request.pageNumber * request.pageSize);
            query = query.Take(request.pageSize);

            var userlist = await query  
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.Id)
                .ToListAsync();

            return new UserVm
            {
                UserList = userlist,
                Total = total,
                pageNumber = request.pageNumber,
                pageSize = request.pageSize,
            };
        }
    }
}