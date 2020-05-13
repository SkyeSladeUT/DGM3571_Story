using System.Collections;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    public float ForwardSpeed, SideSpeed, RotateSpeed, JumpSpeed, Gravity;
    private float forwardAmount, sideAmount, headingAngle, vSpeed;
    public string ForwardAxis, SideAxis;
    public Transform Camera;
    private CharacterController _cc;
    private Vector3 _moveVec, _rotVec, _jumpVec;
    private Quaternion quat;
    public Targeting targetScript;
    private bool canMove;
    

    private void Start()
    {
        canMove = true;
        _cc = GetComponent<CharacterController>();
        _moveVec = Vector3.zero;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            if (canMove)
            {
                Invoke();
                sideAmount = Input.GetAxis(SideAxis);
                forwardAmount = Input.GetAxis(ForwardAxis);
                if (!targetScript.targeting && (sideAmount >= .1f || sideAmount <= -.1f || forwardAmount >= .1f ||
                                                forwardAmount <= -.1f))
                {
                    headingAngle = Quaternion.LookRotation(_moveVec).eulerAngles.y;
                    _rotVec = new Vector3(transform.rotation.x, headingAngle, transform.rotation.z);
                    quat = Quaternion.Euler(_rotVec);
                    transform.rotation = Quaternion.Lerp(transform.rotation, quat, RotateSpeed * Time.deltaTime);
                    //yield return new WaitForFixedUpdate();
                }
            }
            else
            {
                vSpeed -= Gravity * Time.deltaTime;
                _moveVec = Vector3.zero;
                _moveVec.y = vSpeed;
                _cc.Move(_moveVec * Time.deltaTime);
            }
            yield return new WaitForFixedUpdate();
        }

    }

    public void SetMove(bool value)
    {
        canMove = value;
    }

    public virtual void Invoke()
    {
        _moveVec = Camera.forward * ForwardSpeed * Input.GetAxis("Vertical") +
                   Camera.right * SideSpeed * Input.GetAxis("Horizontal");
        _moveVec.y = 0;
        //_moveVec = new Vector3(Input.GetAxis("Horizontal")*ForwardSpeed, 0, Input.GetAxis("Vertical")*SideSpeed);
        //_moveVec = transform.TransformDirection(_moveVec);
        if (_cc.isGrounded) {
            vSpeed = -1;
            if (Input.GetButtonDown ("Jump")) {
                vSpeed = JumpSpeed;
            }
        }
        vSpeed -= Gravity * Time.deltaTime;
        _moveVec.y = vSpeed;
        _cc.Move(_moveVec * Time.deltaTime);
    }

}
