using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb;
    public PlayerStat stat;
    public Transform MuzzleRoot;
    public Transform shootPoint;
    public GameObject muzzle;

    [Header("움직임")]
    public Vector2 moveValue;
    public Vector2 moveValue2;
    public Vector3 currentVelocity;
    public Vector3 dashVelocity;
    public Vector3 dashPos;
    public Vector3 movePosition;

    [Header("스피드")]
    public float targetSpeed;
    public float moveSpeed = 6f;
    public float dashPower = 15f;

    [Header("상태")]
    public bool isDash;
    public bool dashInput;
    public bool usePistol;
    public bool useRifle;
    public bool isFire;

    [Header("쿨타임")]
    public float fireRate;
    public float dashCoolTime = 0f;
    public float fireCoolTime = 0f;
    

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        stat = GetComponent<PlayerStat>();
        shootPoint = transform.Find("ShootPoint");
    }
    void Update()
    {
        InputMove();
        LookAtMouse();        
        Dash();
        Fire();
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputMove()
    {
        moveValue = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));        
        currentVelocity = new Vector3(moveValue.x, 0f, moveValue.y).normalized;
    }

    void LookAtMouse()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
            Debug.DrawLine(cameraRay.origin, pointTolook, Color.red);
        }
    }

    void Move()
    {
        if (!isDash)
        {
            targetSpeed = moveSpeed;
            if (currentVelocity == Vector3.zero) targetSpeed = 0;
            movePosition = Vector3.Lerp(movePosition, currentVelocity, 5f * Time.deltaTime);
        }
        else movePosition = dashPos;

        playerRb.AddForce(movePosition * targetSpeed, ForceMode.VelocityChange);
    }

    void Dash()
    {
        dashCoolTime -= Time.deltaTime;
        if (dashCoolTime < 0) dashCoolTime = 0;

        if (dashCoolTime <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            dashPos = transform.TransformDirection(Vector3.back);
            targetSpeed = dashPower;
            isDash = true;
            dashCoolTime = 3f;
            Invoke("EndDash", 0.3f);
        }
    }

    void EndDash()
    {
        targetSpeed = moveSpeed;
        isDash = false;
    }

    void Fire()
    {
        fireCoolTime -= Time.deltaTime;
        if (fireCoolTime < 0) fireCoolTime = 0;

        if (stat.currentWeaponType != WeaponType.None)
        {
            fireRate = stat.currentWeapon.fireRate;
            if (stat.currentWeaponType == WeaponType.RPG && fireCoolTime <= 0)
            {
                stat.weapons[(int)stat.currentWeaponType - 1].transform.Find("Rocket").gameObject.SetActive(true);
            }

            if (fireCoolTime <= 0 && Input.GetKey(KeyCode.Mouse0))
            {
                if (stat.currentWeaponType == WeaponType.RPG)
                {
                    stat.weapons[(int)stat.currentWeaponType - 1].transform.Find("Rocket").gameObject.SetActive(false);
                }

                if (stat.bulletCount[(int)stat.currentWeaponType - 1] > 0)
                {
                    stat.bulletCount[(int)stat.currentWeaponType - 1]--;
                    //Debug.Log("파이어 " + stat.currentWeapon + stat.bulletCount[(int)stat.currentWeaponType - 1]);
                    fireCoolTime = fireRate;
                    bulletCreate();
                    MuzzleCreate();
                }
            }            
        }
    }

    void bulletCreate()
    {
        switch (stat.currentWeaponType)
        {
            case WeaponType.Pistol:
                Instantiate(GameManager._Instance._BulletPrefabs[1], shootPoint.Find("Pistol").transform.position, transform.rotation);
                break;
            case WeaponType.SMG:
                Instantiate(GameManager._Instance._BulletPrefabs[1], shootPoint.Find("SMG").transform.position, transform.rotation);
                break;
            case WeaponType.SG:
                for(int i = 0; i< 10; i++)
                {                    
                    Instantiate(GameManager._Instance._BulletPrefabs[2], shootPoint.Find("SG").transform.position, transform.rotation);
                }                
                break;
            case WeaponType.AR:
                Instantiate(GameManager._Instance._BulletPrefabs[2], shootPoint.Find("AR").transform.position, transform.rotation);
                break;
            case WeaponType.LMG:
                Instantiate(GameManager._Instance._BulletPrefabs[0], shootPoint.Find("LMG").transform.position, transform.rotation);
                break;
            case WeaponType.RPG:
                Quaternion q = Quaternion.Euler(0f, 180f, 0f);
                Instantiate(GameManager._Instance._BulletPrefabs[4], shootPoint.Find("RPG").transform.position, transform.rotation * q);
                break;
        }
    }

    void MuzzleCreate()
    {
        int num = Random.Range(0, GameManager._Instance._MuzzlePrefabs.Count-1);
        MuzzleRoot = stat.weapons[(int)stat.currentWeaponType - 1].transform.Find("MuzzlePoint");
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

    void GetItem(GameObject obj)
    {
        Item item = obj.GetComponent<Item>();
        if (item.itemType == ItemType.Gold)
        {
            stat.gold += item.gold;
        }
        if (item.itemType == ItemType.Weapon)
        {
            stat.getItemWeapon = item.weaponScript.wType;
            if (stat.currentWeaponType == WeaponType.None)
            {
                stat.UseWeapon();
            }
            if (stat.currentWeaponType != stat.getItemWeapon)
            {
                ChangeWeaponAnim();
                stat.Invoke("SwapWeapon", 0.4f);
            }

            stat.bulletCount[(int)stat.getItemWeapon - 1] = item.weaponScript.bulletCount;
        }
        if (item.itemType == ItemType.Heal)
        {
            stat.currentHp += stat.maxHp * item.healPoint;
        }
    }

    public void ChangeWeaponAnim()
    {
        switch (stat.currentWeaponType)
        {
            case WeaponType.Pistol:
            case WeaponType.SMG:
            case WeaponType.RPG:
                usePistol = !usePistol;
                break;
            case WeaponType.AR:
            case WeaponType.SG:
            case WeaponType.LMG:
                useRifle = !useRifle;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            GetItem(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}


