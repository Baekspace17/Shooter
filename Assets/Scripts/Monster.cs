using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterType
{
    Red,
    Blue,
    Green,
    Boss
}
public class Monster : MonoBehaviour
{
    public MonsterType type;
    public int currentHp;
    public int maxHp;
    public bool isDead;
    public float moveSpeed;

    public Transform weaponRoot;
    public Transform MuzzleRoot;
    public Transform shootPoint;
    public GameObject muzzle;
    public GameObject weapon;
    public Transform playerPos;
    public float distance;
    public bool usePistol;
    public bool useRifle;
    public bool move;
    public NavMeshAgent nav;

    public float fireRate;
    public float fireCoolTime;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = GameManager._Instance._Player.transform;
        distance = (transform.position - playerPos.position).magnitude;
        fireCoolTime -= Time.deltaTime;
        if (fireCoolTime < 0) fireCoolTime = 0;

        DeadCheck();
        if (isDead)
        {
            Dead();
        }
        else
        {
            LookPlayer();

            if (distance > nav.stoppingDistance)
            {
                Move();
            }
            else
            {
                Attack();
            }
        }        
    }

    void Init()
    {
        nav = GetComponent<NavMeshAgent>();        
        MonsterSet();
        nav.speed = moveSpeed;
        nav.angularSpeed = 200f;
        nav.stoppingDistance = 10f;
    }

    void MonsterSet()
    {
        switch (type)
        {
            case MonsterType.Red:
                maxHp = 70;
                currentHp = maxHp;
                weapon = Instantiate(GameManager._Instance._WeaponPrefabs[0], weaponRoot);
                usePistol = true;
                moveSpeed = 4f;
                fireRate = 1f;
                break;
            case MonsterType.Blue:
                maxHp = 100;
                currentHp = maxHp;
                weapon = Instantiate(GameManager._Instance._WeaponPrefabs[1], weaponRoot);
                usePistol = true;
                moveSpeed = 5f;
                fireRate = 0.8f;
                break;
            case MonsterType.Green:
                maxHp = 150;
                currentHp = maxHp;
                weapon = Instantiate(GameManager._Instance._WeaponPrefabs[3], weaponRoot);
                useRifle = true;
                moveSpeed = 3f;
                fireRate = 1.5f;
                break;
            case MonsterType.Boss:
                maxHp = 1000;
                currentHp = maxHp;
                weapon = Instantiate(GameManager._Instance._WeaponPrefabs[2], weaponRoot);
                useRifle = true;
                moveSpeed = 3f;
                fireRate = 0.6f;
                break;
        }
    }

    void LookPlayer()
    {
        transform.LookAt(playerPos.position);
    }

    void Move()
    {
        nav.SetDestination(playerPos.position);
        move = true;
    }

    void Attack()
    {
        move = false;
        if(fireCoolTime <= 0)
        {
            fireCoolTime = fireRate;
            BulletCreate();
            MuzzleCreate();
        }
    }

    void BulletCreate()
    {
        Instantiate(GameManager._Instance._BulletPrefabs[3], shootPoint.transform.position, transform.rotation);
    }

    void MuzzleCreate()
    {
        int num = Random.Range(0, GameManager._Instance._MuzzlePrefabs.Count - 1);
        MuzzleRoot = weapon.transform.Find("MuzzlePoint");
        muzzle = Instantiate(GameManager._Instance._MuzzlePrefabs[num], MuzzleRoot);
        StopCoroutine(DestroyMuzzle());
        StartCoroutine(DestroyMuzzle());
    }

    IEnumerator DestroyMuzzle()
    {
        yield return new WaitForSeconds(.02f);
        Destroy(muzzle);
        yield return null;
    }

    void DeadCheck()
    {
        if (currentHp <= 0)
        {
            isDead = true;            
        }        
    }

    void Dead()
    {
        Destroy(this.gameObject.GetComponent<CapsuleCollider>());
        Destroy(this.gameObject, 1.5f);
    }
}
