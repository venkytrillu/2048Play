using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
    Left,Right,Up,Down
}


public class InputManager : MonoBehaviour
{

    void Update()
    {
        if(GameManager.instance.gameState==GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //Up move
                Board.instance.Move(MoveDirection.Up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //Down move
                Board.instance.Move(MoveDirection.Down);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //Left move
                Board.instance.Move(MoveDirection.Left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //Right move
                Board.instance.Move(MoveDirection.Right);
            }
        }
      
    }
}// class
