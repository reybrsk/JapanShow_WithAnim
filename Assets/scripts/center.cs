using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class center : MonoBehaviour
{

    public GameObject leftFoot;
    public GameObject rightFoot;
    public GameObject leftHand;
    public GameObject rightHand;



    private Vector3 targetPosition;
    private float _y;
    public float distance;
    public float backDistance;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = 
            (leftFoot.transform.position
             + rightFoot.transform.position
             + leftHand.transform.position
             + rightHand.transform.position) / 4;
        
        
        
        _y = targetPosition.y + distance;
        
        targetPosition = new Vector3(targetPosition.x, _y, targetPosition.z - backDistance);
        
        transform.position = targetPosition;
        
    }
}
