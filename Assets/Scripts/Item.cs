using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Gold,
    Weapon,
    Heal
}
public class Item : MonoBehaviour
{
    public float rotSpeed = 50f;
    bool objUp = false;
    public float upDownSpeed = 2f;
    public ItemType itemType;
    public int gold = 0;
    public float healPoint = 0f;
    public Weapon weaponScript;

    // Start is called before the first frame update
    void Start()
    {
        SetItem(itemType);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (transform.position.y > 1f) objUp = false;
        else if (transform.position.y < 0.7f) objUp = true;
        if (objUp) transform.position += new Vector3(0f, upDownSpeed / 10 * Time.deltaTime, 0f);
        else transform.position -= new Vector3(0f, upDownSpeed / 10 * Time.deltaTime, 0f);

        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
    }

    void SetItem(ItemType type)
    {
        if (type == ItemType.Gold)
        {            
            gold = (int)Random.Range(20, 50);
        }
        if (type == ItemType.Weapon)
        {            
            weaponScript = transform.GetChild(0).GetComponent<Weapon>();            
        }
        if (type == ItemType.Heal)
        {
            healPoint = 0.3f;
        }
    }
}