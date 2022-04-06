using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PcapDotNet.TestUtils;
namespace Nodes
{
    public abstract class Node
    {
        protected NodeManager nodeManager;
        protected Vector2 position;
        public readonly string name;
        public readonly ulong id;
        public readonly DataInput<object?, Node>[] inputs;
        public readonly DataOutput<object?, Node>[] outputs;
        public List<Node> Forward()
        {
            List<Node> result = new();
            for (int outputIndex = 0; outputIndex < outputs.Length; outputIndex++)
            {
                List<Node> outputArray = outputs[outputIndex].ConnectedNodes();
                int gamer = outputArray.Count;
                for (int outputArrayIndex = 0; outputArrayIndex < gamer; outputArrayIndex++)
                {
                    if (!result.Contains(outputArray[outputArrayIndex]))
                    {
                        result.Add(outputArray[outputArrayIndex]);
                    }
                }
            }
            return result;
        }
        public List<Node> Backward()
        {
            List<Node> nodes = new();
            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i].Output != null)
                {
                    nodes.Add(inputs[i].Output.node);
                }
            }
            return nodes;
        }
        public Node(string name, DataInput<object?, Node>[] inputs, DataOutput<object?, Node>[] outputs, NodeManager nodeManager)
        {
            this.name = name;
            this.outputs = outputs;
            this.inputs = inputs;
            

            // Feels like the first time i've had to use a do statement.
            do
            {
                id = nodeManager.random.NextULong();
            } while (nodeManager.IdExists(id));
            this.nodeManager = nodeManager;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            float pixelsPerUnit = Camera.Current.PixelsPerUnit;
            spriteBatch.Draw(
                texture: NodeScene.Box,
                position: Camera.Current.UnitToPixel(position,pixelsPerUnit),
                sourceRectangle: null,
                color: Color.Red,
                rotation: 0,
                origin: NodeScene.Box.Bounds.Size.ToVector2() / 2,
                scale: new Vector2(1, 1) * pixelsPerUnit,
                effects: SpriteEffects.None,
                layerDepth: 0f);
            
            //Vector2 gaming = NodeApp.font.MeasureString(name);
            //float multiplication = (1 / gaming.X * 1000);
            //float multiplication = 1;
            //spriteBatch.DrawString(NodeApp.font, name, position, Color.Black, 0, Vector2.Zero, new Vector2(multiplication), SpriteEffects.None, 0); ;

        }
        public abstract void Run();
    }
}
