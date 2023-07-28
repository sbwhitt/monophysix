using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace MonoGraphix;

public class KeyControls {
    private Dictionary<Keys, bool> _keysPressed = new Dictionary<Keys, bool>();

    public bool Pressed(KeyboardState state, Keys key) {
        if (!_keysPressed.ContainsKey(key))
            _keysPressed.Add(key, state.IsKeyDown(key));

        if (state.IsKeyDown(key)) {
            if (!_keysPressed[key]) {
                _keysPressed[key] = true;
                return true;
            }
            return false;
        }
        else {
            if (_keysPressed[key])
                _keysPressed[key] = false;
            return false;
        }
    }
}
