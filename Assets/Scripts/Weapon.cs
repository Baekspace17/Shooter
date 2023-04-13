using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None,
    Pistol,
    SMG,
    AR,
    SG,
    LMG,
    RPG
}

public class Weapon : MonoBehaviour
{

    public int bulletCount;
    public WeaponType wType;
        
    void Start()
    {
        SetAmmo(wType);
    }

    
    void Update()
    {

    }

    void SetAmmo(WeaponType type)
    {
        switch (type)
        {            
            case WeaponType.SMG:
                bulletCount = 25;
                break;
            case WeaponType.AR:
                bulletCount = 15;
                break;
            case WeaponType.SG:
                bulletCount = 5;
                break;
            case WeaponType.LMG:
                bulletCount = 60;
                break;
            case WeaponType.RPG:
                bulletCount = 1;
                break;
            default:
                break;
        }
    }
}