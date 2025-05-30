namespace FrontEnd.ApiModels
{
    public class DepartmentAPI
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; } = null!;

        public decimal Budget { get; set; }

        public DateTime StartDate { get; set; }

        public int? Administrator { get; set; }
    }
}
