using AutoMapper;
using CompanyPortal.Application.Abstractions.Department;
using CompanyPortal.Application.Abstractions.Department.Dtos;
using CompanyPortal.Application.Abstractions.Repositories.Department;
using CompanyPortal.Domain.Entities;

namespace CompanyPortal.Application.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentReadRepository _departmentReadRepository;
        private readonly IDepartmentWriteRepository _departmentWriteRepository;
        private readonly IMapper _mapper;

        public DepartmentService(
            IDepartmentReadRepository departmentReadRepository, 
            IDepartmentWriteRepository departmentWriteRepository,
            IMapper mapper)
        {
            _departmentReadRepository = departmentReadRepository;
            _departmentWriteRepository = departmentWriteRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddDepartment(DepartmentDto department)
        {
            if (department is null)
                return false;

            try
            {
                var mappedResult = _mapper.Map<DepartmentDto, Domain.Entities.Department>(department);

                var response = await _departmentWriteRepository.AddAsync(mappedResult);

                return response;
            }
            catch (Exception) 
            {
                return false;
            }
        }

        public async Task<bool> DeleteDepartment(Guid id)
        {
            if (id == Guid.Empty)
                return false;

            try
            {
                return await _departmentWriteRepository.RemoveAsync(id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditDepartment(DepartmentDto department)
        {
            if (department is null)
                return false;

            try
            {
                var departmentEntity = await _departmentReadRepository.GetFirstOrDefaultAsync(x => x.Id == department.Id);

                if (departmentEntity is null)
                    return false;

                var updateEntity = _mapper.Map<DepartmentDto, Domain.Entities.Department>(department);

                var response = _departmentWriteRepository.Update(updateEntity);

                return response;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<DepartmentDto> GetAllDepartments()
        {
            try
            {
                var departments = _departmentReadRepository.GetQueryable()
                                                           .Where(x => x.Status)
                                                           .ToList();
                if(departments is null)
                   return [];

                var mappedResult = _mapper.Map<List<Domain.Entities.Department>, List<DepartmentDto>>(departments);

                return mappedResult;
            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<DepartmentDto> GetDepartmentById(Guid id)
        {
            if (id == Guid.Empty)
                return new DepartmentDto();

            try
            {
                var department = await _departmentReadRepository.GetByIdAsync(id);

                var mappedResult = _mapper.Map<Domain.Entities.Department, DepartmentDto>(department);

                return mappedResult;
            }
            catch (Exception)
            {
                return new DepartmentDto();
            }
        }

        public async Task<bool> SoftDeleteDepartment(Guid id)
        {
            if (id == Guid.Empty)
                return false;

            try
            {
                var departmentEntity = await _departmentReadRepository.GetByIdAsync(id);

                if (departmentEntity is null)
                    return false;

                departmentEntity.Status = false;

                await _departmentWriteRepository.SaveAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
