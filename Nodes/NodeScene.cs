using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MonoGame.Framework;
using MonoGame.OpenGL;
using Microsoft.Xna.Framework.Input;
using System;

namespace Nodes
{
    public sealed class NodeScene : Scene
    {
        int scroll;
        int oldScroll;
        int deltaScroll;
        private Camera camera;
        public static SpriteFont font;
        public static Texture2D Box { get; private set; }
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private readonly NodeManager nodeManager;
        public NodeScene() : base()
        {
            nodeManager = new NodeManager();
            graphics = new(this);
            IsFixedTimeStep = false;
        }
        
        private void DrawNodes()
        {
            lock (nodeManager.nodes)
            {
                foreach (KeyValuePair<ulong,Node> keyValuePair in nodeManager.nodes)
                {
                    keyValuePair.Value.Draw(spriteBatch);
                }
            }
            
        }
        protected override void Initialize()
        {
            base.Initialize();
            
        }
        
        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";
            spriteBatch = new(GraphicsDevice);
            base.LoadContent();

            camera = new Camera(Vector2.Zero, 10, GraphicsDevice.Viewport);
            camera.SetCurrent();
            
            Box = new Texture2D(GraphicsDevice, 1, 1);
            Box.SetData(new byte[] { byte.MaxValue,byte.MaxValue,byte.MaxValue, byte.MaxValue });
            font = Content.Load<SpriteFont>("Arial");
            nodeManager.AddNode(new CoolNode(nodeManager));
            nodeManager.Start();

        }
        
        protected override void Update(GameTime gameTime)
        {
            scroll = Mouse.GetState().ScrollWheelValue;
            deltaScroll = scroll - oldScroll;


            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                camera.position.Y += 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                camera.position.Y -= 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                camera.position.X += 0.1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                camera.position.X -= 0.1f;
            }
            camera.yScale += deltaScroll / 100f;


            oldScroll = scroll;
            base.Update(gameTime);
        }


        protected override void EndRun()
        {
            if (camera.position != Vector2.Zero)
            {
                returnScene = new NodeScene();
            }
            nodeManager.Shutdown();
            base.EndRun();
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Wheat);
            spriteBatch.Begin(SpriteSortMode.BackToFront);
            DrawNodes();
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
