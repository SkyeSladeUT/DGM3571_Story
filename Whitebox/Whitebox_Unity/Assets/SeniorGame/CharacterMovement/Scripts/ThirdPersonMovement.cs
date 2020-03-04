using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    /*public float ForwardSpeed, RotateSpeed;
    public string ForwardAxis, RotateAxis;
    private CharacterController _cc;
    private Vector3 _moveVec, _rotVec;
    

    private void Start()
    {
        _cc = GetComponent<CharacterController>();
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            _moveVec = transform.forward*Input.GetAxis(ForwardAxis)*ForwardSpeed*Time.deltaTime;
            _rotVec.Set(0,Input.GetAxis(RotateAxis) * RotateSpeed,0);
            _cc.Move(_moveVec);
            transform.Rotate(_rotVec);
            yield return new WaitForFixedUpdate();
        }
    }*/
    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            Debug.Log(Input.GetAxis("Horizontal"));
        }
    }
}
