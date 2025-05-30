using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.Extensions.Logging;

namespace BackEnd.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ILogger<DepartmentService> _logger;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public DepartmentService(IUnidadDeTrabajo unidad,
                                 ILogger<DepartmentService> logger)
        {
            _unidadDeTrabajo = unidad;
            _logger = logger;
        }

        private DepartmentDTO Convertir(Department department)
        {
            return new DepartmentDTO
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                Budget = department.Budget,
                StartDate = department.StartDate,
                Administrator = department.Administrator
            };
        }

        private Department Convertir(DepartmentDTO departmentDTO)
        {
            return new Department
            {
                DepartmentId = departmentDTO.DepartmentId,
                Name = departmentDTO.Name,
                Budget = departmentDTO.Budget,
                StartDate = departmentDTO.StartDate,
                Administrator = departmentDTO.Administrator
            };
        }

        public DepartmentDTO AddDepartment(DepartmentDTO departmentDTO)
        {
            try
            {
                _logger.LogInformation("Ingresa a AddDepartment");
                _unidadDeTrabajo.DepartmentDAL.Add(Convertir(departmentDTO));
                _unidadDeTrabajo.Complete();
                return departmentDTO;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error en AddDepartment");
                throw;
            }
        }

        public DepartmentDTO DeleteDepartment(int id)
        {
            var department = new Department { DepartmentId = id };
            _unidadDeTrabajo.DepartmentDAL.Remove(department);
            _unidadDeTrabajo.Complete();
            return Convertir(department);
        }

        public List<DepartmentDTO> GetDepartments()
        {
            var departments = _unidadDeTrabajo.DepartmentDAL.Get();
            var departmentDTOs = new List<DepartmentDTO>();
            foreach (var department in departments)
            {
                departmentDTOs.Add(Convertir(department));
            }
            return departmentDTOs;
        }

        public DepartmentDTO GetDepartmentById(int id)
        {
            var result = _unidadDeTrabajo.DepartmentDAL.FindById(id);
            return Convertir(result);
        }

        public DepartmentDTO UpdateDepartment(DepartmentDTO departmentDTO)
        {
            var entity = Convertir(departmentDTO);
            _unidadDeTrabajo.DepartmentDAL.Update(entity);
            _unidadDeTrabajo.Complete();
            return departmentDTO;
        }
    }
}

