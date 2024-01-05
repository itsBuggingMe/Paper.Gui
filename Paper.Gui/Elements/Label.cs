using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Paper.Gui.Elements
{
    public class Label : Gui
    {
        private readonly SpriteFont _font;
        private readonly SpriteBatch _batch;

        public Color Color { get; set; }
        public float FontSize { get; set; }
        public string Text { get; set; }

        private readonly Vector2 TextAlign;

        public Label(SpriteBatch spriteBatch, SpriteFont font, string text, Vector2 Location, Vector2 textAlign, Color color, float fontSize = 1) : base(Location)
        {
            _font = font;
            _batch = spriteBatch;

            Text = text;
            Color = color;
            FontSize = fontSize;
            TextAlign = textAlign;
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 Size = _font.MeasureString(Text);
            _batch.DrawString(_font, Text, GlobalLocation, Color, 0, Size * TextAlign, FontSize, SpriteEffects.None, Depth);

            base.Draw(gameTime);
        }
    }
}
