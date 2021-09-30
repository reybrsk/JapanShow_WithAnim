using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool isLeft;
    private Vector3 swipeVector;
    private Vector3 direction;
    private Vector3 slideVector;
    [Range(0.0f,0.5f), SerializeField] private float impulse;

    
    
    private bool isSwiping;

    private int xRes = 640;
    private int yRes = 1320;
    private CharacterController controller;
    private float coef;
    public GameObject leftLeg;
    public GameObject rightLeg;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        controller = gameObject.GetComponent<CharacterController>();
        TouchControls.OnTouchReleased += OnTouchReleased; 
        TouchControls.OnSwipe += onSwipe;
    }

    private void OnTouchReleased(Vector2 start, Vector2 fin)
    {
        isLeft = !isLeft;
        isSwiping = false;
        var sw = fin - start;
        if (sw != Vector2.zero)
        {
            swipeVector = new Vector3(sw.x / xRes, 0f, sw.y / yRes);
            

            direction += swipeVector.normalized;

            AddImpulce();
            if (isLeft)
            {
                gameObject.GetComponent<legMove>().Move(leftLeg, rightLeg, swipeVector);
            }
            else gameObject.GetComponent<legMove>().Move(rightLeg, leftLeg, swipeVector);
        }









    }
    private void onSwipe(Vector2 pos)
    {
        isSwiping = true;
        
    }
    private void AddImpulce()
    {
        coef += impulse;
        
    }

    // Update is called once per frame
    void Update()
    {
        slideVector = direction * coef;
        
        if (coef >= 0f)
        {
            coef -= 0.1f*Time.deltaTime;
        }
        else coef = 0f;

        var dur = gameObject.GetComponent<legMove>().duration;
        
        controller.transform.DOMove(controller.transform.position + slideVector*0.5f, dur).SetEase(Ease.InCirc);
       
        
        
            
            
    }
    
}
