using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
namespace Nodes
{
    public class DataInput<TData, TNode> : DataPoint<TData, TNode> where TNode : Node
    {
        private object? data;
        private volatile bool updated;
        private Node? outputNode;
        public Node? OutputNode => outputNode;
        public void SendData(TData data)
        {
            lock (data)
            {
                this.data = data;
            }
            this.updated = true;
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
        public TData? Read()
        {
            if (updated)
            {
                updated = false;
                return (TData?)data;
            }
            else
            {
                
                return default;
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
        public DataInput(string name, TNode node, SpriteFont font) : base(name, node, font)
        {
            data = null;
            updated = false;
        }


    }
}
