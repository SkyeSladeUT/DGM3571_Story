using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraBase : MonoBehaviour
{
    protected bool canMove;
    protected Coroutine moveFunc;
    public Transform FollowObj; //Attached to Rotate Object, out as far as Camera
    protected float mouseX, mouseY;
    public float rotationSpeed;
    public Transform RotateObject; // Centered at Player
    public string CameraHorizontal = "Mouse X", CameraVertical = "Mouse Y";


    public virtual void StartMove()
    {
        canMove = true;
        moveFunc = StartCoroutine(Move());
    }
    
    public abstract IEnumerator Move();

    public virtual void StopMove()
    {
        canMove = false;
        if(moveFunc != null)
            StopCoroutine(moveFunc);
    }
}
