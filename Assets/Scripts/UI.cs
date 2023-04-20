using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public RectTransform crossHair;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI weaponText;
    public GameObject gameOver;
    public PlayerStat stat;

    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        crossHair.position = Input.mousePosition;

        if (GameManager._Instance._Pstat != null)
        {
            stat = GameManager._Instance._Pstat;
            TextSet();
        }
    }

    void TextSet()
    {
        healthText.text = "Health " + stat.currentHp.ToString();
        scoreText.text = "Score : " + GameManager._Instance.Score;
        switch (stat.currentWeaponType)
        {
            case WeaponType.None:
                ammoText.text = "0";
                weaponText.text = "None";
                break;
            case WeaponType.Pistol:
                ammoText.text = "Infinity";
                weaponText.text = "Pistol";
                break;
            case WeaponType.SMG:
                ammoText.text = "Ammo " + stat.bulletCount[(int)stat.currentWeaponType - 1].ToString();
                weaponText.text = "SMG";
                break;
            case WeaponType.AR:
                ammoText.text = "Ammo " + stat.bulletCount[(int)stat.currentWeaponType - 1].ToString();
                weaponText.text = "Rifle";
                break;
            case WeaponType.SG:
                ammoText.text = "Ammo " + stat.bulletCount[(int)stat.currentWeaponType - 1].ToString();
                weaponText.text = "ShotGun";
                break;
            case WeaponType.LMG:
                ammoText.text = "Ammo " + stat.bulletCount[(int)stat.currentWeaponType - 1].ToString();
                weaponText.text = "LMG";
                break;
            case WeaponType.RPG:
                ammoText.text = "Ammo " + stat.bulletCount[(int)stat.currentWeaponType - 1].ToString();
                weaponText.text = "RPG";
                break;
        }
    }
}
