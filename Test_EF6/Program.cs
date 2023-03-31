using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
namespace Test_EF6
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new MyDbContext())
            {
                var dbcustomer = new List<Customer>
                {
                    new Customer { IdCustomer = 1, CustomerName = "John Doe", OrderList = new List<Order> { new Order { OrderName = "Order 1" } } },
                    new Customer { IdCustomer = 2, CustomerName = "Jane Smith", OrderList = new List<Order> { new Order { OrderName = "Order 2" } } },
                    new Customer { IdCustomer = 3, CustomerName = "Bob Johnson", OrderList = new List<Order> { new Order { OrderName = "Order 3" } } },
                    new Customer { IdCustomer = 4, CustomerName = "Sara Lee", OrderList = new List<Order> { new Order { OrderName = "Order 4" } } },
                    new Customer { IdCustomer = 5, CustomerName = "Alex Brown", OrderList = new List<Order> { new Order { OrderName = "Order 5" } } }
                };
                var customers = from c in dbcustomer
                                orderby c.CustomerName
                                select c;
                foreach (var customer in customers)
                {
                    Console.WriteLine($"Id: {customer.IdCustomer}, Name: {customer.CustomerName}");
                }
                db.Customers.AddRange(customers);
                db.SaveChanges();
            }
        }
    }

    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string OrderName { get; set; }
        public int IdCustomer { get; set; }
        public virtual Customer CustomerObj { get; set; }
    }

    public class Customer
    {
        [Key]
        public int IdCustomer { get; set; }
        public string CustomerName { get; set; }
        public virtual ICollection<Order> OrderList { get; set; }
    }
    public class MyDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }

}
