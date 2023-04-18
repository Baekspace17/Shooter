using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;
    public PlayerStat stat;

    void Start()
    {
        DamageSet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DamageSet()
    {
        switch(stat.currentWeaponType)
        {
            case WeaponType.Pistol:
            case WeaponType.SMG:
                Damage = 20;
                break;
            case WeaponType.SG:
                Damage = 15;
                break;
            case WeaponType.AR:
            case WeaponType.LMG:
                Damage = 30;
                break;
            case WeaponType.RPG:
                Damage = 100;
                break;
        }
    }
}
