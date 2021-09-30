using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.WSA;
using Lightmapping = UnityEditor.Lightmapping;
using Random = UnityEngine.Random;

public class IK_controller1 : MonoBehaviour
{

    public GameObject targetLeftLeg;
    public GameObject targetRightLeg;
    public GameObject targetLeftHand;
    public GameObject targetRightHand;
    public GameObject bodyTarget;
    private Transform leftLeg;
    private Transform rightLeg;
    private Transform leftHand;
    private Transform rightHand;

    private float coef;
    private bool isSwiping;
    private bool isLeft = true;
    private bool isStand = true;
    private Vector3 swipeVector;
    private Vector3 slideVector;

    [Range(0.1f, 1.0f), SerializeField] public float doMoveDuration;

    
   
    



    // Start is called before the first frame update
    void Start()
    {
        leftLeg = targetLeftLeg.transform;
        rightLeg = targetRightLeg.transform;
        leftHand = targetLeftHand.transform;
        rightHand = targetRightHand.transform;

        TouchControls.OnTouchReleased += OnTouchReleased;
        TouchControls.OnSwipe += onSwipe;

    }

    private void onSwipe(Vector2 pos)
    {
        isSwiping = true;
        
    }


    private void OnTouchReleased(Vector2 start, Vector2 fin)
    {
        isSwiping = false;
        isLeft = !isLeft;
        coef += 0.2f;

        Vector2 swipeVector2 = fin - start;
        swipeVector2 = swipeVector2.normalized;
        swipeVector = new Vector3(swipeVector2.x, 0f, swipeVector2.y);
        slideVector += swipeVector * coef;
        if (isLeft)
        {
            MakeStep(swipeVector, leftLeg); 
        }
        else
        {
            MakeStep(swipeVector,rightLeg);
        }

        


    }

    
    
    void Update()
    {
        if (!isSwiping)
        {
            if (coef >= 0.0f)
            {
                coef -= 0.2f * Time.deltaTime;
                slideVector = swipeVector * coef;
            }
            else
            {
                coef = 0.0f;
                slideVector = swipeVector * coef;
            }
           
        }

        GetComponent<CharacterController>().Move(slideVector*Time.deltaTime);

        if (Input.GetKey(KeyCode.U))
        {
            handFly(leftHand);
            handFly(rightHand);
        }
    
       
    }
        
        
        
    
    

    private void MakeStep(Vector3 direction, Transform target)
    {
        


        RaycastHit[] hit;

        hit = Physics.RaycastAll(target.transform.position + target.up * 5f + direction, -target.up, 10f);

        
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.tag == "floor")
            {
                Debug.DrawLine(hit[i].point,target.position,Color.yellow,10f);
                target.up = hit[i].normal;

                if (target == leftLeg)
                {
                    rightLeg.DOMove(rightLeg.position - direction*Vector3.Magnitude(direction)/2f, 0.1f).onComplete();
                } else leftLeg.DOMove(leftLeg.position - direction*Vector3.Magnitude(direction)/2f, 0.1f);


                target.transform.DOMove(hit[i].point, doMoveDuration);

            }
        }
    }


    private void handFly(Transform target)

    {
        /*
         ду мув, подождать пока выполнится, потом снова ду-мув, подождать и еще раз и на место поставить.
         место определить рэйкастами?? или подумать как
         анимацию на руки когда простой
         
          
         */

        
    }

    
}




