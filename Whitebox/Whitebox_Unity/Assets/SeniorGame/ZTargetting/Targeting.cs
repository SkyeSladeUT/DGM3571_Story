using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Targeting : MonoBehaviour
{
    //On Player
    [HideInInspector]public List<GameObject> EnemiesInRange;
    public Transform CamTrans;
    private List<GameObject> CheckEnemies;
    private Vector3 CamPos;
    public bool targeting;
    private bool running;
    private GameObject currentTarget;
    private float minDistance, distance;
    private int minIndex;
    private Vector3 lookatvector;
    public GameObject targetIndicator;
    private Quaternion quat;
    public TransformData currTargetObj;

    private void Start()
    {
        EnemiesInRange = new List<GameObject>();
        running = true;
        targeting = false;
        targetIndicator.SetActive(false);
        targetIndicator.transform.parent = null;
        StartCoroutine(TargetFunction());
    }

    private void findClosest()
    {
        
        if (EnemiesInRange.Count <= 0)
        {
            currentTarget = null;
            targeting = false;
            targetIndicator.transform.parent = null;
            targetIndicator.SetActive(false);
            return;
        }
        else if (EnemiesInRange.Count == 1)
        {
            currentTarget = EnemiesInRange[0];
            targetIndicator.SetActive(true);
            targetIndicator.transform.parent = EnemiesInRange[0].transform;
            targetIndicator.transform.position = EnemiesInRange[0].transform.position;
            return;
        }

        CamPos = CamTrans.gameObject.GetComponent<Camera>().WorldToViewportPoint(EnemiesInRange[0].transform.position);
        minIndex = 0;
        minDistance = Mathf.Sqrt(Mathf.Pow((CamPos.x-.5f)-EnemiesInRange[0].transform.position.x, 2)
                              + Mathf.Pow((CamPos.y-.5f)-EnemiesInRange[0].transform.position.y, 2));
        for (int i = 1; i < EnemiesInRange.Count; i++)
        {
            CamPos = CamTrans.gameObject.GetComponent<Camera>().WorldToViewportPoint(EnemiesInRange[i].transform.position);
            distance = Mathf.Sqrt(Mathf.Pow((CamPos.x-.5f)-EnemiesInRange[0].transform.position.x, 2)
                                  + Mathf.Pow((CamPos.y-.5f)-EnemiesInRange[0].transform.position.y, 2));
            if (distance < minDistance)
            {
                minDistance = distance;
                minIndex = i;
            }
        }
        targetIndicator.SetActive(true);
        targetIndicator.transform.parent = EnemiesInRange[minIndex].transform;
        targetIndicator.transform.position = EnemiesInRange[minIndex].transform.position;
        currentTarget = EnemiesInRange[minIndex];
    }

    private void findRight(GameObject ignoreObj = null)
    {
        if (EnemiesInRange.Count <= 1) return;
        CheckEnemies = EnemiesInRange;
        if (ignoreObj != null)
            CheckEnemies.Remove(ignoreObj);
        CamPos = CamTrans.gameObject.GetComponent<Camera>().WorldToViewportPoint(EnemiesInRange[0].transform.position);
        minIndex = 0;
        minDistance = Mathf.Sqrt(Mathf.Pow((CamPos.x-.5f)-CheckEnemies[0].transform.position.x, 2)
                              + Mathf.Pow((CamPos.y-.5f)-CheckEnemies[0].transform.position.y, 2));
        for (int i = 1; i < EnemiesInRange.Count; i++)
        {
            CamPos = CamTrans.gameObject.GetComponent<Camera>().WorldToViewportPoint(EnemiesInRange[i].transform.position);
            distance = Mathf.Sqrt(Mathf.Pow((CamPos.x-.5f)-CheckEnemies[0].transform.position.x, 2)
                                  + Mathf.Pow((CamPos.y-.5f)-CheckEnemies[0].transform.position.y, 2));
            if ((distance < minDistance) && (((CamPos.x-.5f)- CheckEnemies[i].transform.position.x) < 0))
            {
                minDistance = distance;
                minIndex = i;
            }
        }
        targetIndicator.transform.parent = EnemiesInRange[minIndex].transform;
        targetIndicator.transform.position = EnemiesInRange[minIndex].transform.position;
        currentTarget = CheckEnemies[minIndex];
    }
    
    private void findLeft(GameObject ignoreObj = null)
    {
        if (EnemiesInRange.Count <= 1) return;
        CheckEnemies = EnemiesInRange;
        if (ignoreObj != null)
            CheckEnemies.Remove(ignoreObj);
        CamPos = CamTrans.gameObject.GetComponent<Camera>().WorldToViewportPoint(EnemiesInRange[0].transform.position);
        minIndex = 0;
        minDistance = Mathf.Sqrt(Mathf.Pow((CamPos.x-.5f)-CheckEnemies[0].transform.position.x, 2)
                                 + Mathf.Pow((CamPos.y-.5f)-CheckEnemies[0].transform.position.y, 2));
        for (int i = 1; i < EnemiesInRange.Count; i++)
        {
            CamPos = CamTrans.gameObject.GetComponent<Camera>().WorldToViewportPoint(EnemiesInRange[i].transform.position);
            distance = Mathf.Sqrt(Mathf.Pow((CamPos.x-.5f)-CheckEnemies[0].transform.position.x, 2)
                                  + Mathf.Pow((CamPos.y-.5f)-CheckEnemies[0].transform.position.y, 2));
            if ((distance < minDistance) && (((CamPos.x-.5f) - CheckEnemies[i].transform.position.x) > 0))
            {
                minDistance = distance;
                minIndex = i;
            }
        }
        targetIndicator.transform.parent = EnemiesInRange[minIndex].transform;
        targetIndicator.transform.position = EnemiesInRange[minIndex].transform.position;
        currentTarget = CheckEnemies[minIndex];
    }

    private IEnumerator TargetFunction()
    {
        while (running)
        {
            yield return new WaitUntil(()=>Input.GetButtonDown("Target"));
            yield return new WaitForFixedUpdate();
            findClosest();
            currTargetObj.SetTransform(currentTarget);
            targeting = true;
            while (targeting)
            {
                lookatvector = currentTarget.transform.position - transform.position;
                lookatvector.y = 0;
                quat = Quaternion.LookRotation(lookatvector);
                transform.rotation = quat;
                if (!EnemiesInRange.Contains(currentTarget))
                {
                    findClosest();
                    currTargetObj.SetTransform(currentTarget);
                }
                else if (Input.GetButtonDown("Target"))
                {
                    targetIndicator.SetActive(false);
                    targetIndicator.transform.parent = null;
                    targeting = false;
                }
                else if (Input.GetButtonDown("TargetChange"))
                {
                    if (Input.GetAxisRaw("TargetChange") > 0)
                    {
                        findRight(currentTarget);
                        currTargetObj.SetTransform(currentTarget);
                    }
                    else
                    {
                        findLeft(currentTarget);
                        currTargetObj.SetTransform(currentTarget);
                    }
                }
                yield return new WaitForFixedUpdate();
            }

        }
    }
}
