using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SubLevelScript : MonoBehaviour
{
    #region Fields
    public int mapSublevel;

    public int enemiesAliveCount;
    public GameObject[] enemiesSublevel;   
    #endregion

    #region MonoBeahaviour
    void Start()
    {
        enemiesAliveCount = GameObject.FindObjectsOfType<EnemyHealth>().Length;
    }
    #endregion
}
