using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind_Part_3
{
    interface IRepository
    {
        /// <summary>
        ///     Returns all products.
        /// </summary>
        /// <returns>Returns all of the Product in the repository</returns>
        DbSet<Product> Products();

        /// <summary>
        ///     Returns all categories.
        /// </summary>
        /// <returns>Returns all of the Category in the repository.</returns>
        DbSet<Category> Categories();


        /// <summary>
        ///     Returns all orders.
        /// </summary>
        /// <returns>Returns all of the Order in the repository.</returns>
        DbSet<Order> Orders();

        /// <summary>
        ///     Add an order to the repository.
        ///     Must add order id (max id + 1) to the order before storing it.
        /// </summary>
        /// <param name="order">The Order to add to the repository</param>
        /// <returns>Returns the id of the newly added order</returns>
        long CreateOrder(Order order);
    }
}
