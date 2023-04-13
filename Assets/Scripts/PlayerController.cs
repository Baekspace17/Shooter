using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("������Ʈ")]
    public Rigidbody playerRb;
    public Animator animator;
    public PlayerStat stat;

    public GameObject weaponRoot;

    [HideInInspector] public Transform animCam;
    [HideInInspector] public Vector3 animCamForward;
    [HideInInspector] public Vector3 animMove;
    [HideInInspector] public Vector3 animMoveInput;
    [HideInInspector] public float turnValue;
    [HideInInspector] public float forwardValue;

    [Header("������")]
    public Vector2 moveValue;
    public Vector3 currentVelocity;
    public Vector3 movePosition;

    [Header("���ǵ�")]
    public float targetSpeed;
    public float animSpeed;
    public float moveSpeed = 3f;
    public float runSpeedRate = 2f;
    public float runOffset;

    [Header("����")]
    public bool isRun;
    public bool usePistol;
    public bool useRifle;
    public bool isFire;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        stat = GetComponent<PlayerStat>();

        animCam = Camera.main.transform;
    }
    void Update()
    {
        InputKey();
        LookAtMouse();
    }

    void FixedUpdate()
    {
        Move();
        if (isFire)
        {
            Fire();
        }
        AnimCamSet();
    }

    void InputKey()
    {
        moveValue = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        currentVelocity = new Vector3(moveValue.x, 0f, moveValue.y).normalized;
        runOffset = 0.5f + (Input.GetAxis("Sprint") * 0.5f);
        isRun = Input.GetKey(KeyCode.LeftShift);        
        if (Input.GetKeyDown(KeyCode.Alpha1)) // ���� Ÿ�Կ� ���� �ִϸ��̼� ���� ����
        {
            if(useRifle) useRifle = !useRifle;
            usePistol = !usePistol;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(usePistol) usePistol = !usePistol;
            useRifle = !useRifle;
        }
        
        animSpeed = 2f * runOffset;
    }

    void Move()
    {
        if (isRun)
        {
            targetSpeed = moveSpeed * runSpeedRate;
        }
        else
        {
            targetSpeed = moveSpeed;
        }

        if (currentVelocity == Vector3.zero) targetSpeed = 0;

        movePosition = Vector3.Lerp(movePosition, currentVelocity, 5f * Time.deltaTime);

        playerRb.AddForce(movePosition * targetSpeed, ForceMode.VelocityChange);
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

    void Fire()
    {

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
            int num = (int)item.weaponScript.wType -1;
            if (num < 0) num = 0;
            if (stat.hasWeapons[num] == false)
            {
                stat.hasWeapons[num] = true;
            }            
            stat.bulletCount[num] += item.weaponScript.bulletCount;                                  
        }

    }

    void AnimCamSet()
    {
        if (animCam != null)
        {
            animCamForward = Vector3.Scale(animCam.up, new Vector3(1, 0, 1)).normalized;
            animMove = moveValue.y * animCamForward + moveValue.x * animCam.right;
        }
        else
        {
            animMove = moveValue.y * Vector3.forward + moveValue.x * Vector3.right;
        }

        AnimMoveRotationSet(animMove);
    }

    void AnimMoveRotationSet(Vector3 move)
    {
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        animMoveInput = move;

        ConvertMoveInput();
        UpdateAnim();
    }

    void ConvertMoveInput()
    {
        Vector3 localMove = transform.InverseTransformDirection(animMoveInput);
        turnValue = localMove.x;
        forwardValue = localMove.z;
    }

    void UpdateAnim()
    {
        animator.SetFloat("MoveX", turnValue * animSpeed, 0.1f, Time.deltaTime);
        animator.SetFloat("MoveY", forwardValue * animSpeed, 0.1f, Time.deltaTime);
        animator.SetBool("UsePistol", usePistol);
        animator.SetBool("UseRifle", useRifle);
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

