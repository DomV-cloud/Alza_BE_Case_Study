using Application.Interfaces.Repository.CustomerRepository;
using Application.Interfaces.Repository.ItemRepository;
using Application.Interfaces.Repository.OrderRepository;
using Contracts.Requests.CreateOrder;
using Contracts.Requests.PayOrder;
using Contracts.Responses.CreateOrder;
using Contracts.Responses.GetAllOrders;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alza_BE_Case_Study.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class OrderController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderController(
            ICustomerRepository customerRepository,
            IItemRepository itemRepository,
            IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _itemRepository = itemRepository;
            _orderRepository = orderRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreatedOrderResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                var customer = _customerRepository.GetCustomerByCustomerName(request.CustomerName);
                if (customer == null)
                {
                    return NotFound("Customer not found.");
                }

                var items = await _itemRepository.GetItemBasedOnOrderId(request.ProductIds);
                if (items is null || items.Count != request.ProductIds.Count )
                {
                    return BadRequest("One or more items not found.");
                }

                var order = new Order
                {
                    OrderNumber = request.OrderNumber,
                    CustomerId = customer.Id,
                    OrderDate = DateTime.Now,
                    OrderState = OrderState.New,
                    OrderItems = items.Select(item => new OrderItem
                    {
                        ItemId = item.Id,
                    }).ToList()
                };

                var createdOrder = await _orderRepository.CreateOrder(order);

                if (createdOrder is null)
                {
                    return StatusCode(500, "An error occurred while creating the order.");
                }

                var response = new CreatedOrderResponse
                {
                    CustomerName = customer.CustomerName,
                    OrderDate = createdOrder.OrderDate,
                    OrderState = createdOrder.OrderState.ToString(),
                    Items = createdOrder.OrderItems.Select(oi => new CreatedOrderItemResponse
                    {
                        ItemName = oi.Item.ItemName,
                        NumberOfItems = oi.Item.NumberOfItems,
                        Price = oi.Item.Price
                    }).ToList()
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllOrderWithItemsResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var listOfAllOrders = await _orderRepository.GetAllOrders();

                if (listOfAllOrders.Count == 0 || listOfAllOrders is null)
                {
                    return NotFound("No orders found");
                }

                var response = listOfAllOrders.Select(order => new GetAllOrderWithItemsResponse
                {
                    Id = order.Id,
                    OrderNumber = order.OrderNumber,
                    OrderDate = order.OrderDate,
                    OrderState = order.OrderState.ToString(),
                    Items = order.OrderItems.Select(oi => new GetAllOrderItemResponse
                    {
                        ItemName = oi.Item.ItemName,
                        Price = oi.Item.Price
                    }).ToList()
                }).ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("pay")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PayOrder([FromBody] PayOrderRequest request)
        {
            try
            {
                var orderToPay = await _orderRepository.UpdateOrderState(request.OrderNumber, request.IsPaid);

                if (orderToPay is null)
                {
                    return NotFound("No orderd found with that order number");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
