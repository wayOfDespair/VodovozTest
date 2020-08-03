using System;
using System.Windows;
using Vodovoz.Model;
using Vodovoz.View;

namespace Vodovoz.ViewModel
{
    public class OrderViewModel
    {
        public OrderView View { get; set; }
        public Order Order { get; set; }

        public RelayCommand AddCommand => new RelayCommand(delegate
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    if (Order.OrderId > 0) db.Update(Order);
                    else db.Orders.Add(Order);

                    db.SaveChanges();
                }

                View.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });

        public OrderViewModel(int employeeId, int orderId = 0)
        {
            using (var db = new DatabaseContext())
                Order = (orderId == 0)
                    ? new Order()
                    : db.Orders.Find(orderId);

            Order.EmployeeId = employeeId;

            View = new OrderView() { DataContext = this };
        }
    }
}