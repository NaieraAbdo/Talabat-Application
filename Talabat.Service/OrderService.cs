using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository basketRepo;
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<DeliveryMethod> deliveryMethodRepo;
        private readonly IGenericRepository<Order> orderRepo;

        public OrderService(IBasketRepository basketRepo
            ,IGenericRepository<Product> productRepo,
            IGenericRepository<DeliveryMethod>  DeliveryMethodRepo,
            IGenericRepository<Order> orderRepo
            )
        {
            this.basketRepo = basketRepo;
            this.productRepo = productRepo;
            deliveryMethodRepo = DeliveryMethodRepo;
            this.orderRepo = orderRepo;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            //1.Get Basket Froom Basket Repo
            var Basket = await basketRepo.GetBasketAsync(basketId);
            //2.Get Selected Items at Basket From Product Repo
            var OrderItems = new List<OrderItem>();
            if (Basket?.Items.Count > 0)
            {
                foreach(var item in Basket.Items)
                {
                    var Product = await productRepo.GetByIdAsync(item.Id);
                    var ProductItemOrdered = new ProductItemOrder( Product.Name, item.Id, Product.PictureUrl);
                    var OrderItem = new OrderItem(ProductItemOrdered,  Product.Price, item.Quantity);
                    OrderItems.Add(OrderItem);
                }
            }
            //3.Calculate SubTotal 
            var SubTotal = OrderItems.Sum(O => O.Price * O.Quantity);

            //4.Get Delivery Method
            var DeliveryMethod = await deliveryMethodRepo.GetByIdAsync(deliveryMethodId);

            //5.Create Order
            var Order = new Order(buyerEmail, shippingAddress, DeliveryMethod, OrderItems, SubTotal);

            //6.Add Order Locally
            await orderRepo.Add(Order);

            //7.Save Order to DB

            //8.Return 
            return Order;
        }

        public Task<Order> GetOrderByIdForSpecificUser(string buyerEmail, int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForSpecificUser(string buerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
