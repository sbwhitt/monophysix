using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGraphix;

public class Blob {
    private Texture2D _texture { get; set; }
    private Vector2 _position { get; set; }
    private Vector2 _ambient { get; set; } = new Vector2(0, 0.1f);
    private Vector2 _acceleration { get; set; }
    

    public Blob() {
        _acceleration = new Vector2(0, 0);
    }

    public void Load(Texture2D texture, Vector2 position) {
        _texture = texture;
        _position = position;
    }

    public void Update(GameTime gameTime, int width, int height) {
        int limitY = height - _texture.Height;
        if (_position.Y + _acceleration.Y > limitY) {
            _acceleration = new Vector2(_acceleration.X, _acceleration.Y * -0.5f);
        }
        _acceleration += _ambient;
        _position += _acceleration;
        if (_position.Y > limitY) {
            _position = new Vector2(_position.X, limitY);
        }
    }
    
    public void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(_texture, _position, Color.White);
    }

    public void AddAcceleration(Vector2 acceleration) {
        _acceleration += acceleration;
    }

}
