using UnityEngine;
using System.Collections;

[System.Flags]
public enum WeaponSlot
{
    None = 0,
    Sword = 1 << 0,
    Bow = 1 << 1,
    Staff = 1 << 2
}
