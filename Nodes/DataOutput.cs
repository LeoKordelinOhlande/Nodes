using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
namespace Nodes
{
    public class DataOutput<TData, TNode> : DataPoint<TData, TNode> where TNode : Node
    {

        private readonly List<DataInput<TData, Node>> inputs;
        public int Count { get; private set; } 

        public bool IsReadOnly { get; } = false;

        public DataInput<TData, Node> this[int index]
        {
            get => inputs[index];
            set
            {
                if (value.type.IsAssignableFrom(type))
                {
                    inputs[index] = value;
                }
            }
        }
        public List<Node> ConnectedNodes()
        {
            List<Node> nodes = new();
            int count = inputs.Count;
            for (int i = 0; i < count; i++)
            {
                if(!nodes.Contains(inputs[i].node))
                {
                    nodes.Add(inputs[i].node);
                }
            }
            return nodes;
        }

        public void Send(TData data)
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                if (inputs[i] != null)
                {
                    inputs[i].SendData(data);
                }
            }
        }

        public int IndexOf(DataInput<TData, Node> item)
        {
            return inputs.IndexOf(item);
        }

        public void Connect(DataInput<TData, Node> item)
        {
            if (item.type.IsAssignableFrom(type))
            {
                
                inputs.Add(item);

                item.Connect(this.node, type);
            }
        }

        public void DisconnectAll()
        {

            for (int i = 0; i < i; i++)
            {

            }
            inputs.Clear();
        }

        public bool Remove(DataInput<TData, TNode> item)
        {
            //return inputs.Remove(item);
            return false;
        }
        public int Disconnect(TNode node)
        {
            int amount = 0;
            int index = 0;
            while (index < inputs.Count)
            {
                if (inputs[index].node.Equals(node))
                {
                    amount++;

                    inputs[index].Disconnect();
                    inputs.RemoveAt(index);
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
            return amount;
        }

        public DataOutput(string name, TNode node, List<DataInput<TData, TNode>> dataInputs, SpriteFont font) : base(name, node, font) 
        {
            //this.inputs = dataInputs;
        }
        public DataOutput(string name, TNode node, SpriteFont font) : this(name, node, new List<DataInput<TData, TNode>>(), font) { }
    }
}
