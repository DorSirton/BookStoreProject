using BookStore.Models;
using BookStore.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace BookStore.Data
{
    public interface IRepositery<T> where T : IDataEntity
    {
        Guid Insert(T item);
        bool Delete(Guid id);
        T Edit(T item);
        T Get(Guid id);
        IEnumerable<Prodact> GetAllProdact(Predicate<Prodact> filter = null);
        Guid InsertSale(T item);
        Guid RemoveSale(T item);
        Guid InsertPurchase(T item);
        void InsertExistProdact(Prodact item);

    }
}
