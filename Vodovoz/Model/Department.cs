using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Vodovoz.Model
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public virtual ObservableCollection<Employee> Employees { get; set; }

        [NotMapped]
        public Employee Supervisor
        {
            get => Employees.FirstOrDefault(x => x.IsSupervisor);
            set
            {
                Employees.ToList().ForEach(x => x.IsSupervisor = (x.EmployeeId == value.EmployeeId));
            }
        }

        public Department()
        {
            Employees = new ObservableCollection<Employee>();
        }
    }
}