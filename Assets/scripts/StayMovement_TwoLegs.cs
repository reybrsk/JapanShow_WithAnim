using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StayMovement_TwoLegs : MonoBehaviour
{
    public Transform leftLeg;
    public Transform rightLeg;
    private Vector3 dir;
    private bool isLeft;
    private int xRes = 640;
    private int yRes = 1320;
    private CharacterController contr;
    


    private void Start()
    {
        TouchControls.OnTouchReleased += OnTouchReleased;
        contr = GetComponent<CharacterController>();
    }

    private void OnTouchReleased(Vector2 start, Vector2 fin)
    {
        dir = fin - start;
        dir = new Vector3(dir.x/xRes, 0f, dir.y/yRes);
        isLeft = !isLeft;
        if (isLeft) LegMove(leftLeg,rightLeg);
        else LegMove(rightLeg,leftLeg);
        gameObject.GetComponent<ImpulseToController>().MoveController(dir);
        
    }

    private void LegMove(Transform firstLeg, Transform secondLeg)
    {
        firstLeg.DOMove(firstLeg.position + dir*2f, 1f).SetEase(Ease.InOutCirc).OnComplete(() => BackLeg(firstLeg,secondLeg));
        secondLeg.DOMove(secondLeg.position, 1f).SetEase(Ease.InOutCirc);
    }

    private void BackLeg(Transform leg,Transform sleg)
    {
        var _z = contr.transform.position.z;
        var _y = leg.transform.position.y;// прописать Y по рэйкасту
        var _x = leg.transform.position.x;
        var _sz = contr.transform.position.z;
        var _sy = sleg.transform.position.y;// прописать Y по рэйкасту
        var _sx = sleg.transform.position.x;
        Vector3 vec = new Vector3(_x, _y, _z);
        Vector3 svec = new Vector3(_sx, _sy, _sz);
        leg.DOMove(vec, 0.1f).SetEase(Ease.Flash)
            .OnComplete(() => sleg.DOMove(svec,0.1f));
    }

    private void Update()
    {
        
    }
    
}
