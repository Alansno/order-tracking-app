using Application.User.Dtos;
using Application.User.Mappers;
using Domain.Entities;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands
{
    public class AddUser
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly UserMapper _mapper;

        public AddUser(IRepository<UserEntity> userRepository, UserMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserResponse>> Execute(UserRequest request)
        {
            var user = _mapper.ToEntity(request);
            var userSaved = await _userRepository.Save(user);
            return Result<UserResponse>.Success(_mapper.ToDto(userSaved.Value));
        }
    }
}
