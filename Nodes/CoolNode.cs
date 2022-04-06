using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Nodes
{
    internal class CoolNode : Node
    {
        protected int? Input
        {
            get => 0; //(int?)inputs[0].data;
        }
        protected string? Output
        {
            set => outputs[0].Send(value, nodeManager.tick);
        }
        public CoolNode(NodeManager nodeManager, Vector2 position) :  base("The cool", position, new DataInput<object?, Node>[1], new DataOutput<object?, Node>[1], nodeManager)
        {
            inputs[0] = new DataInput<object?, Node>("Input", this, typeof(int));
            outputs[0] = new DataOutput<object?, Node>("Output", this, typeof(string));
        }
        
        public override void Run()
        {
            Output = (Input % 128).ToString();
        }
    }
}
