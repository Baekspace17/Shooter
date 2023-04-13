using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;

    public GameObject _PlayerPrefab;
    public GameObject _Player;
    public PlayerController _Pctrl;
    public PlayerStat _Pstat;

    public List<GameObject> _WeaponPrefabs;
    public List<GameObject> _Weapon;
    public List<GameObject> _ItemPrefabs;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (_Pstat.isDead) Application.Quit();
    }

    void GameStart()
    {
        CreatePlayer();
        CreateWeapon();
    }

    void Init()
    {
        _PlayerPrefab = Resources.Load<GameObject>("Prefabs/Player");

        GetWeaponPrefabs();
        GetItemPrefabs();
    }

    void CreatePlayer()
    {
        _Player = Instantiate(_PlayerPrefab, Vector3.zero, Quaternion.identity);
        _Pctrl = _Player.GetComponent<PlayerController>();
        _Pstat = _Player.GetComponent<PlayerStat>();
    }

    void CreateWeapon()
    {
        foreach(GameObject obj in _WeaponPrefabs)
        {
            GameObject weapon = Instantiate(obj, _Pctrl.weaponRoot.transform);
            _Pstat.weapons.Add(weapon);
            weapon.SetActive(false);
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
}
