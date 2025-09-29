using Accounting.API.Filters;
using Accounting.Core.DTOs.UpdateDTOs;
using Accounting.Core.DTOs;
using Accounting.Core.Models;
using Accounting.Core.Services.GroupInRoleService;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupInRolesController : CustomBaseController
    {
        private readonly IGroupInRoleService _groupInRoleService;
        private readonly IMapper _mapper;
        public GroupInRolesController(IGroupInRoleService groupInRoleService, IMapper mapper)
        {
            _groupInRoleService = groupInRoleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var groupInRoles = _groupInRoleService.GetAll();
            var dtos = _mapper.Map<List<GroupInRoleDto>>(groupInRoles).ToList();
            return CreateActionResult(CustomResponseDto<List<GroupInRoleDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<GroupInRole>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var groupInRole = await _groupInRoleService.GetByIdAsync(id);
            var dto = _mapper.Map<GroupInRoleDto>(groupInRole);
            return CreateActionResult(CustomResponseDto<GroupInRoleDto>.Success(200, dto));
        }


        [ServiceFilter(typeof(NotFoundFilter<GroupInRole>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            var UserId = GetUserFromToken();
            var groupInRole = await _groupInRoleService.GetByIdAsync(id);
            groupInRole.UpdatedBy = UserId;
            _groupInRoleService.ChangeStatus(groupInRole);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(GroupInRoleDto groupInRoleDto)
        {
            var UserId = GetUserFromToken();
            var processedEntity = _mapper.Map<GroupInRole>(groupInRoleDto);
            processedEntity.Createdby = UserId;
            processedEntity.UpdatedBy = UserId;
            var groupInRole = await _groupInRoleService.AddAsync(processedEntity);

            var groupInRoleResponseDto = _mapper.Map<GroupInRoleDto>(groupInRole);

            return CreateActionResult(CustomResponseDto<GroupInRoleDto>.Success(201, groupInRoleResponseDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(GroupInRoleUpdateDto groupInRoleUpdateDto)
        {
            var UserId = GetUserFromToken();
            var currentGroupInRole = await _groupInRoleService.GetByIdAsync(groupInRoleUpdateDto.Id);

            currentGroupInRole.UpdatedBy = UserId;
            currentGroupInRole.Createdby = UserId;

            _groupInRoleService.Update(currentGroupInRole);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
