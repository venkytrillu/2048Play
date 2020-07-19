using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public bool isMergedTile=false;
    private int number;
    private Text tileText;
    private Image tileImage;
    public int row;
    public int col;
    public int Number
    {
        set
        {
            number = value;
            if(number==0)
            {
                SetEmpty();
            }
            else
            {
                ApplyStyle(number);
                SetVisible(number);
            }
        }
        get
        {
            return number;
        }
    }
    private void Awake()
    {
        tileText = GetComponentInChildren<Text>();
        tileImage = transform.Find("TileCell").GetComponent<Image>();
    }


    void ApplyStyleFromHolder(int _index)
    {
        tileText.text = TileStyleHolder.instance.tileStyles[_index].Number.ToString();
        tileText.color = TileStyleHolder.instance.tileStyles[_index].TextColor;
        tileImage.color = TileStyleHolder.instance.tileStyles[_index].TileColor;
    }

    public GameObject TileChild()
    {
       return tileImage.gameObject;
    }

   public void ApplyStyle(int _num)
    {
        switch(_num)
        {
            case 2:
                ApplyStyleFromHolder(0);
                break;
            case 4:
                ApplyStyleFromHolder(1);
                break;
            case 8:
                ApplyStyleFromHolder(2);
                break;
            case 16:
                ApplyStyleFromHolder(3);
                break;
            case 32:
                ApplyStyleFromHolder(4);
                break;
            case 64:
                ApplyStyleFromHolder(5);
                break;
            case 128:
                ApplyStyleFromHolder(6);
                break;
            case 256:
                ApplyStyleFromHolder(7);
                break;
            case 512:
                ApplyStyleFromHolder(8);
                break;
            case 1024:
                ApplyStyleFromHolder(9);
                break;
            case 2048:
                ApplyStyleFromHolder(10);
                break;
            case 4096:
                ApplyStyleFromHolder(11);
                break;
            default:
                Debug.LogError("Given Number is not in the range of StyleHolder List");
                break;
        }
    }

    void SetEmpty()
    { 
        tileImage.enabled = false;
        tileText.enabled = false;
    }

    void SetVisible(int _num)
    {

        tileImage.enabled = true;
        tileText.enabled = true;
        tileText.text = _num.ToString();
    }


}//class
