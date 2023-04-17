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
    public WeaponType wType;

    public float fireRate; // 연사속도
    public float range; // 사정거리
    public int bulletCount; // 무기별 총알갯수
    
        
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
            case WeaponType.Pistol:
                fireRate = 0.4f;
                bulletCount = 10000;
                break;
            case WeaponType.SMG:
                fireRate = 0.1f;
                bulletCount = 50;
                break;
            case WeaponType.AR:
                fireRate = 0.2f;
                bulletCount = 100;
                break;
            case WeaponType.SG:
                fireRate = 0.7f;
                bulletCount = 10;
                break;
            case WeaponType.LMG:
                fireRate = 0.15f;
                bulletCount = 200;
                break;
            case WeaponType.RPG:
                fireRate = 1f;
                bulletCount = 3;
                break;
            default:
                break;
        }
    }
}