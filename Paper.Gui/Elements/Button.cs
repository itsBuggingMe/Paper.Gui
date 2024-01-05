using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Paper.Gui.Elements
{
    public class Button : BoundedGui
    {
        private SpriteBatch _spriteBatch;
        private MouseState _prevMouseState;
        private Texture2D _texture;

        public Button(SpriteBatch spriteBatch, SpriteFont font,Texture2D texture, Vector2 Location, Color textColor, string text, Action? OnClick = null) : base(new Rectangle(Location.ToPoint(), texture.Bounds.Size))
        {
            _texture = texture;
            _spriteBatch = spriteBatch;
            float fontSize = GuiHelper.ScaleTextToBounds(font, text, texture.Bounds.Size) * 0.8f;
            AddElement(new Label(spriteBatch, font, text, texture.Bounds.Center.ToVector2(), ElementAlign.Center, textColor, fontSize));

            FallingEdge += OnClick;
        }

        protected bool Down;
        protected bool BoundContains;

        protected Action? RisingEdge;
        protected Action? FallingEdge;

        public override void Tick(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            BoundContains = Bounds.Contains(mouseState.Position);

            Down = mouseState.LeftButton == ButtonState.Pressed && BoundContains;

            if (mouseState.LeftButton != _prevMouseState.LeftButton && BoundContains)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    RisingEdge?.Invoke();
                else
                    FallingEdge?.Invoke();
            }

            _prevMouseState = mouseState;
            base.Tick(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(_texture, Bounds, null, (Down ? Color.Gray : Color.White), 0, Vector2.Zero, SpriteEffects.None, Depth);
            base.Draw(gameTime);
        }
    }
}
