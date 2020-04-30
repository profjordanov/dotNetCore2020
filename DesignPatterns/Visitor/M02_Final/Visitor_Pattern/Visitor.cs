using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor_Pattern
{
    public interface IVisitor
    {
        void VisitBook(Book book);
        void VisitVinyl(Vinyl vinyl);
        void Print();
    }

    public interface IVisitableElement
    {
        void Accept(IVisitor visitor);
    }

    public class DiscountVisitor: IVisitor
    {
        private double _savings;

        public void VisitBook(Book book)
        {
            var discount = 0.0;

            if(book.Price < 20.00)
            {
                discount = book.GetDiscount(0.10);
                Console.WriteLine($"DISCOUNTED: Book #{book.ID} is now ${Math.Round(book.Price - discount, 2)}");
            }
            else
            {
                Console.WriteLine($"FULL PRICE: Book #{book.ID} is ${book.Price}");
            }

            _savings += discount;
        }

        public void VisitVinyl(Vinyl vinyl)
        {
            var discount = vinyl.GetDiscount(0.15);
            Console.WriteLine($"SUPER SAVINGS: Vinyl #{vinyl.ID} is now ${Math.Round(vinyl.Price - discount, 2)}");

            _savings += discount;
        }

        public void Print()
        {
            Console.WriteLine($"\nYou saved a total of ${Math.Round(_savings, 2)} on today's order!");
        }

        public void Reset()
        {
            _savings = 0.0;
        }
    }

    public class SalesVisitor: IVisitor
    {
        private int BookCount = 0;
        private int VinylCount = 0;

        public void VisitBook(Book book)
        {
            BookCount++;
        }

        public void VisitVinyl(Vinyl vinyl)
        {
            VinylCount++;
        }

        public void Print()
        {
            Console.WriteLine($"Books sold: {BookCount} \nVinyl sold: {VinylCount}");
            Console.WriteLine($"The store sold {BookCount + VinylCount} units today!");
        }
    }
}
