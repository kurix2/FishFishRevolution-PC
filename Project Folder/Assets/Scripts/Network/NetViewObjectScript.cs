using UnityEngine;
using System.Collections;

public class NetViewObjectScript : MonoBehaviour
{
	//Variables for rod rotation
    public float rotationSpeed = 17.0f;
    public float rotationBreak = 5.0f;
    NetworkView nView;

    public float boatOffsetY;

    public Transform reel;

//	//Variables for casting


    // Remove these bastards later
    public float rotX;
    public float rotY;
    public float rotZ;


    private GameObject boat;

    public Vector3 acceRotationVector = Vector3.zero;



    void Start()
    {
//		GameObject boat = GameObject.Find ("Fishing Boat");
//		
//		transform.SetParent(boat.transform);

        nView = GetComponent<NetworkView>();
        if (!nView.isMine)
        {
            enabled = false;
        }

        boat = GameObject.Find("Fishing Boat");
        reel.rotation = Quaternion.AngleAxis(0, Vector3.right);
    }

    void Update()
    {
        
    }

    [RPC]
    public void vibrate()
    {
       // Debug.Log("Vibrate");
       // Handheld.Vibrate();
    }

/*
    [RPC]
    public void reeling(float angle)
    {
       
    }
    */

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (stream.isWriting) {
            
		} else {

            if (boat)
                transform.position = new Vector3(transform.position.x, boat.transform.position.y + boatOffsetY, transform.position.z);

           /* Quaternion receivedRot = new Quaternion(0, 0, 0, 0);
            stream.Serialize(ref receivedRot);
            Quaternion rotationToUse = receivedRot;
            transform.rotation = rotationToUse;*/
           
			Vector3 receivedRot = Vector3.zero;
			stream.Serialize (ref receivedRot);
			acceRotationVector = receivedRot;
			  
			Quaternion rotation = Quaternion.LookRotation (receivedRot);
			float angle = Quaternion.Angle (transform.rotation, rotation);


			rotZ = rotation.eulerAngles.z;
			rotX = rotation.eulerAngles.x;
			rotY = rotation.eulerAngles.y;


			if (angle > rotationBreak) {
				transform.rotation = Quaternion.Lerp (transform.rotation, rotation, Time.deltaTime * rotationSpeed);
				// Works -> transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
                

        }
		}
    }

}
