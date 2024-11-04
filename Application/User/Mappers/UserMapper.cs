using Application.User.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Mappers
{
    public class UserMapper
    {
        public UserEntity ToEntity(UserRequest request)
        {
            return new UserEntity
            {
                Name = request.Name,
            };
        }

        public UserResponse ToDto(UserEntity user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
            };
        }
    }
}
