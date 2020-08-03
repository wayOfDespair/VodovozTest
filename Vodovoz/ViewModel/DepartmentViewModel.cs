using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Vodovoz.Model;
using Vodovoz.View;

namespace Vodovoz.ViewModel
{
    public class DepartmentViewModel
    {
        public DepartmentView View { get; set; }
        public Department Department { get; set; }
        public List<Employee> Supervisors { get; set; }

        public RelayCommand AddCommand => new RelayCommand(delegate
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    if (Department.DepartmentId > 0) db.Update(Department);
                    else db.Departments.Add(Department);

                    db.SaveChanges();
                }

                View.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });

        public DepartmentViewModel(int departmentId = 0)
        {
            using (var db = new DatabaseContext())
            {
                Department = (departmentId == 0)
                    ? new Department()
                    : Department = db.Departments
                        .Include(x => x.Employees)
                        .ToList()
                        .FirstOrDefault(d => d.DepartmentId == departmentId);
            }

            View = new DepartmentView() { DataContext = this };
        }
    }
}