using UnityEngine;
using System.Collections;

public class Bobber : MonoBehaviour {

    //This comment shows that this script was successfully changed and can be seen after being pushed to github.

    private bool hitOcean;
    public bool reelMeUp;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.y < -1 && !hitOcean)
        {
            Debug.Log("-1");
            rb.useGravity = false;
            rb.isKinematic = true;
            hitOcean = true;
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (hitOcean) { 
            if (other.tag == "BoatRadius")
            {
                reelMeUp = true;
            }
        }
    }

    public bool readyToRealUp()
    {
        return reelMeUp;
    }

    public void reset()
    {
        hitOcean = false;
        reelMeUp = false;
    }

    public bool hasLanded()
    {
        return hitOcean;
    }

}
