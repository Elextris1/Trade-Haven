using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    public static UserInput input = new UserInput();

    public static Vector2 GetAxis2D()
    {
        Vector2 inputVector = input.Player.Move.ReadValue<Vector2>();
        Vector2 direction = new Vector2(inputVector.x, inputVector.y);
        return direction;
    }
}
