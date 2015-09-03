using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {
    private Animator anim;
    public Transform boat;

    private Vector3 startVec;
    private Vector3 endVec;
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
    private bool animOver;
    // Update is called once per frame
    void Update () {
        
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animOver)
        {
            anim.enabled = false;
            animOver = true;

            startTime = Time.time;
            startVec = transform.position;
            endVec = new Vector3( transform.position.x, transform.position.y, -0.7f);
            transform.position = endVec;
        }
    }
}
