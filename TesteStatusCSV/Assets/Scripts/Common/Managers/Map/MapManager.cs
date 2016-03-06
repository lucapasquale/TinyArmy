using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum MapType { WorldMap, PlayableMap}

public class MapManager : MonoBehaviour
{
    #region Singleton
    public static MapManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Fields
    public MapLevelScript currentMap;
    public int subLevelNumber;

    MapLevelScript mapLevel;
    #endregion

    #region MonoBeahaviour
    void OnEnable()
    {
        this.AddObserver(OnEnemyDied, EnemyHealth.EnemyDiedNotification);
        subLevelNumber = 1;
    }

    void OnDisable()
    {
        this.RemoveObserver(OnEnemyDied, EnemyHealth.EnemyDiedNotification);
    }
    #endregion

    void OnEnemyDied(object sender, object args)
    {
        currentMap.currentSubLevel.enemiesAliveCount -= 1;
        if (currentMap.currentSubLevel.enemiesAliveCount == 0)  //All enemies are dead
        {
            Debug.Log(string.Format("Level {0} {1} Ended!", currentMap.mapLevel, currentMap.currentSubLevel.ToString()));

            if (subLevelNumber != currentMap.mapLastSublevel)   //If it's NOT the last subLevel, load the next
                StartCoroutine(DelayRespawn());
            else
                Debug.LogError("Map Finished!");
        }
        
    }

    #region Private
    IEnumerator DelayRespawn()
    {
        yield return new WaitForSeconds(2);
        RespawnPlayers();
        subLevelNumber += 1;
        currentMap.currentSubLevel = currentMap.subLevelList[subLevelNumber - 1];
        SpawnEnemies(currentMap.currentSubLevel);
       
    }

    void RespawnPlayers()
    {
        for (int i = 0; i < GameManager.Instance.party.Count; i++)
        {
            GameObject physics = GameManager.Instance.party[i].GetComponentInChildren<PlayerMovement>().gameObject;
            physics.transform.position = GameObject.Find("Spawn " + i).transform.position;
        }
    }

    void SpawnEnemies(SubLevelScript subLevel)
    {
        int i = 0;
        subLevel.enemiesAliveCount = 0;
        foreach (GameObject enemy in subLevel.enemiesSublevel)
        {
            Instantiate(enemy, GameObject.Find("EnemySpawn " + i).transform.position, Quaternion.identity);
            i++;
            subLevel.enemiesAliveCount++;
        }
    }
    #endregion
}
