using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;

public class ragdollOnOff : MonoBehaviour
{
    public Rigidbody[] rigidbodys;
    public bool isKinem;

        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) isKinem = !isKinem;
        if (isKinem)
        {
            for (int i = 0; i < rigidbodys.Length; i++)
            {
                rigidbodys[i].isKinematic = true;
            }
        }
        else
        {
            GetComponent<FullBodyBipedIK>().enabled = false;
            GetComponent<IK_controller>().enabled = false;
            for (int i = 0; i < rigidbodys.Length; i++)
            {
                rigidbodys[i].isKinematic = false;
                
            }
            
        }
    }
}
