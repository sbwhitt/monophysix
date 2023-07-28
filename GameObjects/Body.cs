using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGraphix;

public class Body {
    public Vector2 position { get; set; }
    public Vector2 velocity { get; set; }
    public float mass { get; set; } = 10;

    private Texture2D _texture { get; set; }
    private bool _fix;

    // ambient forces acting on blob (gravity, etc)
    private Vector2 _ambient { get; set; } = new Vector2(0, 0);

    // boundaries effect values
    private float _bounciness = -0.5f;
    private float _friction = 0;
    

    public Body(Vector2 startPos, float startMass = 10, bool fix = false) {
        position = startPos;
        velocity = new Vector2(0, 0);
        mass = startMass;
        _fix = fix;
    }

    public void Load(Texture2D texture) {
        _texture = texture;
    }

    public void Update(GameTime gameTime, int width, int height) {
        int limitX = width - _texture.Width;
        int limitY = height - _texture.Height;

        CheckCollision(limitX, limitY);

        velocity += _ambient;
        if (!_fix)
            position += velocity;

        ApplyFriction(limitX, limitY);
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

    public Vector2 GetCenter() {
        return new Vector2(position.X + _texture.Width / 2, position.Y + _texture.Height / 2);
    }

    // Helpers

    private void CheckCollision(int limitX, int limitY) {
        CheckCollisionX(limitX);
        CheckCollisionY(limitY);
    }

    private void CheckCollisionX(int limitX) {
        if (position.X + velocity.X < 0) {
            velocity = new Vector2(velocity.X * _bounciness, velocity.Y);
        }
        if (position.X + velocity.X > limitX) {
            velocity = new Vector2(velocity.X * _bounciness, velocity.Y);
        }
    }

    private void CheckCollisionY(int limitY) {
        if (position.Y + velocity.Y < 0) {
            velocity = new Vector2(velocity.X, velocity.Y * _bounciness);
        }
        if (position.Y + velocity.Y > limitY) {
            velocity = new Vector2(velocity.X, velocity.Y * _bounciness);
        }
    }

    private void ApplyFriction(int limitX, int limitY) {
        if (position.Y <= 0 || position.Y >= limitY)
            ApplyFrictionX();
        if (position.X <= 0 || position.X >= limitX)
            ApplyFrictionY();
    }

    private void ApplyFrictionX() {
        if (velocity.X > _friction) {
            velocity += new Vector2(-1 *  _friction, 0);
        }
        else if (velocity.X < -1*_friction) {
            velocity += new Vector2(_friction, 0);
        }
        else {
            velocity = new Vector2(0, velocity.Y);
        }
    }

    private void ApplyFrictionY() {
        if (velocity.Y > _friction) {
            velocity += new Vector2(0, -1 *  _friction);
        }
        else if (velocity.Y < -1*_friction) {
            velocity += new Vector2(0, _friction);
        }
        else {
            velocity = new Vector2(velocity.X, 0);
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
