using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Cinemachine;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CameraSwitch : MonoBehaviour
{
    public CameraBase ThirdPersonCam;
    public CameraBase BowCam;

    [HideInInspector]
    public bool bowCam = false;
    public string CameraSwitchButton;

    private void Start()
    {
        SetThirdPerson();
        bowCam = false;
    }

    public void SetBowCam()
    {
        if (!bowCam)
        {
            Debug.Log("Bow Cam");
            BowCam.gameObject.SetActive(true);
            ThirdPersonCam.gameObject.SetActive(false);
            BowCam.StartMove();
            ThirdPersonCam.StopMove();
            bowCam = true;
        }
    }

    public void SetThirdPerson()
    {
        if (bowCam)
        {
            Debug.Log("Third Person Cam");
            ThirdPersonCam.gameObject.SetActive(true);
            BowCam.gameObject.SetActive(false);
            ThirdPersonCam.StartMove();
            BowCam.StopMove();
            bowCam = false;
        }
    }

    public void SwitchCam()
    {
        if (bowCam)
        {
            SetThirdPerson();
        }
        else
        {
            SetBowCam();
        }
    }
    
}
