using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.GlobalIllumination;

public class IK_controller : MonoBehaviour
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
    public Rigidbody rbSpine;
    


    public bool isLeft;
    public bool isStand;

    private Transform targetLeg;
    private Transform targetHand;
    private Vector3 swipeVector = new Vector3(0f, 0f, 0f);
    [SerializeField] private float fallDistance = 1.0f;
    public float crowlSpeed;
    public float f = 1f;
    public float dir = 0.01f;

    private Vector3 hitpointHand;
    private Vector3 hitpointLeg;

    // private Vector3 startMousePosition;
    // private Vector3 finMousePosition;
    private bool isSwiping = false;

    private float xRes = 640f;
    private float yRes = 1136f;

    private  Vector3 velocity = Vector3.zero;
    [SerializeField] private float stepUp = 0.3f;

    



    // Start is called before the first frame update
    void Start()
    {
        leftLeg = targetLeftLeg.transform;
        rightLeg = targetRightLeg.transform;
        leftHand = targetLeftHand.transform;
        rightHand = targetRightHand.transform;

        TouchControls.OnTouchReleased += OnTouchReleased;
    }

    void Update()
    {
        if (isStand)
        {
            float rightX = rightHand.position.x - bodyTarget.transform.position.x;
            var rightVec = new Vector3(bodyTarget.transform.position.x + rightX, rightHand.position.y, leftLeg.position.z + 0.2f);
            rightHand.gameObject.GetComponent<doTweenController>().DoMove(rightVec);
            Debug.Log("rightvec" + rightVec);
            Debug.Log("leftLeg.position.z" + leftLeg.position.z);
            
            float leftX = leftHand.position.x - bodyTarget.transform.position.x;
            var leftVec = new Vector3(bodyTarget.transform.position.x + leftX, leftHand.position.y, rightLeg.position.z + 0.2f);
            leftHand.gameObject.GetComponent<doTweenController>().DoMove(leftVec);
            Debug.Log("leftvec" + leftVec);
            Debug.Log("rightLegPositionZ" + rightLeg.position.z);

        }
        
        
        if (Input.GetKey(KeyCode.T)) rbSpine.AddForce(swipeVector);
        // transform.position = bodyTarget.transform.position;


        // if (Vector3.Magnitude(leftLeg.position - leftHand.position) > fallDistance 
        //     || Vector3.Magnitude(rightLeg.position - rightHand.position) > fallDistance
        //     || Vector3.Magnitude(leftLeg.position - rightLeg.position) > fallDistance
        //     || Vector3.Magnitude(leftHand.position - rightHand.position)> fallDistance)
        // {
        //     Fall();
        // }
        //
        // if (leftLeg.position.x > rightleg.position.x
        //     || leftHand.position.x > rightHand.position.x
        //     || leftLeg.position.z > leftHand.position.z
        //     || rightleg.position.z > rightHand.position.z)
        // {
        //     Fall();
        // }
        
        // if (!isSwiping)
        // {
        //     if (isLeft)
        //     {
        //         StayAndCrowl(swipeVector, leftLeg, rightHand, crowlSpeed);
        //     }
        //     else
        //     {
        //         StayAndCrowl(swipeVector, rightLeg, leftHand, crowlSpeed);
        //     }
        // }

    }

   
    private void Fall()
    {
        GetComponent<ragdollOnOff>().isKinem = false;
        
        
    }


    private void OnTouchReleased(Vector2 start, Vector2 fin)
    {
        isLeft = !isLeft;
        isSwiping = true;
        var sw = fin - start;
        swipeVector = new Vector3(sw.x / xRes, 0f, sw.y / yRes);
        Debug.Log("swipeVector = " + swipeVector);


        if (!isStand)
        {
            if (isLeft)
            {

                if (Math.Abs(swipeVector.z) > Math.Abs(swipeVector.x))
                {
                    MakeStep(swipeVector, leftLeg);
                    MakeStep(swipeVector, rightHand);
                    
                }
                else if (Math.Abs(swipeVector.z) < Math.Abs(swipeVector.x))
                {
                    MakeStep(swipeVector, leftLeg);
                    MakeStep(swipeVector, leftHand);
                }

            }

            if (!isLeft)
            {

                if (Math.Abs(swipeVector.z) > Math.Abs(swipeVector.x))
                {
                    MakeStep(swipeVector, rightLeg);
                    MakeStep(swipeVector, leftHand);
                }

                else if (Math.Abs(swipeVector.z) < Math.Abs(swipeVector.x))
                {
                    MakeStep(swipeVector, rightLeg);
                    MakeStep(swipeVector, rightHand);
                }


            }
        }
        if (isStand)
        {
            if (isLeft)
            {
                MakeStep(swipeVector, leftLeg);
                
                
            }

            if (!isLeft)
            {
                MakeStep(swipeVector, rightLeg);
                
            }
        }

        isSwiping = false;
    }









    private void StayAndCrowl(Vector3 direction, Transform leg, Transform hand, float translateValue)
    {
        if (isStand)
        {
            leg.Translate(direction * translateValue);
        }
        else
        {
            leg.Translate(-direction * translateValue);
            hand.Translate(direction * translateValue);
        }
    }

    


    private void MakeStep(Vector3 direction, Transform target)
    {
        direction *= dir;


        RaycastHit[] hit;

        hit = Physics.RaycastAll(target.transform.position + target.up * 5f + direction, -target.up, 10f);

        
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.tag == "floor")
            {
                Debug.DrawLine(hit[i].point,target.position,Color.yellow,10f);
                target.up = hit[i].normal;
                
                target.gameObject.GetComponent<doTweenController>().DoMove((hit[i].point - target.position)/2f + Vector3.up * Vector3.Magnitude(direction)/2f);
                
                if (target == leftLeg)
                {
                    rightLeg.DOMove(rightLeg.position - direction*Vector3.Magnitude(direction)/2f, 0.1f);
                } else leftLeg.DOMove(leftLeg.position - direction*Vector3.Magnitude(direction)/2f, 0.1f).SetEase(Ease.InOutElastic);

                
                target.gameObject.GetComponent<doTweenController>().DoMove(hit[i].point);
                
            }
        }
    }

    void OnDrawGizmos()
    {
        // Gizmos.color = Color.red;
        //
        // Gizmos.DrawWireSphere(hitpointHand, 0.1f);
        // Gizmos.color = Color.green;
        // Gizmos.DrawWireSphere(hitpointLeg, 0.1f);

    }

    

}




