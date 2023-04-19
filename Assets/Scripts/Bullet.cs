using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public WeaponType currentWeaponType;
    public float bulletSpeed;
    public float destroyTime = 0.5f;
    public GameObject explosion;

    void Start()
    {
        currentWeaponType = GameManager._Instance._Pstat.currentWeaponType;
        Resources.Load<GameObject>("Temp/Explosion");
        BulletSet();
        RecoilSet();
        BulletMoveSet();
    }

    void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        destroyTime -= Time.deltaTime;
        if (destroyTime < 0)
        {
            if (currentWeaponType == WeaponType.RPG)
            {
                GameObject obj = Instantiate(explosion, this.transform.position, Quaternion.identity);
                obj.GetComponent<Explosion>().damage = this.damage;
            }
            Destroy(this.gameObject);
        }

    }    

    void BulletMoveSet()
    {
        if (currentWeaponType == WeaponType.RPG)
            bulletSpeed *= -1f;                   
    }

    void RecoilSet()
    {
        if (currentWeaponType == WeaponType.RPG) return;
        
        else if (currentWeaponType == WeaponType.SG)
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
        switch(currentWeaponType)
        {
            case WeaponType.Pistol:
            case WeaponType.SMG:
                bulletSpeed = 50f;
                damage = 20;
                break;
            case WeaponType.SG:
                bulletSpeed = 60f;
                destroyTime = 0.1f;
                damage = 15;
                break;
            case WeaponType.AR:
            case WeaponType.LMG:
                bulletSpeed = 50f;
                damage = 30;
                break;
            case WeaponType.RPG:
                
                bulletSpeed = 25f;
                destroyTime = 1f;
                damage = 100;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("!");
            if (currentWeaponType == WeaponType.RPG)
            {
                GameObject obj = Instantiate(explosion, this.transform.position, Quaternion.identity);
                obj.GetComponent<Explosion>().damage = this.damage;
                Destroy(this.gameObject);
            }
            else
            {
                Monster monster = other.gameObject.GetComponent<Monster>();
                monster.currentHp -= damage;
                Destroy(this.gameObject);
            }
        }
    }
}
