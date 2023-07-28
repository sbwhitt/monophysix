using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGraphix;

public class Bodies {
    private List<Body> _bodies = new List<Body>();

    public Bodies() {}

    public void AddBody(Vector2 position) {
        _bodies.Add(new Body(position));
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
        foreach (Body b in _bodies)
            b.Update(gameTime, width, height);
    }

    public void DrawBodies(SpriteBatch spriteBatch) {
        foreach (Body b in _bodies) {
            b.DrawVelocityVector(spriteBatch);
            b.Draw(spriteBatch);
        }
    }
}
