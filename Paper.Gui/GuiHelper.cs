using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Paper.Gui
{
    public static class GuiHelper
    {
        public static float ScaleTextToBounds(SpriteFont font, string text, Point size)
        {
            Vector2 textSize = font.MeasureString(text);

            float scaleX = size.X / textSize.X;
            float scaleY = size.Y / textSize.Y;

            return Math.Min(scaleX, scaleY);
        }
    }
}
