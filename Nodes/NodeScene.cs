using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MonoGame.Framework;
using MonoGame.OpenGL;
using Microsoft.Xna.Framework.Input;
using System;
using GameEngine.Input;
namespace Nodes
{
    public sealed class NodeScene : Scene
    {
        private Camera camera;
        public static SpriteFont font;
        public static Texture2D Box { get; private set; }
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private readonly NodeManager nodeManager;
        private InputManager input = new();
        public NodeScene() : base()
        {
            nodeManager = new NodeManager();
            graphics = new(this);
            IsFixedTimeStep = false;
            IsMouseVisible = true;
            
        }

        private void DrawNodes()
        {
            lock (nodeManager.nodes)
            {
                foreach (KeyValuePair<ulong, Node> keyValuePair in nodeManager.nodes)
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
            Window.AllowUserResizing = true;
            Window.AllowAltF4 = false;
            camera = new Camera(Vector2.Zero, 3, GraphicsDevice);
            camera.SetCurrent();
            Box = Content.Load<Texture2D>("node");
            /*
            Box = new Texture2D(GraphicsDevice, 1, 1);
            Box.SetData(new byte[] { byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue });
            */
            //font = Content.Load<SpriteFont>("Arial");
            nodeManager.AddNode(new CoolNode(nodeManager, Vector2.Zero));
            nodeManager.Start();

        }

        protected override void Update(GameTime gameTime)
        {
            input.Update();
            
            if (input.IsKeyPressed(Keys.Escape))
            {
                Exit();
            }
            if (input.IsKeyDown(Keys.W) || input.IsKeyDown(Keys.Up) || input.IsKeyDown(Keys.I))
            {
                camera.position.Y += 0.1f;
            }
            if (input.IsKeyDown(Keys.S) || input.IsKeyDown(Keys.Down) || input.IsKeyDown(Keys.K))
            {
                camera.position.Y -= 0.1f;
            }
            if (input.IsKeyDown(Keys.D) || input.IsKeyDown(Keys.Right) || input.IsKeyDown(Keys.L))
            {
                camera.position.X += 0.1f;
            }
            if (input.IsKeyDown(Keys.A) || input.IsKeyDown(Keys.Left) || input.IsKeyDown(Keys.J))
            {
                camera.position.X -= 0.1f;
            }

            
            if (input.LeftMouseDown)
            {
                camera.position -= camera.PixelToUnitWithoutTranslation(input.MousePositionDelta);
            }
            else if(input.RightMouseClicked || input.MiddleMouseDown)
            {
                nodeManager.AddNode(new CoolNode(nodeManager, camera.PixelToUnit(input.MousePosition)));
            }
            camera.yScale -= input.ScrollWheelDelta / 100f;
            if (camera.yScale < 0.01f)
            {
                camera.yScale = 0.01f;
            }

            
            input.LateUpdate();
            base.Update(gameTime);
        }


        protected override void EndRun()
        {
            if (input.IsKeyDown(Keys.N))
            {
                returnScene = new NodeScene();
            }
            nodeManager.Shutdown();
            base.EndRun();
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MidnightBlue * 0.5f);
            spriteBatch.Begin(SpriteSortMode.BackToFront);
            DrawNodes();
            spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}
