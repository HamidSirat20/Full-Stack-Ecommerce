using AutoMapper;
using backend.Business.src.Common;
using backend.Business.src.Dtos;
using backend.Business.src.Interfaces;
using backend.Domain.src.Entities;
using backend.Domain.src.RepoInterfaces;

namespace backend.Business.src.Implementations;

public class OrderItemService
    : BaseService<OrderItem, OrderItemReadDto, OrderItemCreateDto, OrderItemUpdateDto>,
        IOrderItemService
{
    private readonly IOrderItemRepo _orderItemRepo;
    private readonly IProductRepo _productRepo;
    private readonly IOrderRepo _orderRepo;

    public OrderItemService(
        IOrderItemRepo orderItemRepo,
        IMapper mapper,
        IProductRepo productRepo,
        IOrderRepo orderRepo
    )
        : base(orderItemRepo, mapper)
    {
        _orderItemRepo = orderItemRepo;
        _productRepo = productRepo;
        _orderRepo = orderRepo;
    }

    public async Task<OrderItem> CreateOrderItem(OrderItem entity)
    {
        var product = await _productRepo.GetOneById(entity.Product.Id);

        var createdOrderItem = await _orderItemRepo.CreateOne(entity);

        return createdOrderItem;
    }

    public override async Task<OrderItemReadDto> CreateOne(OrderItemCreateDto dto)
    {
        var orderProduct = _mapper.Map<OrderItem>(dto);
        var product = await _productRepo.GetOneById(dto.ProductId);

        if (product == null || product.Inventory < dto.Amount)
        {
            throw CustomErrorHandler.NotFoundException(
                "Product not found or not enough for your order"
            );
        }
        return _mapper.Map<OrderItemReadDto>(await _orderItemRepo.CreateOne(orderProduct));
    }
}
