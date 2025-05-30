using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface IDepartmentService
    {

        List<DepartmentDTO> GetDepartments();
        DepartmentDTO GetDepartmentById(int id);
        DepartmentDTO AddDepartment(DepartmentDTO department);
        DepartmentDTO UpdateDepartment(DepartmentDTO department);
        DepartmentDTO DeleteDepartment(int id);
    }
}
