using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Nodes
{
    public class Camera
    {
        private Viewport graphicsDeviceViewport;
        public float PixelsPerUnit => graphicsDeviceViewport.Height / yScale;
        public static Camera? Current { get; private set; }
        public Vector2 position;
        public float yScale;
        public Vector2 Scale => new(graphicsDeviceViewport.AspectRatio * yScale, yScale);
        public void SetCurrent() => Current = this;
        public Vector2 UnitToPixel(Vector2 unitPosition) => UnitToPixel(unitPosition, PixelsPerUnit);
        public Vector2 UnitToPixel(Vector2 unitPosition, float pixelsPerUnit) => ((unitPosition - position) * new Vector2(1, -1) + (Scale / 2)) * pixelsPerUnit;
        //public Vector2 PixelToUnit(Vector2 pixelPosition) => ((pixelPosition / PixelsPerUnit) - (Scale / 2)) * new Vector2(1, -1);
        public Camera(Vector2 position, float yScale, Viewport graphicsDeviceViewport)
        {
            this.position = position;
            this.yScale = yScale;
            this.graphicsDeviceViewport = graphicsDeviceViewport;
        }
        
        
        
    }
}
