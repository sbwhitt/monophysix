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
    private Bodies _bodies = new Bodies();

    public Graphix()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        SetScreenDimensions(1000, 800);

        float orbitMass = 10;
        float fixMass = 5000;
        _bodies.AddBody(new Vector2(200, 100), orbitMass);
        _bodies.AddBody(new Vector2(_screenWidth - 10, _screenHeight - 10), orbitMass);
        _bodies.AddBody(new Vector2(_screenWidth - 10, 0), orbitMass);
        _bodies.AddBody(new Vector2(0, _screenHeight - 10), orbitMass);
        _bodies.AddBody(new Vector2(0, 0), orbitMass);
        _bodies.AddBody(new Vector2(_screenWidth - 100, _screenHeight - 100), orbitMass);
        _bodies.AddBody(new Vector2(_screenWidth - 100, 0), orbitMass);
        _bodies.AddBody(new Vector2(0, _screenHeight - 100), orbitMass);

        _bodies.AddBody(new Vector2(_screenWidth / 2, _screenHeight / 2), fixMass, true);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        _bodies.LoadBodies(Content.Load<Texture2D>("imgs/red_blob"));
    }

    protected override void Update(GameTime gameTime)
    {
        _screenWidth = _graphics.PreferredBackBufferWidth;
        _screenHeight = _graphics.PreferredBackBufferHeight;

        KeyboardState keyState = Keyboard.GetState();

        // IsActive block only accepts input if window is focused
        if (IsActive) {
            if (keyState.IsKeyDown(Keys.Q))
                Exit();

            float delta = 1f;
            bool once = false;
            Vector2 deltaVector = new Vector2(0, 0);
            if (_keyControls.Pressed(keyState, Keys.Left, once))
                deltaVector += new Vector2(-delta, 0);
            if (_keyControls.Pressed(keyState, Keys.Right, once))
                deltaVector += new Vector2(delta, 0);
            if (_keyControls.Pressed(keyState, Keys.Up, once))
                deltaVector += new Vector2(0, -delta);
            if (_keyControls.Pressed(keyState, Keys.Down, once))
                deltaVector += new Vector2(0, delta);
            
            _bodies.AddVelocity(deltaVector);
        }

        _bodies.Update(gameTime, _screenWidth, _screenHeight);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkGray);

        // TODO: Add your drawing code here

        _spriteBatch.Begin();

        _bodies.DrawBodies(_spriteBatch, false);
        
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
