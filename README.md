# Paper.Gui 
![image](https://raw.githubusercontent.com/itsBuggingMe/Paper.Gui/master/icon.png)    
Paper.Gui is a simple Gui Library -Monogame style. 

The philosophy is to allow you to easily make Guis, while location/scaling is handled by the library
	
The library first assumes the user is using a certain resolution, and provides information about location and scaling based on the actual location.
## Usage  

### Initalization
The library must first be initalized.
```csharp
protected override void Initialize()
{
    //The point passed into represents assumed size of the screen
    //I reccomend that it is the resolution of your monitor
    //However, it does not have to be.
    //The rest of this document will assume that you have the default size of 1920, 1080
    Point useSize = new Point(1920, 1080);
    Gui.Initalize(this, useSize);
    
    base.Initialize();
}
```

Gui  "Elements" work as a Parent - Child relationship  
Every gui element has a parent gui element. A child gui's location is relative to it's parent.
The only gui element that does not have a parent is from the `BaseGui` class
```csharp
GuiBase BaseGui = new GuiBase();

SpriteFont font = Content.Load<SpriteFont>("font");
Texture2D texture = Content.Load<Texture2D>("button");
Vector2 buttonLocation = new Vector2(960,540);//this is the center of 1920,1080
Color textColor = Color.Black;

//this button will always render at the center of the screen,
//even if the resolution is not 1920,1080
var button = new Button(
	_spriteBatch, 
	font, 
	texture, 
	textColor, 
	"Exit Game", 
	ButtonClick);
	
BaseGui.AddElement(button);

void ButtonClick()
{
    Console.WriteLine("This Button is Pressed!");
    Exit();
}
```

### Updating  
The BaseGui object has to have `Tick(GameTime gameTime)` and `Draw(GameTime gameTime)` manually called.  

```csharp
//your Game1 class
protected override void Update(GameTime gameTime)
{
    BaseGui.Tick(gameTime);
    base.Update(gameTime);
}

protected override void Draw(GameTime gameTime)
{
    GraphicsDevice.Clear(Color.CornflowerBlue);
    
    _spriteBatch.Begin(SpriteSortMode.FrontToBack);
    BaseGui.Draw(gameTime);
    _spriteBatch.End();

    base.Draw(gameTime);
}
```

### Making own elements  
Say you have a settings menu with many gui elements.  
```csharp
BaseGui GuiBase = new BaseGui();

var someButton = new Button(
	_spriteBatch, 
	font, 
	someTexture, 
	Color.White, 
	"SomeText", 
	ButtonClick);
	
GuiBase.AddElement(someButton);
//...other elements

```
To reuse the settings menu in multiple locations, you could refactor into a class the extends Gui
```csharp
internal class SettingsMenu : Gui
{
    public SettingsMenu(Vector2 Location) : base(Location)
    {
        var someButton = new Button(
			_spriteBatch, 
			font, 
			someTexture, 
			Color.White, 
			"SomeText", 
			ButtonClick);
			
		GuiBase.AddElement(someButton);
        //...other elements

    }
}
```

Now to reuse Settings Menu it is a manner of calling
```csharp
GuiBase.AddElement(new SettingsMenu(Vector2.Zero));
```

Making your own elements is similar

```csharp
internal class RainbowBox : Gui
{
    private Color currentColor;
    private SpriteBatch batch;
    private Texture2D texture;
    public RainbowBox(Vector2 Location, SpriteBatch batch, Texture2D texture) : base(Location)
    {
        this.font = font;
        this.batch = batch;
        this.texture = texture;
    }

	public override void Tick(GameTime gameTime)
	{
	    //make this a random color every tick
	    currentColor = new Color((uint)Random.Shared.NextInt64());
	    currentColor *= 8;

		base.Tick(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        //Use GlobalLocation for drawing
        //GlobalLocation has been scaled and transformed based on parent positions
        batch.Draw(texture, GlobalLocation, Color.White);
        base.Draw(gameTime);
    }
}
```
