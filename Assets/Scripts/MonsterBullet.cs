using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public int damage = 10;
    public float bulletSpeed = 30f;
    public float destroyTime = 0.5f;
    
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        destroyTime -= Time.deltaTime;
        if (destroyTime < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStat player = other.gameObject.GetComponent<PlayerStat>();
            player.currentHp -= damage;
            Destroy(this.gameObject);
        }
    }
}
