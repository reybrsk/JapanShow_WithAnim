using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTheFloor : MonoBehaviour
{
    private void Update()
    {
        Ray ray = new Ray(transform.position + Vector3.up*3f, Vector3.down*60f);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.tag == "floor")
            {
                transform.position = hits[i].point;
            }
        }
        
            
    }
}
