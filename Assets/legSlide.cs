using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RootMotion.FinalIK;
using UnityEngine;

public class legSlide : MonoBehaviour
{
    
    [SerializeField] private FullBodyBipedIK fullBodyBipedIK;
    private Transform leftLeg;
    private Transform rightLeg;
    public bool isStay;
    private bool isSlide;
   
    
    void Start()
    {
        leftLeg = fullBodyBipedIK.solver.leftFootEffector.target;
        rightLeg = fullBodyBipedIK.solver.rightFootEffector.target;
    }

   
    void Update()
    {
       if (isStay && !isSlide)StartCoroutine(Slide());
    }

    

    private void IsSlideOn(bool b)
    {
        isSlide = b;
    }

    private IEnumerator Slide()
    {
        IsSlideOn(true);
        Vector3 lLeg = leftLeg.position;
        Vector3 rLeg = rightLeg.position;
        leftLeg.DOMove(leftLeg.position + (Vector3.forward * 0.1f + -Vector3.right * 0.1f), 2.0f).SetEase(Ease.InCirc)
            .OnComplete(() => leftLeg.DOMove(lLeg, 0.2f).OnComplete(() => IsSlideOn(false)));
        yield return new WaitForSeconds(0.7f);
        rightLeg.DOMove(rightLeg.position + (Vector3.forward * 0.1f + Vector3.right * 0.1f), 1.0f).SetEase(Ease.InCirc)
            .OnComplete(() => rightLeg.DOMove(rLeg, 0.2f));
    }
    
}
