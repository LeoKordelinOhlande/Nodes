using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace GameEngine.Input
{
    public struct InputManager
    {
        private KeyboardState keyboardState;
        private KeyboardState lateKeyboardState;
        private MouseState mouseState;
        private MouseState lateMouseState;
        private int keyCount;
        private bool keysAreZero;
        private int scrollWheelDelta;
        private Vector2 mousePos;
        private Vector2 lateMousePos;
        private Vector2 mousePosDelta;
        public void Update()
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            keyCount = keyboardState.GetPressedKeyCount();
            
            keysAreZero = keyCount > 0;

            mousePos = new(mouseState.X, mouseState.Y);
            lateMousePos = new(lateMouseState.X, lateMouseState.Y);
            mousePosDelta = mousePos - lateMousePos;

            scrollWheelDelta = mouseState.ScrollWheelValue - lateMouseState.ScrollWheelValue;
        }

        public void LateUpdate()
        {
            lateKeyboardState = keyboardState;
            lateMouseState = mouseState;
        }

        public bool IsKeyDown(Keys keys) => keyboardState.IsKeyDown(keys);
        public bool IsKeyPressed(Keys keys) => keyboardState.IsKeyDown(keys) && !lateKeyboardState.IsKeyDown(keys);

        public int AmountOfKeysPressed => keyCount;
        public bool AnyKeyDown => keysAreZero;

        public bool LeftMouseDown => mouseState.LeftButton == ButtonState.Pressed;
        public bool LeftMouseClicked => mouseState.LeftButton == ButtonState.Pressed && lateMouseState.LeftButton == ButtonState.Released;
        public bool RightMouseDown => mouseState.RightButton == ButtonState.Pressed;

        public bool RightMouseClicked => mouseState.RightButton == ButtonState.Pressed && lateMouseState.RightButton == ButtonState.Released;

        public bool MiddleMouseDown => mouseState.MiddleButton == ButtonState.Pressed;

        public bool MiddleMouseUp => mouseState.MiddleButton == ButtonState.Released;

        public bool ScrollWheelClicked => mouseState.MiddleButton == ButtonState.Pressed && lateMouseState.MiddleButton == ButtonState.Released;

        public int ScrollWheelValue => mouseState.ScrollWheelValue;

        public int ScrollWheelDelta => scrollWheelDelta;

        public Vector2 MousePosition => mousePos;
        public Vector2 LateMousePosition => lateMousePos;
        public Vector2 MousePositionDelta => mousePosDelta;
    }
}