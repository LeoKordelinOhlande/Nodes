using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Nodes
{
    internal class CoolNode : Node
    {
        protected int? Input
        {
            get => (int?)inputs[0].Read();
        }
        protected string? Output
        {
            set => outputs[0].Send(value);
        }
        public CoolNode(NodeManager nodeManager, Vector2 position, SpriteFont font) :  base(":)", position, new DataInput<object?, Node>[2], new DataOutput<object?, Node>[1], nodeManager, font)
        {
            inputs[0] = new DataInput<object?, Node>("Input 1", this, font);
            inputs[1] = new DataInput<object?, Node>("Input 2", this, font);
            outputs[0] = new DataOutput<object?, Node>("Output", this, font);
            
        }
        
        public override void Run()
        {
            Output = (Input % 128).ToString();
        }
    }
}
