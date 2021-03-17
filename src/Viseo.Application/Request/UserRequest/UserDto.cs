using Viseo.Domain.Entities;
using Viseo.Application.Common.Mapping;
using AutoMapper;
using System;
using Viseo.Application.Common.Extensions;

namespace Viseo.Application.Request.UserRequest
{
    public class UserDto : IMapFrom<AppUser>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int Status { get; set; }
        public string StatusString { get; set; }

        public string CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedById { get; set; }
        public string LastModifiedByName { get; set; }
        public DateTime? LastModified { get; set; }

        public void Mapping(Profile profile)
        {

            profile.CreateMap<AppUser, UserDto>()
             .ForMember(o => o.Id, opt => opt.MapFrom(x => x.Id))
             .ForMember(o => o.FirstName, opt => opt.MapFrom(x => x.FirstName))
             .ForMember(o => o.LastName, opt => opt.MapFrom(x => x.LastName))
             .ForMember(o => o.Email, opt => opt.MapFrom(x => x.Email))
             .ForMember(o => o.UserName, opt => opt.MapFrom(x => x.UserName))
             .ForMember(o => o.Password, opt => opt.MapFrom(x => x.PasswordHash))
             .ForMember(o => o.Role, opt => opt.MapFrom(x => x.Role.ToString()))
             .ForMember(o => o.Status, opt => opt.MapFrom(x => x.Status))
             .ForMember(o => o.StatusString, opt => opt.MapFrom(x => x.Status.ToString()))
             .ForMember(o => o.CreatedById, opt => opt.MapFrom(x => x.CreatedById))
             .ForMember(o => o.CreatedByName, opt => opt.MapFrom(x => x.GetCreatorFullName()))
             .ForMember(o => o.Created, opt => opt.MapFrom(x => x.Created))
             .ForMember(o => o.LastModifiedById, opt => opt.MapFrom(x => x.LastModifiedById))
             .ForMember(o => o.LastModifiedByName, opt => opt.MapFrom(x => x.GetModifierFullName()))
             .ForMember(o => o.LastModified, opt => opt.MapFrom(x => x.LastModified));
        }
    }
}