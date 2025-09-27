using Accounting.API.Filters;
using Accounting.Core.DTOs.UpdateDTOs;
using Accounting.Core.DTOs;
using Accounting.Core.Models;
using Accounting.Core.Services.GroupService;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Accounting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : CustomBaseController
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        public GroupsController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var groups = _groupService.GetAll();
            var dtos = _mapper.Map<List<GroupDto>>(groups).ToList();
            return CreateActionResult(CustomResponseDto<List<GroupDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Group>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var group = await _groupService.GetByIdAsync(id);
            var dto = _mapper.Map<GroupDto>(group);
            return CreateActionResult(CustomResponseDto<GroupDto>.Success(200, dto));
        }


        [ServiceFilter(typeof(NotFoundFilter<Group>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            var UserId = 1; // token olmadığı için şuanlık 1 yaptım
            var group = await _groupService.GetByIdAsync(id);
            group.UpdatedBy = UserId;
            _groupService.ChangeStatus(group);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(GroupDto groupDto)
        {
            // token olmadığı için şuanlık 1 yaptım
            var UserId = 1;
            var processedEntity = _mapper.Map<Group>(groupDto);
            processedEntity.Createdby = UserId;
            processedEntity.UpdatedBy = UserId;
            var group = await _groupService.AddAsync(processedEntity);

            var groupResponseDto = _mapper.Map<GroupDto>(group);

            return CreateActionResult(CustomResponseDto<GroupDto>.Success(201, groupResponseDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(GroupUpdateDto groupUpdateDto)
        {
            // token olmadığı için şuanlık 1 yaptım
            var UserId = 1;
            var currentGroup = await _groupService.GetByIdAsync(groupUpdateDto.Id);

            currentGroup.UpdatedBy = UserId;
            currentGroup.Createdby = UserId;

            _groupService.Update(currentGroup);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
