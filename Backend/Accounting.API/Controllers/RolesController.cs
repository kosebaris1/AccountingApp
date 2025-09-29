using Accounting.API.Filters;
using Accounting.Core.DTOs.UpdateDTOs;
using Accounting.Core.DTOs;
using Accounting.Core.Models;
using Accounting.Core.Services.RoleService;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : CustomBaseController
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        public RolesController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = _roleService.GetAll();
            var dtos = _mapper.Map<List<RoleDto>>(roles).ToList();
            return CreateActionResult(CustomResponseDto<List<RoleDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Role>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            var dto = _mapper.Map<RoleDto>(role);
            return CreateActionResult(CustomResponseDto<RoleDto>.Success(200, dto));
        }


        [ServiceFilter(typeof(NotFoundFilter<Role>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            var UserId = GetUserFromToken();
            var role = await _roleService.GetByIdAsync(id);
            role.UpdatedBy = UserId;
            _roleService.ChangeStatus(role);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(RoleDto roleDto)
        {
            var UserId = GetUserFromToken();
            var processedEntity = _mapper.Map<Role>(roleDto);
            processedEntity.Createdby = UserId;
            processedEntity.UpdatedBy = UserId;
            var role = await _roleService.AddAsync(processedEntity);

            var roleResponseDto = _mapper.Map<RoleDto>(role);

            return CreateActionResult(CustomResponseDto<RoleDto>.Success(201, roleResponseDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(RoleUpdateDto roleUpdateDto)
        {
            var UserId = GetUserFromToken();
            var currentRole = await _roleService.GetByIdAsync(roleUpdateDto.Id);

            currentRole.UpdatedBy = UserId;
            currentRole.Createdby = UserId;

            _roleService.Update(currentRole);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
