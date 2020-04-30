using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor_Pattern
{
    public class Item
    {
        public int ID;
        public double Price;

        public Item(int id, double price)
        {
            this.ID = id;
            this.Price = price;
        }

        public double GetDiscount(double percentage)
        {
            return Math.Round(Price * percentage, 2);
        }
    }

    public class Book: Item, IVisitableElement
    {
        public Book(int id, double price) : base(id, price) { }
        public void Accept(IVisitor visitor)
        {
            visitor.VisitBook(this);
        }
    }

    public class Vinyl: Item, IVisitableElement
    {
        public Vinyl(int id, double price) : base(id, price) { }
        public void Accept(IVisitor visitor)
        {
            visitor.VisitVinyl(this);
        }
    }
}
