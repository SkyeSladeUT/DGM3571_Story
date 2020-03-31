﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    public float ForwardSpeed, SideSpeed, RotateSpeed;
    private float forwardAmount, sideAmount, headingAngle;
    public string ForwardAxis, SideAxis;
    public Transform Camera;
    private CharacterController _cc;
    private Vector3 _moveVec, _rotVec;
    private Quaternion quat;
    public Targeting targetScript;
    

    private void Start()
    {
        _cc = GetComponent<CharacterController>();
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            //Get  ForwardAxis
            forwardAmount = Input.GetAxis(ForwardAxis);
            //Get SideAxis
            sideAmount = Input.GetAxis(SideAxis);
            _moveVec = Camera.forward * forwardAmount * ForwardSpeed * Time.deltaTime
                       + Camera.right * sideAmount * SideSpeed * Time.deltaTime;
            _moveVec.y = 0;
            _cc.Move(_moveVec);
            if (!targetScript.targeting && (sideAmount >= .1f || sideAmount <= -.1f || forwardAmount >= .1f || forwardAmount <= -.1f))
            {
                headingAngle = Quaternion.LookRotation(_moveVec).eulerAngles.y;
                _rotVec = new Vector3(transform.rotation.x, headingAngle, transform.rotation.z);
                //Debug.Log(_rotVec);
                quat = Quaternion.Euler(_rotVec);
                transform.rotation = Quaternion.Lerp(transform.rotation, quat, RotateSpeed * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForFixedUpdate();
            

        }

    }


}
