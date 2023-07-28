using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGraphix;

public class Blob {
    public Vector2 position { get; set; }
    public Vector2 velocity { get; set; }

    private Texture2D _texture { get; set; }
    private Vector2 _ambient { get; set; } = new Vector2(0, 0.2f);

    private float _deltaX = -0.75f;
    private float _deltaY =-0.75f;
    

    public Blob(Vector2 startPos) {
        position = startPos;
        velocity = new Vector2(0, 0);
    }

    public void Load(Texture2D texture) {
        _texture = texture;
    }

    public void Update(GameTime gameTime, int width, int height) {
        int limitX = width - _texture.Width;
        int limitY = height - _texture.Height;

        CheckVelocity(limitX, limitY);

        velocity += _ambient;
        position += velocity;

        CheckBoundaries(limitX, limitY);
    }
    
    public void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(_texture, position, Color.White);
    }

    public void AddVelocity(Vector2 change) {
        velocity += change;
    }

    public void DrawVelocityVector(SpriteBatch spriteBatch) {
        Primitives2D.DrawLine(
            spriteBatch,
            GetCenter(),
            GetCenter() + (velocity * new Vector2(10, 10)),
            Color.Black,
            1
        );
    }

    // Helpers

    private Vector2 GetCenter() {
        return new Vector2(position.X + _texture.Width / 2, position.Y + _texture.Height / 2);
    }

    private void CheckVelocity(int limitX, int limitY) {
        CheckVelocityX(limitX);
        CheckVelocityY(limitY);
    }

    private void CheckVelocityX(int limitX) {
        if (position.X + velocity.X < 0) {
            velocity = new Vector2(velocity.X * _deltaX, velocity.Y);
        }
        if (position.X + velocity.X > limitX) {
            velocity = new Vector2(velocity.X * _deltaX, velocity.Y);
        }
    }

    private void CheckVelocityY(int limitY) {
        if (position.Y + velocity.Y < 0) {
            velocity = new Vector2(velocity.X, velocity.Y * _deltaY);
        }
        if (position.Y + velocity.Y > limitY) {
            velocity = new Vector2(velocity.X, velocity.Y * _deltaY);
        }
    }

    private void CheckBoundaries(int limitX, int limitY) {
        CheckBoundaryX(limitX);
        CheckBoundaryY(limitY);
    }

    private void CheckBoundaryX(int limitX) {
        if (position.X < 0) {
            position = new Vector2(0, position.Y);
        }
        if (position.X >= limitX) {
            position = new Vector2(limitX, position.Y);
        }
    }

    private void CheckBoundaryY(int limitY) {
        if (position.Y < 0) {
            position = new Vector2(position.X, 0);
        }
        if (position.Y >= limitY) {
            position = new Vector2(position.X, limitY);
        }
    }

}
