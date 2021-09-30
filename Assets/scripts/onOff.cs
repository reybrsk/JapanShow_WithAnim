using System.Collections;
using System.Collections.Generic;
using DitzelGames.FastIK;
using UnityEngine;

public class onOff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        
        gameObject.GetComponent<FastIKFabric>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
