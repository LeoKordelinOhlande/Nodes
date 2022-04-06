using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodes
{
    public abstract class DataPoint<TData, TNode> where TNode : Node
    {
        public readonly string name;
        public readonly TNode node;
        public readonly Type type;
        public DataPoint(string name, TNode node)
        {
            this.name = name;
            this.node = node;
            type = typeof(TData);
        }
        public DataPoint(string name, TNode node, Type type)
        {
            this.name = name;
            this.node = node;
            this.type = type;
        }
    }
}
