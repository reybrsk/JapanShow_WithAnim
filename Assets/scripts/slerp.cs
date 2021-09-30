
using UnityEngine;
using System.Collections;

public class slerp : MonoBehaviour
{
    public Transform sunrise;
    public Transform sunset;

   
    public float journeyTime = 1.0f;

    
    private float startTime;

    void Start()
    {
        
        startTime = Time.time;
    }

    void Update()
    {
     
        Vector3 center = (sunrise.position + sunset.position) * 0.5F;

        // move the center a bit downwards to make the arc vertical
        center -= new Vector3(0, 1, 0);

        // Interpolate over the arc relative to center
        Vector3 riseRelCenter = sunrise.position - center;
        Vector3 setRelCenter = sunset.position - center;

        // The fraction of the animation that has happened so far is
        // equal to the elapsed time divided by the desired time for
        // the total journey.
        float fracComplete = (Time.time - startTime) / journeyTime;

        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;
    }
}
