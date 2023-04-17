using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerStat : MonoBehaviour
{
    public int currentHp;
    public int maxHp = 100;
    public bool isDead;

    public int gold = 0;

    public Transform weaponRoot;
    public Transform itemPocketRoot;
    public PlayerController player;

    public List<GameObject> weapons; // 플레이어 무기 모델 setActive용
    public Weapon currentWeapon;
    public WeaponType currentWeaponType; // 현재 무기타입 저장
    public WeaponType getItemWeapon; // 아이템획득시 저장
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
        player = GetComponent<PlayerController>();
        currentHp = maxHp;
        bulletCount = new int[6];        
    }    

    public void UseWeapon()
    {
        currentWeaponType = getItemWeapon;
        weapons[(int)currentWeaponType - 1].SetActive(true);
        currentWeapon = weapons[(int)currentWeaponType - 1].GetComponent<Weapon>();
        player.ChangeWeaponAnim();
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

    public void Heal(int hp)
    {
        currentHp += hp;
        CheckHp();
    }        
}
