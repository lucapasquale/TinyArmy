using UnityEngine;
using System.Collections;

[System.Flags]
public enum EquipSlots
{
    None = 0,
    Armor = 1 << 0,
    Rune = 1 << 1,
    Weapon = 1 << 2
}
