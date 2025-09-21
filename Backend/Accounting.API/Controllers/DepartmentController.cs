using Accounting.API.Filters;
using Accounting.Core.DTOs.UpdateDTOs;
using Accounting.Core.DTOs;
using Accounting.Core.Models;
using Accounting.Core.Services.DepartmentService;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : CustomBaseController
    {
       private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = _departmentService.GetAll();
            var dtos = _mapper.Map<List<DepartmentDto>>(departments).ToList();
            return CreateActionResult(CustomResponseDto<List<DepartmentDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Department>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            var dto = _mapper.Map<DepartmentDto>(department);
            return CreateActionResult(CustomResponseDto<DepartmentDto>.Success(200, dto));
        }


        [ServiceFilter(typeof(NotFoundFilter<Department>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            var UserId = 1; // token olmadığı için şuanlık 1 yaptım
            var department = await _departmentService.GetByIdAsync(id);
            department.UpdatedBy = UserId;
            _departmentService.ChangeStatus(department);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(DepartmentDto departmentDto)
        {
            // token olmadığı için şuanlık 1 yaptım
            var UserId = 1;
            var processedEntity = _mapper.Map<Department>(departmentDto);
            processedEntity.Createdby = UserId;
            processedEntity.UpdatedBy = UserId;
            var department = await _departmentService.AddAsync(processedEntity);

            var departmentResponseDto = _mapper.Map<DepartmentDto>(department);

            return CreateActionResult(CustomResponseDto<DepartmentDto>.Success(201, departmentResponseDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(DepartmentUpdateDto departmentUpdateDto)
        {
            // token olmadığı için şuanlık 1 yaptım
            var UserId = 1;
            var currentDepartment = await _departmentService.GetByIdAsync(departmentUpdateDto.Id);

            currentDepartment.UpdatedBy = UserId;
            currentDepartment.Createdby = UserId;

            _departmentService.Update(currentDepartment);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
