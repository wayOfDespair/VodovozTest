using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Vodovoz.Model;
using Vodovoz.View;

namespace Vodovoz.ViewModel
{
    public class EmployeeViewModel
    {
        public EmployeeView View { get; set; }
        public Employee Employee { get; set; }

        public IEnumerable<Department> Departments { get; set; }

        public IEnumerable<EmployeeSex> Sexes => Enum.GetValues(typeof(EmployeeSex)).Cast<EmployeeSex>();
        public DateTime DisplayDateStart = DateTime.Now.AddYears(-100);

        public RelayCommand AddCommand => new RelayCommand(delegate
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    if (Employee.EmployeeId > 0) db.Update(Employee);
                    else db.Employees.Add(Employee);

                    db.SaveChanges();
                }

                View.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });

        public EmployeeViewModel(int departmentId, int employeeId = 0)
        {
            using (var db = new DatabaseContext())
            {
                Departments = db.Departments.ToList();
                Employee = (employeeId == 0)
                    ? new Employee()
                    : db.Employees
                        .Include(x => x.Department)
                        .FirstOrDefault(x => x.EmployeeId == employeeId);

                Employee.DepartmentId = departmentId;
            }

            View = new EmployeeView() { DataContext = this };
        }
    }
}