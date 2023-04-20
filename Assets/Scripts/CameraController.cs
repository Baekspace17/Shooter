using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform playerTr;
    public float smoothing = 0.1f;
    public float camZoffset = -10f;

    void Start()
    {
        
    }

    void Update()
    {
        if(playerTr == null) playerTr = GameManager._Instance._Player.transform;
        else
        {
            Vector3 targetPos = new Vector3(playerTr.position.x, transform.position.y, playerTr.position.z + camZoffset);
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }    
}
