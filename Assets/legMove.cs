using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RootMotion.FinalIK;
using UnityEngine;
public class legMove : MonoBehaviour
{


    [Range(0.0f,1.0f), SerializeField] public float duration = 1.0f;
    private Transform firstLegStay;
    private Transform secondLegStay;
    public GameObject firstLeg;
    public GameObject secondLeg;

    

    public void Move(GameObject first, GameObject second, Vector3 vec)
    {
        
        StartCoroutine(MoveMove(firstLeg.transform, secondLeg.transform, vec, duration));
    }
    
    private IEnumerator MoveMove(Transform first, Transform second, Vector3 vec,float duration)
    {
        
        secondLeg.transform.DOMove(second.position - vec, duration)
            .SetEase(Ease.InCirc);
        
        yield return new WaitForSeconds(duration/2.0f);
        firstLeg.transform.DOMove(first.position + vec/2.0f, duration/2.0f).SetEase(Ease.InCirc)
            .OnComplete(() => LegsToStay());
        
    }

    void LegsToStay()
    {
        firstLegStay = firstLeg.transform;
        secondLegStay = secondLegStay.transform;
        
        
        secondLeg.transform.DOMove(secondLegStay.position, duration / 2)
            .OnComplete(() => firstLeg.transform.DOMove(firstLegStay.position, duration / 2));
        Debug.Log("nenenen");
        
        
    }
}
