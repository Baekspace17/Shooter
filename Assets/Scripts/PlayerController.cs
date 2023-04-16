using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("ÄÄÆ÷³ÍÆ®")]
    public Rigidbody playerRb;
    public Animator animator;
    public PlayerStat stat;    

    [HideInInspector] public Transform animCam;
    [HideInInspector] public Vector3 animCamForward;
    [HideInInspector] public Vector3 animMove;
    [HideInInspector] public Vector3 animMoveInput;
    [HideInInspector] public float turnValue;
    [HideInInspector] public float forwardValue;

    [Header("¿òÁ÷ÀÓ")]
    public Vector2 moveValue;
    public Vector2 moveValue2;
    public Vector3 currentVelocity;
    public Vector3 dashVelocity;
    public Vector3 dashPos;
    public Vector3 movePosition;

    [Header("½ºÇÇµå")]
    public float targetSpeed;
    public float moveSpeed = 6f;
    public float dashPower = 15f;

    [Header("»óÅÂ")]
    public bool isDash;
    public bool dashInput;
    public bool usePistol;
    public bool useRifle;
    public bool isFire;

    [Header("ÄðÅ¸ÀÓ")]
    public float dashCoolTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        stat = GetComponent<PlayerStat>();

        animCam = Camera.main.transform;
    }
    void Update()
    {
        InputMove();
        LookAtMouse();        
        Dash();        
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

    void InputMove()
    {
        moveValue = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));        
        currentVelocity = new Vector3(moveValue.x, 0f, moveValue.y).normalized;
    }

    void Move()
    {
        if (!isDash)
        {
            targetSpeed = moveSpeed;
            if (currentVelocity == Vector3.zero) targetSpeed = 0;
        }
        else movePosition = dashPos;        

        movePosition = Vector3.Lerp(movePosition, currentVelocity, 5f * Time.deltaTime);

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
        if(stat.currentWeapon != WeaponType.None)
        {
            
        }
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
            // ÃÑ¾Ë / È¸º¹ ¾ÆÀÌÅÛ È¹µæ½Ã
                                            
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
        animator.SetFloat("MoveX", turnValue * 2f, 0.1f, Time.deltaTime);
        animator.SetFloat("MoveY", forwardValue * 2f, 0.1f, Time.deltaTime);
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


