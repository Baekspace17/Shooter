using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;
    public PlayerStat stat;
    public float bulletSpeed;
    public float destroyTime = 0.5f;

    void Start()
    {
        stat = GameManager._Instance._Pstat;
        BulletSet();
        RecoilSet();
        BulletMoveSet();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        destroyTime -= Time.deltaTime;
        if (destroyTime < 0) Destroy(this.gameObject);
    }    

    void BulletMoveSet()
    {
        if (stat.currentWeaponType == WeaponType.RPG)
            bulletSpeed *= -1f;                   
    }

    void RecoilSet()
    {
        if (stat.currentWeaponType == WeaponType.RPG) return;
        
        else if (stat.currentWeaponType == WeaponType.SG)
        {
            float random = Random.Range(-20f, 20f);
            transform.Rotate(0f, random, 0f);
        }        
        else
        {
            float random = Random.Range(-3f, 3f);
            transform.Rotate(0f, random, 0f);
        }
    }

    void BulletSet()
    {
        switch(stat.currentWeaponType)
        {
            case WeaponType.Pistol:
            case WeaponType.SMG:
                bulletSpeed = 50f;
                Damage = 20;
                break;
            case WeaponType.SG:
                bulletSpeed = 60f;
                destroyTime = 0.1f;
                Damage = 15;
                break;
            case WeaponType.AR:
            case WeaponType.LMG:
                bulletSpeed = 50f;
                Damage = 30;
                break;
            case WeaponType.RPG:
                
                bulletSpeed = 25f;
                destroyTime = 1f;
                Damage = 100;
                break;
        }
    }
}
