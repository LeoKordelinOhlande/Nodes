using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nodes
{
    public abstract class DataPoint<TData, TNode> where TNode : Node
    {
        public readonly string name;
        public readonly float nameWidth;
        public readonly float typeWidth;
        public readonly TNode node;
        public readonly Type type;
        public ulong id;
        public DataPoint(string name, TNode node, SpriteFont font) : this(name, node, typeof(TData), font) { }
        public DataPoint(string name, TNode node, Type type, SpriteFont font)
        {
            
            {
                Vector2 bounds = font.MeasureString(name);
                this.nameWidth = bounds.X / bounds.Y * 0.75f;
            }
            {
                Vector2 bounds = font.MeasureString(type.Name);
                this.typeWidth = bounds.X / bounds.Y * 0.25f;
            }
            this.name = name;
            this.node = node;
            this.type = type;
        }
    }
}
