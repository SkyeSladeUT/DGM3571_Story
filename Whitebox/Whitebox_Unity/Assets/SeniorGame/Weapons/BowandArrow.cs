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
    
    public override void Initialize()
    {
        //any init stuff needed
        _waitforbutton = new WaitUntil(()=>Input.GetButtonDown(useButton));
        currWeapon = true;
        StartCoroutine(Attack());

    }

    public override IEnumerator Attack()
    {
        while (currWeapon)
        {
            yield return _waitforbutton;
            currPower = 0;
            currArrow = Instantiate(ArrowPrefab, InitPos);
            currArrow.SetActive(true);
            ArrowRB = currArrow.GetComponent<Rigidbody>();
            while (Input.GetButton(useButton))
            {
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
            if (targetObj.transform != null)
            {
                transform.LookAt(targetObj.transform);
            }
            ArrowRB.AddForce(transform.forward*currPower, ForceMode.Impulse);  
        }
    }

    public override void End()
    {
        //and end stuff needed
        currWeapon = false;
    }
}
