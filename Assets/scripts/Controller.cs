using System.Collections;
using System.Collections.Generic;
using DitzelGames.FastIK;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Experimental.Audio;

public class Controller : MonoBehaviour
{

    private CharacterController _controller;
    public Transform leftLeg;
    public Transform rightLeg;
    public float distance;
    public Transform neck;
    public bool isOnTheFloor = false;
    public float forvDist;
    public float neckDistance;
    // public GameObject leftHandGameObject;
    // public GameObject rightHandGameObject;
    // public GameObject neckGameObject;

    public Transform leftHand;
    public Transform rightHand;


    public float neckForvard;


    // Start is called before the first frame update
    void Start()
    {
        
        _controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        var vec = (leftLeg.position + rightLeg.position) / 2;

        _controller.transform.position = new Vector3(vec.x, vec.y + distance, vec.z + forvDist);
        

        if (isOnTheFloor)
        {
            
            // leftHandGameObject.GetComponent<FastIKFabric>().enabled = true;
            // rightHandGameObject.GetComponent<FastIKFabric>().enabled = true;
            // neckGameObject.GetComponent<FastIKFabric>().enabled = true;
            
            
            Ray ray = new Ray(neck.position, Vector3.forward);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 10f);




            Transform newPos = neck;
            newPos.position = (leftHand.position + rightHand.position) / 2f;


            newPos.up = hit.normal;
            Debug.DrawRay(newPos.position, newPos.up, Color.cyan);
            newPos.Translate(Vector3.up * neckDistance + Vector3.forward * neckForvard, Space.Self);

            neck = newPos;
            neck.Rotate(0f, -90f, -90f);



        }
        else
        {
            // leftHandGameObject.GetComponent<FastIKFabric>().enabled = false;
            // rightHandGameObject.GetComponent<FastIKFabric>().enabled = false;
            // neckGameObject.GetComponent<FastIKFabric>().enabled = false;
            return;
        }
    }
}

