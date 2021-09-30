using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class doTweenController : MonoBehaviour
{
    
    
    [Range(0.0f, 1.0f), SerializeField] private float moveDuration = 1.0f;
    [SerializeField] private Ease moveEase = Ease.Linear;
    [SerializeField] private DoTweenType doTweenType = DoTweenType.OneWay;
    private enum DoTweenType
    {
        OneWay,
        TwoWays
    }
    
    
    
    
    // Start is called before the first frame update
    public void DoMove(Vector3 targetLocation)
    
    {
        
        
            if (doTweenType == DoTweenType.OneWay)
            {
                transform.DOMove(targetLocation, moveDuration).SetEase(moveEase);
            }
        
    }

   
}
