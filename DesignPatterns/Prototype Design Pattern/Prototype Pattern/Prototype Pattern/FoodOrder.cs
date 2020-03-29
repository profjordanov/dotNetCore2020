using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype_Pattern
{
    public abstract class Prototype
    {
        public abstract Prototype ShallowCopy();
        public abstract Prototype DeepCopy();
        public abstract void Debug();
    }

    public class OrderInfo
    {
        public int id;

        public OrderInfo(int id)
        {
            this.id = id;
        }
    }

    public class FoodOrder: Prototype
    {
        public string customerName;
        public bool isDelivery;
        public string[] orderContents;
        public OrderInfo info;

        public FoodOrder(string name, bool delivery, string[] contents, OrderInfo info)
        {
            this.customerName = name;
            this.isDelivery = delivery;
            this.orderContents = contents;
            this.info = info;
        }

        public override Prototype ShallowCopy()
        {
            return (Prototype)this.MemberwiseClone();
        }

        public override Prototype DeepCopy()
        {
            FoodOrder clonedOrder = (FoodOrder)this.MemberwiseClone();
            clonedOrder.info = new OrderInfo(this.info.id);

            return clonedOrder;
        }

        public override void Debug()
        {
            Console.WriteLine("------- Prototype Food Order -------");
            Console.WriteLine("\nName: {0} \nDelivery: {1}", this.customerName, this.isDelivery);
            Console.WriteLine("ID: {0}", this.info.id);
            Console.WriteLine("Order Contents: " + string.Join(",", orderContents) + "\n");
        }
    }

    public class PrototypeManager
    {
        private Dictionary<string, Prototype> _prototypeLibrary = new Dictionary<string, Prototype>();
        
        public Prototype this[string key]
        {
            get { return _prototypeLibrary[key]; }
            set { _prototypeLibrary.Add(key, value); }
        }
    }
}
