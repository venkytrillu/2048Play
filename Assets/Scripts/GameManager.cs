using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing,GameOver, WaitForMoveToEnd
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        BoardSetUp();
    }

    void BoardSetUp()
    {
        Board.instance.CreateTiles();
    }



}//class
