using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGraphix;

public class Graphix : Game
{
    private int _screenWidth;
    private int _screenHeight;
    private KeyControls _keyControls = new KeyControls();
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Blob _blob;

    public Graphix()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        SetScreenDimensions(1200, 800);

        _blob = new Blob(new Vector2(_screenWidth / 2, _screenHeight / 2));

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        _blob.Load(Content.Load<Texture2D>("imgs/red_blob"));
    }

    protected override void Update(GameTime gameTime)
    {
        _screenWidth = _graphics.PreferredBackBufferWidth;
        _screenHeight = _graphics.PreferredBackBufferHeight;

        KeyboardState keyState = Keyboard.GetState();
        if (keyState.IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        if (_keyControls.Pressed(keyState, Keys.Left))
            _blob.AddVelocity(new Vector2(-5f, 0));
        if (_keyControls.Pressed(keyState, Keys.Right))
            _blob.AddVelocity(new Vector2(5f, 0));
        if (_keyControls.Pressed(keyState, Keys.Up))
            _blob.AddVelocity(new Vector2(0, -5f));
        if (_keyControls.Pressed(keyState, Keys.Down))
            _blob.AddVelocity(new Vector2(0, 5f));

        _blob.Update(gameTime, _screenWidth, _screenHeight);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.LightGray);

        // TODO: Add your drawing code here

        _spriteBatch.Begin();

        _blob.DrawVelocityVector(_spriteBatch);
        _blob.Draw(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void SetScreenDimensions(int width, int height)
    {
        _graphics.PreferredBackBufferWidth = width;
        _graphics.PreferredBackBufferHeight = height;
        _graphics.ApplyChanges();

        _screenWidth = width;
        _screenHeight = height;
    }
}
