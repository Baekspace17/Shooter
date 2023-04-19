using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject Weapon;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        DeadCheck();
        if (isDead)
        {
            Dead();
        }        
    }



    void Init()
    {
        switch(type)
        {
            case MonsterType.Red:
                maxHp = 100;
                currentHp = maxHp;
                moveSpeed = 6f;
                break;
            case MonsterType.Blue:
                maxHp = 100;
                currentHp = maxHp;
                moveSpeed = 9f;
                break;
            case MonsterType.Green:
                maxHp = 150;
                currentHp = maxHp;
                moveSpeed = 5f;
                break;
            case MonsterType.Boss:
                maxHp = 1000;
                currentHp = maxHp;
                moveSpeed = 3f;
                break;
        }
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
        // ÀÌÆåÆ®³ª Á×´Â ¸ð¼Ç
        Destroy(this.gameObject);
    }
}
