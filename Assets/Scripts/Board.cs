using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript.Steps;

public class Board : MonoBehaviour
{

    public static Board instance;
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private int row, col;
    [SerializeField][Range(0,1f)]
    private float delay;
    private bool isMovemade = false;
    private bool[] lineMoveCompleted = new bool[4] { true, true, true, true };
    private Tile[,] allTiles;
    public List<Tile> emptyTiles = new List<Tile>();
    private List<Tile[]> coloumsTiles = new List<Tile[]>();
    private List<Tile[]> rowsTiles = new List<Tile[]>();
    private void Awake()
    {
        if (instance == null)
            instance = this;
        allTiles = new Tile[row, col];
    }

    public void CreateTiles()
    {
        for (int x = 0; x < row; x++)
        {
           
            for (int y = 0; y < col; y++)
            {
                GameObject tileObject = Instantiate(tilePrefab);
                tileObject.transform.SetParent(transform.GetChild(0));
                tileObject.transform.localScale = Vector3.one;
                tileObject.name ="tile "+x +","+y;
                Tile tile = tileObject.GetComponent<Tile>();
                tile.row = x;
                tile.col = y;
                tile.Number = 0;
                allTiles[x, y] = tile;
                emptyTiles.Add(tile);
            }
           
        }

        coloumsTiles.Add(new Tile[] { allTiles[0, 0], allTiles[1, 0], allTiles[2, 0], allTiles[3, 0] });
        coloumsTiles.Add(new Tile[] { allTiles[0, 1], allTiles[1, 1], allTiles[2, 1], allTiles[3, 1] });
        coloumsTiles.Add(new Tile[] { allTiles[0, 2], allTiles[1, 2], allTiles[2, 2], allTiles[3, 2] });
        coloumsTiles.Add(new Tile[] { allTiles[0, 3], allTiles[1, 3], allTiles[2, 3], allTiles[3, 3] });

        rowsTiles.Add(new Tile[] { allTiles[0, 0], allTiles[0, 1], allTiles[0, 2], allTiles[0, 3] });
        rowsTiles.Add(new Tile[] { allTiles[1, 0], allTiles[1, 1], allTiles[1, 2], allTiles[1, 3] });
        rowsTiles.Add(new Tile[] { allTiles[2, 0], allTiles[2, 1], allTiles[2, 2], allTiles[2, 3] });
        rowsTiles.Add(new Tile[] { allTiles[3, 0], allTiles[3, 1], allTiles[3, 2], allTiles[3, 3] });
        InitialTiles();
    }

    void InitialTiles()
    {
        for(int i=0;i<2;i++)
            Generate();
    }


    private void Generate()
    {
        if (emptyTiles.Count > 0)
        {
            int indexOfRandomNumberFromTilesList = Random.Range(0, emptyTiles.Count);
            int randomNumber = Random.Range(0, 10);
            if (randomNumber <= 1)
            {
                emptyTiles[indexOfRandomNumberFromTilesList].Number = 4;
                StartCoroutine(TileAnimations.instance.AppearedTile(emptyTiles[indexOfRandomNumberFromTilesList].TileChild()));
            }
            else
            {
                emptyTiles[indexOfRandomNumberFromTilesList].Number = 2;
               StartCoroutine( TileAnimations.instance.AppearedTile(emptyTiles[indexOfRandomNumberFromTilesList].TileChild()));

            }
            emptyTiles.RemoveAt(indexOfRandomNumberFromTilesList);
        }
    }

    private bool MakeOneTileDownIndex(Tile[] _linesOfTiles)
    {
        for(int i=0;i<_linesOfTiles.Length-1;i++)
        {
            //Move Block
            if(_linesOfTiles[i].Number==0 && _linesOfTiles[i+1].Number!= 0)
            {
                _linesOfTiles[i].Number = _linesOfTiles[i+1].Number;
                _linesOfTiles[i + 1].Number = 0;
                return true;
            }

            //Merge Tiles
            if(_linesOfTiles[i].Number!=0 && _linesOfTiles[i].Number == _linesOfTiles[i+1].Number && 
               _linesOfTiles[i].isMergedTile == false &&  _linesOfTiles[i + 1].isMergedTile==false)
            {
                _linesOfTiles[i].Number *= 2;
                _linesOfTiles[i + 1].Number = 0;
                _linesOfTiles[i].isMergedTile = true;
                StartCoroutine(TileAnimations.instance.MergedTile(_linesOfTiles[i].TileChild()));
                UIManager.instance.CurrentScore += _linesOfTiles[i].Number;
                if (_linesOfTiles[i].Number == 2048)
                {
                    UIManager.instance.YouWin();
                }
                return true;
            }
        }
        return false;
    }

    private bool MakeOneTileUpIndex(Tile[] _linesOfTiles)
    {
        for (int i = _linesOfTiles.Length - 1; i >0; i--)
        {
            //Move Block
            if (_linesOfTiles[i].Number == 0 && _linesOfTiles[i - 1].Number != 0)
            {
                _linesOfTiles[i].Number = _linesOfTiles[i - 1].Number;
                _linesOfTiles[i - 1].Number = 0;
                return true;
            }

            //Merge Tiles
            if (_linesOfTiles[i].Number != 0 && _linesOfTiles[i].Number == _linesOfTiles[i - 1].Number &&
               _linesOfTiles[i].isMergedTile == false && _linesOfTiles[i - 1].isMergedTile == false)
            {
                _linesOfTiles[i].Number *= 2;
                _linesOfTiles[i - 1].Number = 0;
                _linesOfTiles[i].isMergedTile = true;
                StartCoroutine(TileAnimations.instance.MergedTile(_linesOfTiles[i].TileChild()));
                UIManager.instance.CurrentScore += _linesOfTiles[i].Number;
                if(_linesOfTiles[i].Number==2048)
                {
                    UIManager.instance.YouWin();
                }
                return true;
            }
        }
        return false;
    }

    private void ResetMergedTiles()
    {
        foreach(Tile t in allTiles)
        {
            t.isMergedTile = false;
        }
    }

    private void UpdateEmptyTiles()
    {
        emptyTiles.Clear();
        foreach (Tile t in allTiles)
        {
            if (t.Number == 0)
                emptyTiles.Add(t);
        }
    }

    bool CanMove()
    {
        if (emptyTiles.Count > 0)
        {
            return true;
        }
        else
        {
            // checking possible merges in coloumns
            for(int i=0;i< coloumsTiles.Count; i++)
            {
                for (int j = 0; j < rowsTiles.Count-1; j++)
                {
                    if(allTiles[j, i].Number== allTiles[j+1, i].Number)
                    {
                        return true;
                    }
                }
            }

            // checking possible merges in rows
            for (int i = 0; i < rowsTiles.Count; i++)
            {
                for (int j = 0; j < coloumsTiles.Count - 1; j++)
                {
                    if (allTiles[i, j].Number == allTiles[i, j+1].Number)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
  

    public void Move(MoveDirection _move)
    {
        Debug.Log(_move.ToString() + " move");
        isMovemade = false;
        ResetMergedTiles();
        if(delay>0.000f)
        {
            StartCoroutine(MoveCoroutine(_move));
        }
        else
        {
            for (int i = 0; i < row; i++)
            {
                switch (_move)
                {
                    case MoveDirection.Down:
                        while (MakeOneTileUpIndex(coloumsTiles[i]))
                        {
                            isMovemade = true;
                        }
                        break;
                    case MoveDirection.Left:
                        while (MakeOneTileDownIndex(rowsTiles[i]))
                        {
                            isMovemade = true;
                        }
                        break;
                    case MoveDirection.Right:
                        while (MakeOneTileUpIndex(rowsTiles[i]))
                        {
                            isMovemade = true;
                        }
                        break;
                    case MoveDirection.Up:
                        while (MakeOneTileDownIndex(coloumsTiles[i]))
                        {
                            isMovemade = true;
                        }
                        break;
                }
            }
        }


        MadeMove();
    }

    void MadeMove()
    {
        if (isMovemade)
        {
            UpdateEmptyTiles();
            Generate();
            if (CanMove() == false)
            {
                UIManager.instance.GameOver();
            }
            //else
            //{
            //    GameManager.instance.gameState = GameState.Playing;
            //}
        }
    }

    IEnumerator MoveCoroutine(MoveDirection _move)
    {
      
        switch (_move)
        {
            case MoveDirection.Down:
                for(int i = 0; i < coloumsTiles.Count; i++)
                {
                    StartCoroutine(MoveOneLineUpIndexCoroutine(coloumsTiles[i], i));
                }
                break;
            case MoveDirection.Left:
                for (int i = 0; i < rowsTiles.Count; i++)
                {
                    StartCoroutine(MoveOneLineDownIndexCoroutine(rowsTiles[i], i));
                }
                break;
            case MoveDirection.Right:
                for (int i = 0; i < rowsTiles.Count; i++)
                {
                    StartCoroutine(MoveOneLineUpIndexCoroutine(rowsTiles[i], i));
                }
                break;
            case MoveDirection.Up:
                for (int i = 0; i < coloumsTiles.Count; i++)
                {
                    StartCoroutine(MoveOneLineDownIndexCoroutine(coloumsTiles[i], i));
                }
                break;
        }

        //wait untill the move is over for all lines
        while(!(lineMoveCompleted[0] && lineMoveCompleted[1] && lineMoveCompleted[2] && lineMoveCompleted[3]))
        {
            yield return null;
        }

      //  MadeMove();

    }

    IEnumerator MoveOneLineUpIndexCoroutine(Tile[] _line,int index)
    {
        lineMoveCompleted[index] = false;
        while(MakeOneTileUpIndex(_line))
        {
            GameManager.instance.gameState = GameState.WaitForMoveToEnd;
            isMovemade = true;
            yield return new WaitForSeconds(delay);
        }
        lineMoveCompleted[index] = true;
        GameManager.instance.gameState = GameState.Playing;
    }

    IEnumerator MoveOneLineDownIndexCoroutine(Tile[] _line, int index)
    {
        lineMoveCompleted[index] = false;
        while (MakeOneTileDownIndex(_line))
        {
            GameManager.instance.gameState = GameState.WaitForMoveToEnd;
            isMovemade = true;
            yield return new WaitForSeconds(delay);
        }
        lineMoveCompleted[index] = true;
        GameManager.instance.gameState = GameState.Playing;
    }



}//class
