using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositeDemo.Structural
{
    public class Composite : Component
    {
        private List<Component> children = new List<Component>();

        public Composite(string name) : base(name)
        {
        }

        public void Add(Component c)
        {
            this.children.Add(c);
        }

        public override void PrimaryOperation(int depth)
        {
            Console.WriteLine(new String('-', depth) + this.Name);
            foreach (var component in this.children)
            {
                component.PrimaryOperation(depth + 2);
            }
        }

        public void Remove(Component c)
        {
            this.children.Remove(c);
        }
    }
}
