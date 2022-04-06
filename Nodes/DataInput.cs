using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodes
{
    public class DataInput<TData, TNode> : DataPoint<TData, TNode> where TNode : Node
    {
        private object? data;
        private volatile bool tick;
        private Node? outputNode;
        public Node? OutputNode => outputNode;
        public void SendData(TData data, bool tick)
        {
            lock (data)
            {
                this.data = data;
            }
            this.tick = tick;
        }
        public bool Connect(Node node, Type type)
        {
            lock (outputNode)
            {
                bool gaming = type.IsAssignableTo(this.type);

                if (gaming)
                {
                    outputNode = node;
                }
                return gaming;
            }
        }
        public void Disconnect()
        {
            if (outputNode != null)
            {
                lock (outputNode)
                {
                    outputNode = null;
                }
            }
        }
        public DataInput(string name, TNode node, Type type) : base(name, node, type)
        {
            data = null;
            tick = false;
        }


    }
}
