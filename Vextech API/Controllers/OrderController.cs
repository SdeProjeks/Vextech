using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vextech_API.DataAccess;
using Vextech_API.Models;
using Vextech_API.Models.ViewModels;

namespace Vextech_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public List<OrderModel> Orders { get; set; }
        public List<OrderProductModel> OrderProducts { get; set; }

        [HttpGet]
        public ActionResult<List<OrderModel>> GetOrder(int orderID, int userID)
        {
            try
            {
                Orders = new();
                OrderProducts = new();
                string sql = $"SELECT orders.ID, orders.Address, orders.PostNumber, orders.Country, orders.Date, orders.OrderStatusID, order_status.Name, orders.UserID, users.Email, users.Firstname, users.Lastname FROM orders INNER JOIN order_status ON orders.OrderStatusID = order_status.ID INNER JOIN users ON orders.UserID = users.ID WHERE orders.ID={orderID} AND orders.UserID={userID};";
                var result = SqlDataAccess.LoadData<VOrderModel>(sql);

                // Checks if we got anything or not.
                if (result.Count == 0)
                {
                    return this.StatusCode(StatusCodes.Status204NoContent, "We could not find your order in the system.");
                }

                sql = $"SELECT order_products.Amount, order_products.Price, products.Name FROM order_products INNER JOIN products ON order_products.ProductID = products.ID WHERE OrderID = {orderID};";
                var OrderProductResult = SqlDataAccess.LoadData<VOrderProductModel>(sql);

                foreach (var orderproduct in OrderProductResult)
                {
                    OrderProductModel OrderProduct = new()
                    {
                        Product = new()
                        {
                            Name = orderproduct.Name
                        },
                        Amount = orderproduct.Amount,
                        Price = orderproduct.Price
                    };

                    OrderProducts.Add(OrderProduct);
                }

                foreach (var order in result)
                {
                    OrderModel Order = new()
                    {
                        ID = order.ID,
                        User = new()
                        {
                            ID = order.UserID,
                            Email = order.Email,
                            Firstname = order.firstname,
                            Lastname = order.lastname
                        },
                        OrderStatus = new()
                        {
                            ID = order.OrderStatusID,
                            Name = order.Name
                        },
                        Address = order.Address,
                        PostNumber = order.PostNumber,
                        Country = order.Country,
                        Products = OrderProducts
                    };

                    Orders.Add(Order);
                }



                return Orders;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public ActionResult CreateOrder(ulong userID, string address, string postnumber, string country, List<VOrderProductModel> products)
        {
            try
            {
                VOrderModel data = new()
                {
                    UserID = userID,
                    Address = address,
                    PostNumber = postnumber,
                    Country = country,
                };

                string sql = @"INSERT INTO orders (UserID, Address, PostNumber, Country) VALUES (@UserID,@Address,@PostNumber,@Country)";
                var result = SqlDataAccess.SaveData<VOrderModel>(sql, data);

                foreach (VOrderProductModel product in products)
                {
                    sql = @"INSERT INTO order_products (OrderID, ProductID, Amount, Price) VALUES ((SELECT MAX(ID) FROM Orders),@ProductID,@Amount,@Price);";
                    SqlDataAccess.SaveData<VOrderProductModel>(sql, product);
                }

                return Ok("Order has been created.");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Your order was not created due to wrong inputs try changing your inputs and try again.");
            }
        }

        [HttpPut]
        public ActionResult UpdateOrderStatus(int orderID, int orderstatusID)
        {
            try
            {
                string sql = $"UPDATE orders SET OrderStatusID={orderstatusID} WHERE ID={orderID};";
                var result = SqlDataAccess.UpdateData(sql);

                return Ok("Order status has been updated.");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, "We could not find the order in the database.");
            }
        }

        [HttpDelete]
        public ActionResult DeleteOrder(int orderID)
        {
            try
            {
                string sql = $"DELETE FROM orders WHERE ID={orderID};";
                var result = SqlDataAccess.DeleteData(sql);

                return Ok("Order was successfully deleted.");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, "We could not find your order in the database.");
            }
        }

        [HttpPost]
        public ActionResult CreateOrderStatus(string ordercategoryName)
        {
            try
            {
                OrderStatusModel data = new()
                {
                    Name = ordercategoryName
                };

                var sql = @"INSERT INTO order_status (Name) VALUES (@Name);";
                var result = SqlDataAccess.SaveData<OrderStatusModel>(sql, data);

                return this.StatusCode(StatusCodes.Status201Created,"Order status has been created.");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Invalid input order category was not created try changing your input.");
            }
        }


        [HttpPut]
        public ActionResult UpdateStatus(int orderstatusID, string ordercategoryName)
        {
            try
            {
                OrderStatusModel data = new()
                {
                    ID = orderstatusID,
                    Name = ordercategoryName
                };

                var sql = @"UPDATE order_status SET Name=@Name WHERE ID=@ID;";
                var result = SqlDataAccess.SaveData<OrderStatusModel>(sql, data);

                return Ok("Order status has been updated.");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, "We could not find your order in the database nothing was updated.");
            }
        }
    }
}
