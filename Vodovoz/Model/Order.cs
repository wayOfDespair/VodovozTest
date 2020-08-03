using System.ComponentModel.DataAnnotations;

namespace Vodovoz.Model
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public string ProductName { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}