using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThridPersonCamera : MonoBehaviour
{
    private bool canMove = true;
    private Vector3 movement, rotation, offset;
    private Quaternion quat;
    public Transform followObj;
    private float x, y;
    public float CamSpeed;
    public float sensitivity;
    public float CamFollowSpeed;

    private void Start()
    {
        offset = transform.position - followObj.position;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(CamMove());
    }

    private IEnumerator CamMove()
    {
        while (canMove)
        {
            movement = Vector3.Lerp(transform.position, followObj.position + offset, CamFollowSpeed*Time.deltaTime);
            transform.position = movement;
            x = Input.GetAxis("Mouse X");
            y = Input.GetAxis("Mouse Y");
            rotation = transform.rotation.eulerAngles;
            rotation.x -= y * CamSpeed;
            rotation.y += x * CamSpeed;
            rotation.z = 0;
            quat = Quaternion.Euler(rotation);
            //transform.rotation = quat;
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, sensitivity*Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
