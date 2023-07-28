using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGraphix;

public class Bodies {
    private List<Body> _bodies = new List<Body>();
    private float _g = 0.1f;

    public Bodies() {}

    public void AddBody(Vector2 position, float mass = 10, bool fix = false) {
        _bodies.Add(new Body(position, mass, fix));
    }

    public void LoadBodies(Texture2D texture) {
        foreach (Body b in _bodies)
            b.Load(texture);
    }

    public void AddVelocity(Vector2 change) {
        foreach (Body b in _bodies)
            b.AddVelocity(change);
    }

    public void Update(GameTime gameTime, int width, int height) {
        // handle gravitational forces on each body
        foreach (Body b in _bodies) {
            foreach (Body other in _bodies) {
                if (b != other) {
                    HandleAttraction(b, other);
                }
            }
        }

        foreach (Body b in _bodies)
            b.Update(gameTime, width, height);
    }

    public void DrawBodies(SpriteBatch spriteBatch, bool drawVelocityVector) {
        foreach (Body b in _bodies) {
            if (drawVelocityVector)
                b.DrawVelocityVector(spriteBatch);
            b.Draw(spriteBatch);
        }
    }

    // Helpers

    private void HandleAttraction(Body b, Body other) {
        Vector2 orientation = new Vector2(1, 1);
        if (b.GetCenter().X > other.GetCenter().X)
            orientation.X = -1;
        if (b.GetCenter().Y > other.GetCenter().Y)
            orientation.Y = -1;
        float distance = Vector2.Distance(b.GetCenter(), other.GetCenter());
        float attraction = _g * ((b.mass * other.mass) / (distance * distance));
        b.AddVelocity((attraction * orientation) / b.mass);
    }
}
