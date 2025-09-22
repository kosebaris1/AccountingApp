using Accounting.API.Filters;
using Accounting.Core.DTOs.UpdateDTOs;
using Accounting.Core.DTOs;
using Accounting.Core.Models;
using Accounting.Core.Services.UserService;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : CustomBaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = _userService.GetAll().ToList();
            var dtos = _mapper.Map<List<UserDto>>(users);
            return CreateActionResult(CustomResponseDto<List<UserDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            var dto = _mapper.Map<UserDto>(user);
            return CreateActionResult(CustomResponseDto<UserDto>.Success(200, dto));
        }


        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            var UserId = 1; // token olmadığı için şuanlık 1 yaptım
            var user = await _userService.GetByIdAsync(id);
            user.UpdatedBy = UserId;
            _userService.ChangeStatus(user);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserDto userDto)
        {
            // token olmadığı için şuanlık 1 yaptım
            var UserId = 1;
            var processedEntity = _mapper.Map<User>(userDto);
            processedEntity.Createdby = UserId;
            processedEntity.UpdatedBy = UserId;
            var user = await _userService.AddAsync(processedEntity);

            var userResponseDto = _mapper.Map<UserDto>(user);

            return CreateActionResult(CustomResponseDto<UserDto>.Success(201, userResponseDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            // token olmadığı için şuanlık 1 yaptım
            var UserId = 1;
            var currentUser = await _userService.GetByIdAsync(userUpdateDto.Id);

            currentUser.UpdatedBy = UserId;
            currentUser.Createdby = UserId;

            _userService.Update(currentUser);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
