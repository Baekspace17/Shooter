using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public int currentHp;
    public int maxHp = 10;
    public bool isDead;

    public int gold = 0;

    public List<GameObject> weapons;
    public bool[] hasWeapons;
    public int[] bulletCount;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHp();
    }
    
    void Init()
    {
        currentHp = maxHp;
        hasWeapons = new bool[6];
        bulletCount = new int[6];
    }

    void CheckHp()
    {
        if(currentHp <= 0)
        {
            currentHp = 0;
            isDead = true;
        }
        if(currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }

    public void Heal(int hp)
    {
        currentHp += hp;
        CheckHp();
    }
}
