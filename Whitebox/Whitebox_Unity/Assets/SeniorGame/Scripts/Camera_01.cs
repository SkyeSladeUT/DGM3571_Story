using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_01 : MonoBehaviour
{
    public Transform FollowObj; //Attached to Rotate Object, out as far as Camera
    private float mouseX, mouseY;
    public float rotationSpeed;
    public Transform RotateObject; // Centered at Player

    public string MouseHorizontal, MouseVertical;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        transform.position = FollowObj.transform.position;
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -35, 60);
        RotateObject.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }
}
