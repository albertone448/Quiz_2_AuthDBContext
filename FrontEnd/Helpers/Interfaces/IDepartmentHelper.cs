using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IDepartmentHelper
    {
        DepartmentViewModel Get(int id);

        string Token { get; set; }
        List<DepartmentViewModel> GetDepartments();

        void Add(DepartmentViewModel department);
        void Update(DepartmentViewModel department);
        void Delete(int id);
    }
}
