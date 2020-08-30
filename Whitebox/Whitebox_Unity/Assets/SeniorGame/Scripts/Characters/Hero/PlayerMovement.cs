using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
        public CharacterRotate rotate;

        public CharacterTranslate translate;

        public Targeting targetScript;

        private Coroutine rotateFunc, moveFunc, runFunc;

        private CharacterController _cc;

        private void Start()
        {
                _cc = GetComponent<CharacterController>();
                Init();
        }

        private void Init()
        {
                rotate.Init(this.transform, Camera.main.transform, targetScript);
                translate.Init(_cc, Camera.main.transform, targetScript);
                StartAll();
        }

        public void SwapMovement(CharacterRotate newRot, CharacterTranslate newTrans)
        {
                StopAll();
                rotate = newRot;
                translate = newTrans;
                Init();
        }

        public void StopAll()
        {
                StopMove();
                StopRotate();
        }

        public void StartAll()
        {
                StartMove();
                StartRotate();
        }

        public void StopMove()
        {
                translate.canMove = false;
                translate.canRun = false;
                if(moveFunc != null)
                        StopCoroutine(moveFunc);
                if(runFunc!= null)
                        StopCoroutine(runFunc);
        }

        public void StartMove()
        {
                translate.canMove = true;
                translate.canRun = true;
                moveFunc = StartCoroutine(translate.Move());
                runFunc = StartCoroutine(translate.Run());
        }

        public void StopRotate()
        {
                rotate.canRotate = false;
                if(rotateFunc != null)
                        StopCoroutine(rotateFunc);
        }

        public void StartRotate()
        {
                rotate.canRotate = true;
                rotateFunc = StartCoroutine(rotate.Rotate());
        }
}
