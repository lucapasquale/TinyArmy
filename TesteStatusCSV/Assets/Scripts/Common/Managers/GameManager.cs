using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class GameManager : MonoBehaviour {

    #region Singleton
    public static GameManager Instance { get; private set; }

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

    public List<GameObject> party;
    InventoryManager invManager;
    public StatPanelController statPanelController;

    public const string ChangeMapNotification = "GameManager.ChangeMapNotification";

    #region MonoBehaviour
    void OnEnable()
    {
        MapLevelScript curMap = GameObject.FindObjectOfType<MapLevelScript>();
        MapManager.Instance.currentMap = curMap;
        MapManager.Instance.currentMap.currentSubLevel = GameObject.Find("SubLevel 1").GetComponent<SubLevelScript>();
    }

    void Start() {
        party = GetParty();
        invManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
        for (int i = 0; i < party.Count; i++)
            InitPlayer(party[i]);
        CreateWeapons();
        statPanelController.ShowPanels(party);
    }
    #endregion

    public void ButtonOneClick()
    {
        party[0].GetComponent<Stats>()[StatTypes.EXP] += 50;
    }

    public void ButtonTwoClick()
    {
        invManager.EquipItem(invManager.Inventory[0], party[0]);
    }

    #region Private
    

    List<GameObject> GetParty()
    {
        party = new List<GameObject>();
        Rank[] playerCount = GameObject.FindObjectsOfType<Rank>();
        for (int i = 0; i < playerCount.Length; i++) {
            {
                party.Add(GameObject.Find(string.Format("Slot {0}", i)).transform.GetChild(0).gameObject);

            }
        }
        return party;
    }

    void InitPlayer (GameObject player)
    {
        Rank rank = player.GetComponent<Rank>();
        rank.Init(1);
        Stats stat = player.GetComponent<Stats>();
        stat[StatTypes.Alive] = 1;
        switch (stat.classType)     //Create Initial Weapons and equip them
        {
            case ClassTypes.Warrior:
                invManager.CreateWeapon("Base Sword", WeaponSlot.Sword, 5, MagicType.None, 0f, 0f);
                invManager.EquipItem(invManager.Inventory[0], player);
                break;
            case ClassTypes.Archer:
                invManager.CreateWeapon("Base Bow", WeaponSlot.Bow, 5, MagicType.None, 0f, 0f);
                invManager.EquipItem(invManager.Inventory[0], player);
                break;
            case ClassTypes.Wizard:
                invManager.CreateWeapon("Base Staff", WeaponSlot.Staff, 5, MagicType.None, 0f, 0f);
                invManager.EquipItem(invManager.Inventory[0], player);
                break;
        }
    }

    void CreateWeapons()
    {
        //invManager.CreateWeapon("Bow Poison", WeaponSlot.Bow, 10, MagicType.Poison, 0.5f, 10f);
        invManager.CreateWeapon("Sword Poison", WeaponSlot.Sword, 10, MagicType.Poison, 1.5f, 80f);
        invManager.CreateWeapon("Sword Fire", WeaponSlot.Sword, 10, MagicType.Fire, 0.5f, 20f);
        invManager.CreateWeapon("Sword Fire 2", WeaponSlot.Sword, 20, MagicType.Ice, 0.5f, 20f);
    }
    #endregion

}
