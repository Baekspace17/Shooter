using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public Animator animator;
    public PlayerController pc;

    public Transform animCam;
    public Vector3 animCamForward;
    public Vector3 animMove;
    public Vector3 animMoveInput;
    public float turnValue;
    public float forwardValue;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();

        animCam = Camera.main.transform;
    }

    void FixedUpdate()
    {
        AnimCamSet();
    }

    void AnimCamSet()
    {
        if (animCam != null)
        {
            animCamForward = Vector3.Scale(animCam.up, new Vector3(1, 0, 1)).normalized;
            animMove = pc.moveValue.y * animCamForward + pc.moveValue.x * animCam.right;
        }
        else
        {
            animMove = pc.moveValue.y * Vector3.forward + pc.moveValue.x * Vector3.right;
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
        animator.SetBool("UsePistol", pc.usePistol);
        animator.SetBool("UseRifle", pc.useRifle);
    }

}
