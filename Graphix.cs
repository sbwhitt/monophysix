using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGraphix;

public class Graphix : Game {
    private int _screenWidth;
    private int _screenHeight;
    private KeyControls _keyControls = new KeyControls();
    private Random _random = new Random();
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Bodies _bodies = new Bodies();
    private bool _velocityTrail = false;

    public Graphix() {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize() {
        // TODO: Add your initialization logic here

        SetScreenDimensions(1000, 800);

        float fixMass = 5000;
        _bodies.AddBody(new Vector2(_screenWidth / 2, _screenHeight / 2), fixMass, true);
        float orbitMass = 100;
        for (int i = 0; i < 20; i++)
            _bodies.AddBody(new Vector2(GetRandomX(), GetRandomY()), orbitMass);        

        base.Initialize();
    }

    protected override void LoadContent() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        _bodies.LoadBodies(Content.Load<Texture2D>("imgs/red_blob"));
    }

    protected override void Update(GameTime gameTime) {
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

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.DarkGray);

        // TODO: Add your drawing code here

        _spriteBatch.Begin();

        _bodies.DrawBodies(_spriteBatch, _velocityTrail);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void SetScreenDimensions(int width, int height) {
        _graphics.PreferredBackBufferWidth = width;
        _graphics.PreferredBackBufferHeight = height;
        _graphics.ApplyChanges();

        _screenWidth = width;
        _screenHeight = height;
    }

    private int GetRandomX() {
        return _random.Next(0, _screenWidth - 10);
    }

    private int GetRandomY() {
        return _random.Next(0, _screenHeight - 10);
    }
}
