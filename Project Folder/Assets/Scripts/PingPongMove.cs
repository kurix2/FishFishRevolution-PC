using UnityEngine;
using System.Collections;

public class PingPongMove : MonoBehaviour {
   public float value = 0.0f;
   public float maxValue = 1f;
   public float changeSpeed = 1.0f;
 
    // Update is called once per frame
    void Update () {
        value = Mathf.PingPong(Time.time * changeSpeed, maxValue);	
	}
}
