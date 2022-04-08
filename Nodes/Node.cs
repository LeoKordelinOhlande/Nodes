using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PcapDotNet.TestUtils;
namespace Nodes
{
    public abstract class Node
    {
        const float idSize = 0.125f;
        const float shit = 1 / 16f;
        const float padding = 0.5f;
        public Vector2 size;
        protected readonly SpriteFont font;
        protected NodeManager nodeManager;
        protected Vector2 position;
        public readonly string name;
        public readonly float nameWidth;
        public readonly ulong id;
        public readonly string idString;
        public readonly float idWidth;
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
            {
                Vector2 eouc = font.MeasureString(name);
                nameWidth = eouc.X / eouc.Y;
            }
            
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
            idString = $"ID: {id}";
            {
                Vector2 bounds = font.MeasureString(idString);
                idWidth = bounds.X / bounds.Y * idSize;
            }
            this.nodeManager = nodeManager;
        }

        public abstract void GetInput(KeyboardState keyboard);
        public virtual void Initialize()
        {
            CalculateSize();
        }
        public virtual void OnClick()
        {
            throw new NotImplementedException("woops, you have to put the cd in your computer");
        }
        public virtual void CalculateSize()
        {
            size.X = nameWidth;

            for (int i = 0; i < maxLengths; i++)
            {
                float currentNameWidth = 0;
                float currentTypeWidth = 0;
                if (inputs.Length > i)
                {
                    currentNameWidth += inputs[i].nameWidth + 0.25f;
                    currentTypeWidth += inputs[i].typeWidth + 0.25f;
                }
                if (outputs.Length > i)
                {
                    currentNameWidth += outputs[i].nameWidth + 0.25f;
                    currentTypeWidth += outputs[i].typeWidth + 0.25f;
                }
                size.X = MathF.Max(MathF.Max(currentNameWidth, currentTypeWidth), size.X);   
            }
            size.X += padding;
            size.Y = maxLengths + 1;

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(
                texture: NodeScene.Box,
                position: Camera.Current.UnitToPixel(position),
                sourceRectangle: null,
                color: Color.DarkGray,
                rotation: 0,
                origin: NodeScene.Box.Bounds.Size.ToVector2() / 2,
                scale: size * Camera.Current.PixelsPerUnit,
                effects: SpriteEffects.None,
                layerDepth: 0f);

            spriteBatch.Draw(
                texture: NodeScene.Box,
                position: Camera.Current.UnitToPixel(position + new Vector2(0, (size.Y) / 2f - 0.5f)),
                sourceRectangle: null,
                color: Color.Gray,
                rotation: 0,
                origin: NodeScene.Box.Bounds.Size.ToVector2() / 2,
                scale: new Vector2(size.X, 1) * Camera.Current.PixelsPerUnit,
                effects: SpriteEffects.None,
                layerDepth: 0.1f);

            spriteBatch.DrawString(
               spriteFont: font,
               text: name,
               position: Camera.Current.UnitToPixel(position + size * new Vector2(-0.5f,0.5f)),
               color: Color.Black,
               rotation: 0,
               origin: Vector2.Zero,
               scale: shit * Camera.Current.PixelsPerUnit,
               effects: SpriteEffects.None,
               layerDepth: 0.2f);

            spriteBatch.DrawString(
               spriteFont: font,
               text: idString,
               position: Camera.Current.UnitToPixel(position + (size * 0.5f) + new Vector2(-idWidth, 0)),
               color: Color.DarkGray,
               rotation: 0,
               origin: Vector2.Zero,
               scale: shit * Camera.Current.PixelsPerUnit * idSize,
               effects: SpriteEffects.None,
               layerDepth: 0.2f);


            for (int i = 0; i < inputs.Length; i++)
            {
                spriteBatch.Draw(
                    texture: NodeScene.Box,
                    position: Camera.Current.UnitToPixel(position + (size * new Vector2(-0.5f,0.5f)) + new Vector2(0,-1.5f - i)),
                    sourceRectangle: null,
                    color: Color.CornflowerBlue,
                    rotation: 0,
                    origin: NodeScene.Box.Bounds.Size.ToVector2() / 2,
                    scale: new Vector2(0.5f) * Camera.Current.PixelsPerUnit,
                    effects: SpriteEffects.None,
                    layerDepth: 0.1f);

                spriteBatch.DrawString(
                    spriteFont: font,
                    text: inputs[i].name,
                    position: Camera.Current.UnitToPixel(position + size * new Vector2(-0.5f,0.5f) + new Vector2(.25f, -1f -i)),
                    color: Color.Black,
                    rotation: 0,
                    origin: Vector2.Zero,
                    scale: shit * Camera.Current.PixelsPerUnit * 0.75f,
                    effects: SpriteEffects.None,
                    layerDepth: 0.1f);

                spriteBatch.DrawString(
                    spriteFont: font,
                    text: inputs[i].type.Name,
                    position: Camera.Current.UnitToPixel(position + size * new Vector2(-0.5f, 0.5f) + new Vector2(.25f, -1.75f - i)),
                    color: Color.Gray,
                    rotation: 0,
                    origin: Vector2.Zero,
                    scale: shit * Camera.Current.PixelsPerUnit * 0.25f,
                    effects: SpriteEffects.None,
                    layerDepth: 0.1f);
            }
            for (int i = 0; i < outputs.Length; i++)
            {
                spriteBatch.Draw(
                    texture: NodeScene.Box,
                    position: Camera.Current.UnitToPixel(position + size * new Vector2(0.5f, 0.5f) + new Vector2(0f, -1.5f - i)),
                    sourceRectangle: null,
                    color: Color.CornflowerBlue,
                    rotation: 0,
                    origin: NodeScene.Box.Bounds.Size.ToVector2() / 2,
                    scale: new Vector2(0.5f) * Camera.Current.PixelsPerUnit,
                    effects: SpriteEffects.None,
                    layerDepth: 0.1f);

                spriteBatch.DrawString(
                    spriteFont: font,
                    text: outputs[i].name,
                    position: Camera.Current.UnitToPixel(position + size * new Vector2(0.5f) + new Vector2(-0.25f - outputs[i].nameWidth, -1f - i)),
                    color: Color.Black,
                    rotation: 0,
                    origin: Vector2.Zero,
                    scale: shit * Camera.Current.PixelsPerUnit * 0.75f,
                    effects: SpriteEffects.None,
                    layerDepth: 0.1f);
                spriteBatch.DrawString(
                    spriteFont: font,
                    text: outputs[i].type.Name,
                    position: Camera.Current.UnitToPixel(position + size * new Vector2(0.5f) + new Vector2(-0.25f - outputs[i].typeWidth, -1.75f - i)),
                    color: Color.Gray,
                    rotation: 0,
                    origin: Vector2.Zero,
                    scale: shit * Camera.Current.PixelsPerUnit * 0.25f,
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
