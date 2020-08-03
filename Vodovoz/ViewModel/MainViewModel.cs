using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using Vodovoz.Model;

namespace Vodovoz.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<Department> _departments;

        public ObservableCollection<Department> Departments

        {
            get => _departments;
            set { _departments = value; OnPropertyChanged(); }
        }

        private Department _selectedDepartment;

        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set { _selectedDepartment = value; OnPropertyChanged(); }
        }

        private Employee _selectedEmployee;

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set { _selectedEmployee = value; OnPropertyChanged(); }
        }

        private Order _selectedOrder;

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set { _selectedOrder = value; OnPropertyChanged(); }
        }

        public RelayCommand DepartmentAddCommand => new RelayCommand(delegate
        {
            var vm = new DepartmentViewModel();
            if (vm.View.ShowDialog() == true) LoadDepartments();
        });

        public RelayCommand DepartmentEditCommand => new RelayCommand(param =>
        {
            if (!(param is Department department)) return;

            var vm = new DepartmentViewModel(department.DepartmentId);
            if (vm.View.ShowDialog() == true) LoadDepartments();
        });

        public RelayCommand DepartmentDeleteCommand => new RelayCommand(param =>
        {
            if (!(param is Department department)) return;

            using (var db = new DatabaseContext())
            {
                db.Departments.Remove(department);
                db.SaveChanges();
                Departments.Remove(department);
            }
        });

        public RelayCommand EmployeeAddCommand => new RelayCommand(delegate
        {
            var vm = new EmployeeViewModel(SelectedDepartment.DepartmentId);

            if (vm.View.ShowDialog() == true) LoadDepartments();
        });

        public RelayCommand EmployeeEditCommand => new RelayCommand(param =>
        {
            if (!(param is Employee employee)) return;

            var vm = new EmployeeViewModel(SelectedDepartment.DepartmentId, employee.EmployeeId);
            if (vm.View.ShowDialog() == true) LoadDepartments();
        });

        public RelayCommand EmployeeDeleteCommand => new RelayCommand(param =>
        {
            if (!(param is Employee employee)) return;

            using (var db = new DatabaseContext())
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
                SelectedDepartment.Employees.Remove(employee);
            }
        });

        public RelayCommand OrderAddCommand => new RelayCommand(delegate
        {
            var vm = new OrderViewModel(SelectedEmployee.EmployeeId);

            if (vm.View.ShowDialog() == true) LoadDepartments();
        });

        public RelayCommand OrderEditCommand => new RelayCommand(param =>
        {
            if (!(param is Order order)) return;

            var vm = new OrderViewModel(SelectedEmployee.EmployeeId, order.OrderId);
            if (vm.View.ShowDialog() == true) LoadDepartments();
        });

        public RelayCommand OrderDeleteCommand => new RelayCommand(param =>
        {
            if (!(param is Order order)) return;

            using (var db = new DatabaseContext())
            {
                db.Orders.Remove(order);
                db.SaveChanges();
                SelectedEmployee.Orders.Remove(order);
            }
        });

        public void LoadDepartments()
        {
            using (var db = new DatabaseContext())
            {
                Departments = new ObservableCollection<Department>(
                    db.Departments
                    .Include(d => d.Employees)
                    .ThenInclude(k => k.Orders)
                    .ToList());

                db.Departments.ToList().ForEach(x =>
                    db.Entry(x)
                        .Collection(d => d.Employees)
                            .Load());
            }
        }

        public MainViewModel()
        {
            LoadDepartments();
        }
    }
}