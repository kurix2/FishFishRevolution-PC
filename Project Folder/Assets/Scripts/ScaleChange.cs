using UnityEngine;
using System.Collections;

public class ScaleChange : MonoBehaviour {

    public PingPongMove _pingPong;
    Vector3 defaultScale;
 

    
 
    public bool CHANGE_X = false;
    public bool CHANGE_Y = true;
    public bool CHANGE_Z = false;

    // Use this for initialization
    void Start () {
        _pingPong = this.gameObject.GetComponent<PingPongMove>();
        defaultScale = transform.localScale; 
    }
 
    // Update is called once per frame
    void Update () {
            this.changeSize();
    }
 
    void changeSize() {
        Vector3 newSize = transform.localScale;
 
        if (CHANGE_X) {
            newSize.x = defaultScale.x * _pingPong.value / _pingPong.maxValue;
        }
        if (CHANGE_Y) {
            newSize.y = defaultScale.y * _pingPong.value / _pingPong.maxValue;
        }
 
        if (CHANGE_Z) {
            newSize.z = defaultScale.z * _pingPong.value / _pingPong.maxValue;
        }

        transform.localScale = newSize;
    }
}
