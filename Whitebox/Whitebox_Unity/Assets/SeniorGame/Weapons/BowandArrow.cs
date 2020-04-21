using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BowandArrow : WeaponBase
{
    //hold e to increase power
    //release e to shoot
    //Shoots straight forward from player
    
    private WaitForFixedUpdate _fixedUpdate = new WaitForFixedUpdate();
    private WaitUntil _waitforbutton;
    public GameObject ArrowPrefab;
    public Transform InitPos;
    private Rigidbody ArrowRB;
    public string useButton;
    private float currPower;
    public float MaxPower, PowerIncreaseScale;
    private GameObject currArrow;
    private Vector3 direction;
    public TransformData targetObj;
    public Transform camTrans;
    private Vector3 rotDirection, initRotation;
    public GameObject WeaponObj;
    
    public override void Initialize()
    {
        //any init stuff needed
        initRotation = transform.rotation.eulerAngles;
        _waitforbutton = new WaitUntil(CheckInput);
        currWeapon = true;
        WeaponObj.SetActive(true);
        attack = Attack();
        StartCoroutine(attack);

    }

    public override IEnumerator Attack()
    {
        while (currWeapon)
        {
            rotDirection = initRotation;
            rotDirection.y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(rotDirection);
            yield return _waitforbutton;
            inUse = true;
            if (currWeapon)
            {
                currPower = 0;
                currArrow = Instantiate(ArrowPrefab, InitPos);
                currArrow.SetActive(true);
                ArrowRB = currArrow.GetComponent<Rigidbody>();
                while (Input.GetButton(useButton))
                {
                    if (targetObj.transform != null)
                    {
                        transform.LookAt(targetObj.transform);
                    }
                    else
                    {
                        rotDirection = transform.rotation.eulerAngles;
                        rotDirection.x = camTrans.rotation.eulerAngles.x;
                        transform.rotation = Quaternion.Euler(rotDirection);
                    }

                    Debug.Log("Current Power: " + currPower);
                    currPower += Time.deltaTime * PowerIncreaseScale;
                    if (currPower >= MaxPower)
                    {
                        currPower = MaxPower;
                    }

                    yield return _fixedUpdate;
                }
                ArrowRB.constraints = RigidbodyConstraints.None;
                currArrow.transform.parent = null;
                ArrowRB.AddForce(transform.forward * currPower, ForceMode.Impulse);
                inUse = false;
            }

        }
    }
    

    public override void End()
    {
        //and end stuff needed
        WeaponObj.SetActive(false);
        currWeapon = false;
        inUse = false;
        StopCoroutine(attack);
    }
    
    private bool CheckInput()
    {
        if (Input.GetButtonDown(useButton) || !currWeapon)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
