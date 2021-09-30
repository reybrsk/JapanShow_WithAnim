using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triiiigggeeeerrr : MonoBehaviour
{

    public GameObject player;

    public bool onOffBool;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter()
    {
      player.GetComponent<Controller>().isOnTheFloor = onOffBool;


    }
}
