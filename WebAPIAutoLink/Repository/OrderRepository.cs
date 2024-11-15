﻿using WebAPIAutoLink.Data;
using WebAPIAutoLink.DTO;
using WebAPIAutoLink.Interfaces;
using WebAPIAutoLink.Models;

namespace WebAPIAutoLink.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateOrder(Order order)
        {
            _context.Add(order);
            return Save();
        }

        public bool DeleteOrder(Order order)
        {
            _context.Remove(order);
            return Save();
        }

        public ICollection<Order> GetNotConfirmedOrder()
        {
            return _context.Orders.Where(r => r.IsConfirmed == false).ToList();
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.FirstOrDefault(r => r.Id == id);
        }

        public ICollection<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public ICollection<Order> GetUserOrders(int id)
        {
            return _context.Orders.Where(c => c.User.Id == id).ToList();
        }

        public bool OrderExists(int id)
        {
            return _context.Orders.Any(r => r.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOrder(Order order)
        {
            _context.Update(order);
            return Save();
        }
    }
}
