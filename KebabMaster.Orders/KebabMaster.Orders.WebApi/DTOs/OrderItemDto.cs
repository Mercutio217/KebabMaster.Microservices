﻿namespace KebabMaster.Orders.DTOs;

public class OrderItemDto
{
    public Guid MenuItemId { get; set; }
    public int Quantity { get; set; }
}