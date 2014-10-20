using System.Collections.Generic;

namespace Northwind
{
    public interface IRepository
    {
        /// <summary>
        ///     Returns all products.
        /// </summary>
        /// <returns>Returns all of the Products in the repository</returns>
        List<Product> Products();

        /// <summary>
        ///     Returns all categories.
        /// </summary>
        /// <returns>Returns all of the Categories in the repository.</returns>
        List<Category> Categories();


        /// <summary>
        ///     Returns all orders.
        /// </summary>
        /// <returns>Returns all of the Orders in the repository.</returns>
        List<Order> Orders();

        /// <summary>
        ///     Add an order to the repository.
        ///     Must add order id (max id + 1) to the order before storing it.
        /// </summary>
        /// <param name="order">The Order to add to the repository</param>
        /// <returns>Returns the id of the newly added order</returns>
        long CreateOrder(Order order);
    }
}