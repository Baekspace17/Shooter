using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;

    public GameObject _PlayerPrefab;
    public GameObject _Player;
    public GameObject _DashTrail;
    public PlayerController _Pctrl;
    public PlayerStat _Pstat;
    public UI ui;

    public List<GameObject> _WeaponPrefabs;
    public List<GameObject> _ItemPrefabs;
    public List<GameObject> _MuzzlePrefabs;
    public List<GameObject> _BulletPrefabs;
    public List<GameObject> _MonsterPrefabs;

    public float SpawnTime = 2f;
    public bool gameOver = false;
    public int Score;

    private void Awake()
    {
        if(_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {        
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_Pstat.isDead)
        {
            SpawnTime -= Time.deltaTime;
            if (SpawnTime <= 0) CreateMonster();
        }
        else
        {
            ui.playerStatObj.SetActive(false);
            ui.gameOver.SetActive(true);
            gameOver = true;
        }
    }

    void GameStart()
    {
        Cursor.visible = false;
        CreatePlayer();
        CreateWeapon();
    }

    void Init()
    {
        _PlayerPrefab = Resources.Load<GameObject>("Prefabs/Player");

        GetWeaponPrefabs();
        GetItemPrefabs();
        GetMuzzlePrefabs();
        GetBulletPrefabs();
        GetMonsterPrefabs();
    }

    void CreatePlayer()
    {
        _Player = Instantiate(_PlayerPrefab, Vector3.zero, Quaternion.identity);
        _Pctrl = _Player.GetComponent<PlayerController>();
        _Pstat = _Player.GetComponent<PlayerStat>();
        _DashTrail = new GameObject("DashTrail");
        _DashTrail.AddComponent<DashTrail>();
    }

    void CreateWeapon()
    {
        foreach(GameObject obj in _WeaponPrefabs)
        {
            GameObject weapon = Instantiate(obj, _Pstat.weaponRoot);
            _Pstat.weapons.Add(weapon);            
            weapon.SetActive(false);
        }        
    }

    void CreateMonster()
    {
        int num = Random.Range(0, _MonsterPrefabs.Count);
        float x = Random.Range(-47, 47);
        float z = Random.Range(-47, 47);
        Vector3 SpawnPoint = new Vector3(x, 0, z);
        if((SpawnPoint - _Player.transform.position).magnitude > 1)
        {
            Instantiate(_MonsterPrefabs[num], SpawnPoint, Quaternion.identity);
            SpawnTime = 2f;
        }
    }

    void GetWeaponPrefabs()
    {
        GameObject[] getObj = Resources.LoadAll<GameObject>("Prefabs/Weapons/");
        foreach(GameObject obj in getObj)
        {
            _WeaponPrefabs.Add(obj);
        }        
    }

    void GetItemPrefabs()
    {
        GameObject[] getObj = Resources.LoadAll<GameObject>("Prefabs/Items/");
        foreach (GameObject obj in getObj)
        {
            _ItemPrefabs.Add(obj);
        }
    }

    void GetMuzzlePrefabs()
    {
        GameObject[] getObj = Resources.LoadAll<GameObject>("Prefabs/Muzzle/");
        foreach (GameObject obj in getObj)
        {
            _MuzzlePrefabs.Add(obj);
        }
    }

    void GetBulletPrefabs()
    {
        GameObject[] getObj = Resources.LoadAll<GameObject>("Prefabs/Bullet/");
        foreach (GameObject obj in getObj)
        {
            _BulletPrefabs.Add(obj);
        }
    }

    void GetMonsterPrefabs()
    {
        GameObject[] getObj = Resources.LoadAll<GameObject>("Prefabs/Monster/");
        foreach (GameObject obj in getObj)
        {
            _MonsterPrefabs.Add(obj);
        }
    }
}
