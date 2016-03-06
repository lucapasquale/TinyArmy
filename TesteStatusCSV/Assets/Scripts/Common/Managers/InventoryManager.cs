using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> Inventory = new List<GameObject>();

    public GameObject baseSword;

    #region MonoBehaviour
    void OnEnable()
    {
        this.AddObserver(OnEquippedItem, Equipment.EquippedNotification);
        this.AddObserver(OnUnEquippedItem, Equipment.UnEquippedNotification);
    }

    void OnDisable()
    {
        this.RemoveObserver(OnEquippedItem, Equipment.EquippedNotification);
        this.RemoveObserver(OnUnEquippedItem, Equipment.UnEquippedNotification);
    }
    #endregion

    #region Event Handlers
    void OnEquippedItem(object sender, object args)
    {
        Equipment eq = sender as Equipment;
        Equippable item = args as Equippable;
        ClassTypes player = eq.GetComponentInParent<Stats>().classType;
        Inventory.Remove(item.gameObject);
        string message = string.Format("{0} equipped {1}", player, item.name);
        Debug.Log(message);
    }

    void OnUnEquippedItem(object sender, object args)
    {
        Equipment eq = sender as Equipment;
        Equippable item = args as Equippable;
        ClassTypes player = eq.GetComponentInParent<Stats>().classType;
        Inventory.Add(item.gameObject);
        string message = string.Format("{0} un-equipped {1}", player, item.name);
        Debug.Log(message);
    }
    #endregion

    #region Public
    public void CreateWeapon(string name, WeaponSlot wSlot, int damage, MagicType mType, float mPower, float mDuration)
    {
        GameObject _item = new GameObject(name);
        _item.tag = "Item";
        StatModifierFeature smf = _item.AddComponent<StatModifierFeature>();
        smf.type = StatTypes.weaponDMG;
        smf.amount = damage;
        Equippable equip = _item.AddComponent<Equippable>();
        equip.defaultSlot = EquipSlots.Weapon;
        equip.weaponSlot = wSlot;
        MagicFeature mFeature = _item.AddComponent<MagicFeature>();
        mFeature.magicType = mType;
        mFeature.magicPower = mPower;
        mFeature.duration = mDuration;

        Inventory.Add(_item);
        _item.transform.SetParent(GameObject.Find("Inventory").transform);
    }


    public void AddModifierFeature(GameObject item, StatTypes sType, int value)
    {
        StatModifierFeature smf = item.AddComponent<StatModifierFeature>();
        smf.type = sType;
        smf.amount = value;
    }

    public void EquipItem(GameObject item, GameObject player)
    {
        EquipSlots slot = item.GetComponent<Equippable>().defaultSlot;
        if (slot == EquipSlots.Weapon)
        {
            WeaponSlot wSlot = item.GetComponent<Equippable>().weaponSlot;
            if (!CanEquipWeaponType(player.GetComponent<Stats>().classType, wSlot))
            {
                Debug.Log(player.GetComponent<Stats>().classType + " não pode equipar " + item.name);
                return;
            }
        }
        if (ItemInSlot(player, slot) != null)
            UnequipItem(player, slot);

        player.GetComponentInChildren<Equipment>().Equip(item.GetComponent<Equippable>(), item.GetComponent<Equippable>().defaultSlot);
        item.transform.SetParent(player.GetComponentInChildren<Equipment>().transform);
    }


    public void UnequipItem(GameObject player, EquipSlots slot)
    {
        GameObject item = ItemInSlot(player, slot);
        player.GetComponentInChildren<Equipment>().UnEquip(slot);
        item.transform.SetParent(GameObject.Find("Inventory").transform);
    }

    public GameObject ItemInSlot(GameObject player, EquipSlots slot)
    {
        Equipment equip = player.GetComponentInChildren<Equipment>();
        for (int i = 0; i < equip.items.Count; i++)
        {
            if (equip.items[i].defaultSlot == slot)
                return equip.items[i].GetComponentInParent<Transform>().gameObject;
        }
        return null;
    }
    #endregion

    #region Private
    bool CanEquipWeaponType(ClassTypes classType, WeaponSlot slot)
    {
        switch (classType)
        {
            case ClassTypes.Warrior:
                if ((slot & WeaponSlot.Sword) == WeaponSlot.Sword)
                    return true;
                break;
            case ClassTypes.Archer:
                if ((slot & WeaponSlot.Bow) == WeaponSlot.Bow)
                    return true;
                break;
            case ClassTypes.Wizard:
                if ((slot & WeaponSlot.Staff) == WeaponSlot.Staff)
                    return true;
                break;
        }

        return false;
    }

    #endregion

}