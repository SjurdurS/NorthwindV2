using System;
using System.Linq;

namespace NorthWindNS
{
    public interface IRepository : IDisposable
    {
        /// <summary>
        ///     Returns all products.
        /// </summary>
        /// <returns>Returns all of the Product in the repository</returns>
        IQueryable<Product> GetProducts { get; }

        /// <summary>
        ///     Returns all categories.
        /// </summary>
        /// <returns>Returns all of the Category in the repository.</returns>
        IQueryable<Category> GetCategories { get; }

        /// <summary>
        ///     Returns all orders.
        /// </summary>
        /// <returns>Returns all of the Order in the repository.</returns>
        IQueryable<Order> GetOrders { get; }

        /// <summary>
        ///     Add an order to the repository.
        ///     Must add order id (max id + 1) to the order before storing it.
        /// </summary>
        /// <param name="order">The Order to add to the repository</param>
        /// <returns>Returns the id of the newly added order</returns>
        int CreateOrder(Order order);
    }
}