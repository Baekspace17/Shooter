using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    public Animator animator;
    public Monster mob;

    //public Transform animCam;
    //public Vector3 animCamForward;
    //public Vector3 moveValue;
    //public Vector3 animMove;
    //public Vector3 animMoveInput;
    //public float turnValue;
    //public float forwardValue;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        mob = GetComponent<Monster>();

        //animCam = Camera.main.transform;
    }

    void FixedUpdate()
    {
        //AnimCamSet();
        UpdateAnim();
    }

    //void AnimCamSet()
    //{
    //    moveValue = mob.nav.destination;
    //    if (animCam != null)
    //    {
    //        animCamForward = Vector3.Scale(animCam.up, new Vector3(1, 0, 1)).normalized;
    //        animMove = moveValue.y * animCamForward + moveValue.x * animCam.right;
    //    }
    //    else
    //    {
    //        animMove = moveValue.y * Vector3.forward + moveValue.x * Vector3.right;
    //    }

    //    AnimMoveRotationSet(animMove);
    //}
    //void AnimMoveRotationSet(Vector3 move)
    //{
    //    if (move.magnitude > 1)
    //    {
    //        move.Normalize();
    //    }

    //    animMoveInput = move;

    //    ConvertMoveInput();
    //    UpdateAnim();
    //}

    //void ConvertMoveInput()
    //{
    //    Vector3 localMove = transform.InverseTransformDirection(animMoveInput);
    //    turnValue = localMove.x;
    //    forwardValue = localMove.z;
    //}

    void UpdateAnim()
    {
        animator.SetBool("Die", mob.isDead);
        animator.SetBool("Move", mob.move);
        animator.SetBool("UsePistol", mob.usePistol);
        animator.SetBool("UseRifle", mob.useRifle);
    }
}
