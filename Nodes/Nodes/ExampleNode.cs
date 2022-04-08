using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Nodes
{
    internal class ExampleNode : Node
    {
        protected float? InputA
        {
            get => (int?)inputs[0].Data;
        }
        protected float? InputB
        {
            get => (float?)inputs[1].Data;
        }
        protected string? Output
        {
            set => outputs[0].Send(value);
        }
        public ExampleNode(NodeManager nodeManager, Vector2 position, SpriteFont font) :  base("Example Node", position, new DataInput<object?, Node>[2], new DataOutput<object?, Node>[1], nodeManager, font)
        {
            inputs[0] = new DataInput<object?, Node>("Input A", this, typeof(int), font);
            inputs[1] = new DataInput<object?, Node>("Input B", this, typeof(float), font);
            outputs[0] = new DataOutput<object?, Node>("Output", this, typeof(string), font);
            
        }
        
        public override void Run()
        {
            Output = (InputA + InputB).ToString();
        }

        public override void GetInput(KeyboardState keyboard)
        {
            throw new NotImplementedException();
        }
    }
}
