using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Nodes
{
    public class Camera
    {
        private GraphicsDevice graphics;
        public float PixelsPerUnit => graphics.Viewport.Height / yScale;
        public static Camera Current { get; private set; } = new Camera(Vector2.Zero, 10, null);
        public Vector2 position;
        public float yScale;
        public Vector2 Scale => new(graphics.Viewport.AspectRatio * yScale, yScale);
        public void SetCurrent() => Current = this;
        public Vector2 UnitToPixel(Vector2 unitPosition) => UnitToPixel(unitPosition, PixelsPerUnit);
        public Vector2 UnitToPixel(Vector2 unitPosition, float pixelsPerUnit) => (((unitPosition - position) * new Vector2(1, -1) + (Scale / 2)) * pixelsPerUnit);
        public Vector2 PixelToUnit(Vector2 pixelPosition) => (((pixelPosition / PixelsPerUnit) - (Scale / 2)) * new Vector2(1, -1)) + position;
        public Vector2 PixelToUnitWithoutTranslation(Vector2 pixelPosition) => (pixelPosition * new Vector2(1, -1)) / PixelsPerUnit;
        public Camera(Vector2 position, float yScale, GraphicsDevice graphics)
        {
            this.position = position;
            this.yScale = yScale;
            this.graphics = graphics;
        }
        
        
        
    }
}
