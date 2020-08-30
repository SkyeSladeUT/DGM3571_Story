using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : CameraBase
{
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    public override IEnumerator Move()
    {
        while (canMove)
        {
            transform.position = FollowObj.transform.position;
            mouseX += Input.GetAxis(CameraHorizontal) * rotationSpeed * Time.deltaTime;
            mouseY -= Input.GetAxis(CameraVertical) * rotationSpeed * Time.deltaTime;
            mouseY = Mathf.Clamp(mouseY, -35, 60);
            RotateObject.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            yield return new WaitForFixedUpdate();
        }
    }
}
