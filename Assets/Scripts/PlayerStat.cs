using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public float currentHp;
    public float maxHp = 100f;
    public bool isDead;

    public int gold = 0;

    public Transform weaponRoot;
    public PlayerController player;

    public List<GameObject> weapons; // �÷��̾� ���� �� setActive��
    public Weapon currentWeapon;
    public WeaponType currentWeaponType; // ���� ����Ÿ�� ����
    public WeaponType getItemWeapon; // ������ȹ��� ����
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
        if (isDead) Dead();
    }

    void FixedUpdate()
    {
        WeaponCheck();
    }

    void Init()
    {
        player = GetComponent<PlayerController>();
        currentHp = maxHp;
        bulletCount = new int[6];
    }
    public void WeaponCheck()
    {
        if (currentWeaponType != WeaponType.None && bulletCount[(int)currentWeaponType - 1] <= 0)
        {            
            getItemWeapon = WeaponType.Pistol;
            bulletCount[(int)getItemWeapon - 1] = 10000;
            player.ChangeWeaponAnim();
            Invoke("SwapWeapon", 0.4f);            
        }
    }

    public void UseWeapon()
    {        
        currentWeaponType = getItemWeapon;
        currentWeapon = weapons[(int)currentWeaponType - 1].GetComponent<Weapon>();        
        weapons[(int)currentWeaponType - 1].SetActive(true);
        player.ChangeWeaponAnim();
        for (int i = 0; i < bulletCount.Length; i++)
        {
            if(i != (int)currentWeaponType - 1) bulletCount[i] = 0;
        }
    }

    public void SwapWeapon()
    {        
        weapons[(int)currentWeaponType - 1].SetActive(false);
        UseWeapon();
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

    void Dead()
    {
        
    }

    public void Heal(int hp)
    {
        currentHp += hp;
        CheckHp();
    }        
}
