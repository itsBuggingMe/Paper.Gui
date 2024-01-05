using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paper.Gui;
using Paper.Gui.Elements;
using System.Diagnostics;

namespace GuiTest
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GuiBase Base = new();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Gui.Initalize(this, new Point(800, 400));

            base.Initialize();

            Base = new GuiBase();
            Base.AddElement(new Button(_spriteBatch, Content.Load<SpriteFont>("font"), Content.Load<Texture2D>("button"), new Vector2(100), Color.Black, "Exit", Exit));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            Base.Tick(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack);
            Base.Draw(gameTime);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
