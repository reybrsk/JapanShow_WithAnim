using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class amplitude : MonoBehaviour
{
    public float ampl;
    private Transform target;
    private bool isUp;
    private float newX = 0f;
    private float zeroX;
    public float speed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        target = gameObject.transform;
        var position = target.position;
        zeroX = position.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (newX >= zeroX + ampl || newX <= zeroX - ampl) isUp = !isUp;

        if (isUp)
        {
            newX += speed;
        }
        else newX += -speed;





        gameObject.transform.position = new Vector3(newX, target.position.y, target.position.z);
    }
}
