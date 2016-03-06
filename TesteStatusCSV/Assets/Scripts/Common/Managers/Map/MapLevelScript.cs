using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MapLevelScript : MonoBehaviour
{
    #region Fields
    public int mapLevel;
    public int mapLastSublevel;

    public SubLevelScript currentSubLevel;
    public SubLevelScript[] subLevelList;
    #endregion

    #region MonoBeahaviour
    void Start()
    {
        mapLastSublevel = subLevelList.Length;
    }
    #endregion
}

