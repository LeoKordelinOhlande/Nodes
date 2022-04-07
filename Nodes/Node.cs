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
        const float shit = 1 / 1333f;
        protected readonly SpriteFont font;
        protected NodeManager nodeManager;
        protected Vector2 position;
        public readonly string name;
        public readonly float nameWidth;
        public readonly ulong id;
        public readonly DataInput<object?, Node>[] inputs;
        public readonly DataOutput<object?, Node>[] outputs;

        public readonly int maxLengths;
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
            /*List<Node> nodes = new();
            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i].Output != null)
                {
                    nodes.Add(inputs[i].Output.node);
                }
            }
            return nodes;*/
            throw new NotImplementedException();
        }
        public Node(string name, Vector2 position, DataInput<object?, Node>[] inputs, DataOutput<object?, Node>[] outputs, NodeManager nodeManager, SpriteFont font)
        {
            this.font = font;
            Vector2 eouc = font.MeasureString(name);
            nameWidth = eouc.X / eouc.Y;
            this.position = position;
            this.name = name;
            this.outputs = outputs;
            this.inputs = inputs;
            this.maxLengths = (int)MathF.Max(inputs.Length, outputs.Length);

            // Feels like the first time i've had to use a do statement.
            do
            {
                id = nodeManager.random.NextULong();
            } while (nodeManager.IdExists(id));
            this.nodeManager = nodeManager;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            float maxWidth = nameWidth;

            for (int i = 0; i < maxLengths; i++)
            {
                float currentWidth = 0;
                if (inputs.Length > i)
                {
                    currentWidth += inputs[i].bounds + 0.25f;
                }
                if (outputs.Length > i)
                {
                    currentWidth += outputs[i].bounds + 0.25f;
                }
                maxWidth = MathF.Max(currentWidth, maxWidth);
            }
            maxWidth += 1f;

            spriteBatch.Draw(
                texture: NodeScene.Box,
                position: Camera.Current.UnitToPixel(position),
                sourceRectangle: null,
                color: Color.DarkGray,
                rotation: 0,
                origin: NodeScene.Box.Bounds.Size.ToVector2() / 2,
                scale: new Vector2(maxWidth, maxLengths + 1) * Camera.Current.PixelsPerUnit,
                effects: SpriteEffects.None,
                layerDepth: 0f);

            spriteBatch.Draw(
                texture: NodeScene.Box,
                position: Camera.Current.UnitToPixel(position + new Vector2(0, (maxLengths + 1)/2f - 0.5f)),
                sourceRectangle: null,
                color: Color.Gray,
                rotation: 0,
                origin: NodeScene.Box.Bounds.Size.ToVector2() / 2,
                scale: new Vector2(maxWidth, 1) * Camera.Current.PixelsPerUnit,
                effects: SpriteEffects.None,
                layerDepth: 0.1f);

            spriteBatch.DrawString(
               spriteFont: font,
               text: name,
               position: Camera.Current.UnitToPixel(position + new Vector2(-maxWidth / 2f, (maxLengths + 1) / 2f)),
               color: Color.Black,
               rotation: 0,
               origin: Vector2.Zero,
               scale: shit * Camera.Current.PixelsPerUnit,
               effects: SpriteEffects.None,
               layerDepth: 0.2f);

            for (int i = 0; i < inputs.Length; i++)
            {
                spriteBatch.Draw(
                    texture: NodeScene.Box,
                    position: Camera.Current.UnitToPixel(position + new Vector2(-maxWidth / 2f, ((maxLengths + 1) / 2f) - 1.5f - i)),
                    sourceRectangle: null,
                    color: Color.CornflowerBlue,
                    rotation: 0,
                    origin: NodeScene.Box.Bounds.Size.ToVector2() / 2,
                    scale: new Vector2(0.25f) * Camera.Current.PixelsPerUnit,
                    effects: SpriteEffects.None,
                    layerDepth: 0.1f);

                spriteBatch.DrawString(
                    spriteFont: font,
                    text: inputs[i].name,
                    position: Camera.Current.UnitToPixel(position + new Vector2(-maxWidth / 2f + 0.25f, ((maxLengths + 1) / 2f) - 1f - i)),
                    color: Color.Black,
                    rotation: 0,
                    origin: Vector2.Zero,
                    scale: shit * Camera.Current.PixelsPerUnit,
                    effects: SpriteEffects.None,
                    layerDepth: 0.1f);
            }
            for (int i = 0; i < outputs.Length; i++)
            {
                spriteBatch.Draw(
                    texture: NodeScene.Box,
                    position: Camera.Current.UnitToPixel(position + new Vector2(maxWidth / 2f, ((maxLengths + 1) / 2f) - 1.5f - i)),
                    sourceRectangle: null,
                    color: Color.CornflowerBlue,
                    rotation: 0,
                    origin: NodeScene.Box.Bounds.Size.ToVector2() / 2,
                    scale: new Vector2(0.25f) * Camera.Current.PixelsPerUnit,
                    effects: SpriteEffects.None,
                    layerDepth: 0.1f);

                spriteBatch.DrawString(
                    spriteFont: font,
                    text: outputs[i].name,
                    position: Camera.Current.UnitToPixel(position + new Vector2(maxWidth / 2f - 0.25f - outputs[i].bounds, ((maxLengths + 1) / 2f) - 1f - i)),
                    color: Color.Black,
                    rotation: 0,
                    origin: Vector2.Zero,
                    scale: shit * Camera.Current.PixelsPerUnit,
                    effects: SpriteEffects.None,
                    layerDepth: 0.1f);
            }

            //Vector2 gaming = NodeApp.font.MeasureString(name);
            //float multiplication = (1 / gaming.X * 1000);
            //float multiplication = 1;
            //spriteBatch.DrawString(NodeApp.font, name, position, Color.Black, 0, Vector2.Zero, new Vector2(multiplication), SpriteEffects.None, 0); ;

        }
        public abstract void Run();
    }
}
