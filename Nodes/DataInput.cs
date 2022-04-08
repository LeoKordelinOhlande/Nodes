using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.TestUtils;
using Microsoft.Xna.Framework.Graphics;
namespace Nodes
{
    public sealed class DataInput<TData, TNode> : DataPoint<TData, TNode> where TNode : Node
    {
        private object? data;
        public object? Data
        { 
            get
            {
                Updated = false;
                return data;
            }
        }
        public bool Updated { get; private set; }
        private Node? outputNode;
        public Node? OutputNode => outputNode;
        public void SendData(TData data)
        {
            lock (data)
            {
                this.data = data;
            }
            Updated = true;
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
        public DataInput(NodeManager nodeManager, string name, TNode node, Type type, SpriteFont font) : base(name, node, type, font)
        {
            do
            {
                this.id = nodeManager.random.NextULong();
            } while (nodeManager.IdExists(id));
            data = null;
            Updated = false;
        }


    }
}
