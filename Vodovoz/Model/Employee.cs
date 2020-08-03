using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Vodovoz.Model
{
    public enum EmployeeSex { Male, Female }

    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string MiddleName { get; set; }
        public DateTime Birthday { get; set; }
        public EmployeeSex Sex { get; set; }
        public bool IsSupervisor { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual ObservableCollection<Order> Orders { get; set; }

        public Employee()
        {
            Birthday = DateTime.Now;
            Orders = new ObservableCollection<Order>();
        }

        public override string ToString()
        {
            return string.Join(" ", Lastname, Firstname, MiddleName);
        }
    }
}