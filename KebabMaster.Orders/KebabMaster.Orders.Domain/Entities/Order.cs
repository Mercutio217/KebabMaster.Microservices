﻿using System.Net.Mail;
using KebabMaster.Orders.Domain.Entities.Base;
using KebabMaster.Orders.Domain.Exceptions;
using KebabMaster.Orders.Domain.Tools;

namespace KebabMaster.Orders.Domain.Entities;

public class Order : Entity
{
    public string Email { get; private set; }
    public DateTime DateCreated { get; set; }
    public Guid? UserId { get; init; }
    public Address Address { get; private set; }
    public IEnumerable<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();
    private Order() { }

    private Order(string email, Address address, IEnumerable<OrderItem> orderItems)
    {
        Email = email;
        Address = address;
        OrderItems = orderItems;
        DateCreated = DateTime.Now;
    }
    
    private Order(Guid userId, string email, Address address, IEnumerable<OrderItem> orderItems)
    {
        UserId = userId;
        Email = email;
        Address = address;
        OrderItems = orderItems;
        DateCreated = DateTime.Now;

    }
    public static Order Create(string email, Address address, IEnumerable<OrderItem> orderItems)
    {
        EmailValidator.Validate(email);

        return new Order(email, address, orderItems);
    }
    
    public static Order Create(Guid userId, string email, Address address, IEnumerable<OrderItem> orderItems)
    {
        EmailValidator.Validate(email);

        return new Order(userId, email, address, orderItems);
    }
    public void UpdateAddress(Address address)
    {
        Address = address ?? throw new MissingMandatoryPropertyException<Order>(nameof(Address));
    }

    public void UpdateOrderItems(IEnumerable<OrderItem> orderItems)
    {
        OrderItems = orderItems ?? throw new MissingMandatoryPropertyException<Order>(nameof(Address));
    }
}