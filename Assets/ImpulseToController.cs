using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ImpulseToController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 slideDirection = Vector3.zero;
    private float impulceWeight = 0.0f;
    [Range(0.0f, 1.0f), SerializeField] private float addToImpulce = 0.5f;
    
    
    
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        
    }

    public void MoveController(Vector3 dir)
    {
        slideDirection += dir;
        impulceWeight += addToImpulce;
    }

    private void Update()
    {
        controller.Move(slideDirection * impulceWeight);
        if (impulceWeight > 0f)
        {
            impulceWeight -= 0.01f;
        }
        else impulceWeight = 0.0f;

    }
}
