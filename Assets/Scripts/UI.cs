using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public RectTransform crossHair;
    public TextMeshProUGUI scoreText;
    public Slider healthBar;
    public Slider dashCooltime;
    public TextMeshProUGUI ammoText;
    public GameObject gameOver;
    public GameObject playerStatObj;
    public PlayerStat statScript;

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
            statScript = GameManager._Instance._Pstat;
            playerStatObj.SetActive(true);
            SliderSet();
            TextSet();
        }
    }
    void SliderSet()
    {
        healthBar.value = statScript.currentHp / statScript.maxHp;
        dashCooltime.value = GameManager._Instance._Pctrl.dashCoolTime / 3f;
    }

    void TextSet()
    {
        scoreText.text = "Score : " + GameManager._Instance.Score;

        switch (statScript.currentWeaponType)
        {
            case WeaponType.None:
                ammoText.text = "0";
                ammoText.fontSize = 25;
                ammoText.rectTransform.anchoredPosition = Vector2.zero;
                ammoText.color = new Color(1f, 1f, 1f, 1f);
                break;
            case WeaponType.Pistol:
                ammoText.text = "¡Ä";
                ammoText.fontSize = 55;
                ammoText.rectTransform.anchoredPosition = new Vector2(0f, 3f);
                ammoText.color = new Color(1f, 1f, 1f, 1f);
                break;
            case WeaponType.SMG:
                ammoText.text = statScript.bulletCount[(int)statScript.currentWeaponType - 1].ToString();
                ammoText.fontSize = 25;
                ammoText.rectTransform.anchoredPosition = Vector2.zero;
                ammoText.color = new Color(1f, 0f, 1f, 1f);
                break;
            case WeaponType.AR:
                ammoText.text = statScript.bulletCount[(int)statScript.currentWeaponType - 1].ToString();
                ammoText.fontSize = 25;
                ammoText.rectTransform.anchoredPosition = Vector2.zero;
                ammoText.color = new Color(0f, 1f, 1f, 1f);
                break;
            case WeaponType.SG:
                ammoText.text = statScript.bulletCount[(int)statScript.currentWeaponType - 1].ToString();
                ammoText.fontSize = 25;
                ammoText.rectTransform.anchoredPosition = Vector2.zero;
                ammoText.color = new Color(1f, 1f, 0f, 1f);
                break;
            case WeaponType.LMG:
                ammoText.text = statScript.bulletCount[(int)statScript.currentWeaponType - 1].ToString();
                ammoText.fontSize = 25;
                ammoText.rectTransform.anchoredPosition = Vector2.zero;
                ammoText.color = new Color(0.5f, 0f, 1f, 1f);
                break;
            case WeaponType.RPG:
                ammoText.text = statScript.bulletCount[(int)statScript.currentWeaponType - 1].ToString();
                ammoText.fontSize = 25;
                ammoText.rectTransform.anchoredPosition = Vector2.zero;
                ammoText.color = new Color(1f, 0f, 0f, 1f);
                break;
        }
    }
}
