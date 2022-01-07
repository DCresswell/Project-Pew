using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
[SerializeField] int damage = 10;
public int getWeaponDamage()
{
    return damage;
}

}
