using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAnimations : MonoBehaviour
{
    public static TileAnimations instance;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }

    public IEnumerator AppearedTile(GameObject _object)
    {
        if(_object.transform.localScale==Vector3.one)
         iTween.ScaleFrom(_object, iTween.Hash("scale",new Vector3(0.5f,0.5f,0.5f),"islocal",true,"time",0.35f,"easetype",iTween.EaseType.linear));
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator MergedTile(GameObject _object)
    {
        if (_object.transform.localScale == Vector3.one)
            iTween.ScaleFrom(_object, iTween.Hash("scale", new Vector3(1.3f, 1.3f, 0.5f), "islocal", true, "time", 0.35f, "easetype", iTween.EaseType.linear));
        yield return new WaitForEndOfFrame();
    }

}//class
