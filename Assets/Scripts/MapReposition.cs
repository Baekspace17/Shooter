using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MapReposition : MonoBehaviour
{
    public GameObject player;
    public Vector3 playerPos;
    public Vector3 myPos;
    public Vector2 distance;
    public Vector2 direction;

    // Update is called once per frame
    void Update()
    {
        if (player == null && GameManager._Instance._Player != null) player = GameManager._Instance._Player;
        if (player == null) return;
        
        playerPos = player.transform.position;
        myPos = transform.position;

        distance = new Vector2(playerPos.x - myPos.x, playerPos.z - myPos.z);
        if (distance.x < 0) direction.x = distance.x * -1;
        else direction.x = distance.x;
        if (distance.y < 0) direction.y = distance.y * -1;
        else direction.y = distance.y;

        if (direction.x >= 20)
        {
            if (distance.x < 0) myPos.x -= 20;
            else myPos.x += 20;
        }
        if (direction.y >= 20)
        {
            if (distance.y < 0) myPos.z -= 20;
            else myPos.z += 20;
        }

        transform.position = myPos;
    }
}
