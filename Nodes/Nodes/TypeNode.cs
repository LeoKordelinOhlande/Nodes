using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Nodes
{
    public sealed class TypeNode<T> : Node
    {
        private readonly float inputBoxWidth;
        private StringBuilder text;
        public delegate T? Parser(string input);
        private readonly Parser parser;
        private T? Output
        {
            set => outputs[0].Send(value);
        }

        public TypeNode(NodeManager nodeManager, Vector2 position, SpriteFont font, Parser parser) : base($"{typeof(T).Name} Node", position, Array.Empty<DataInput<object?, Node>>(), new DataOutput<object?, Node>[1], nodeManager, font)
        {
            this.parser = parser;
            outputs[0] = new DataOutput<object?, Node>("Output", this, typeof(T), font);
        }
        public override void CalculateSize()
        {
            base.CalculateSize();

        }
        public override void GetInput(KeyboardState keyboard)
        {
            Keys[] keys = keyboard.GetPressedKeys();
            for (int i = 0; i < keys.Length; i++)
            {
                text.Append((char)keys[i]);
            }
        }
        public override void Run()
        {
            Output = parser(text.ToString());
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
