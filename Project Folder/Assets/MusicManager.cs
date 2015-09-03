using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    private RodStatus status;
    public GameObject rod;
 
    public AudioSource audioClip1;
    public AudioSource audioClip2;
    public AudioSource audioClip3;

    void Start()
    {
        rod = GameObject.Find("NetworkView_TestObject(Clone)");
        status = RodStatus.rodstatus;
    }

    void Update()
    {
        Debug.Log(RodStatus.rodstatus);
        if(rod != null){
            }
                 
            if(status.getStatus() == "reeling in"){
                if (audioClip1.isPlaying || audioClip3.isPlaying)
                {
                    audioClip1.Stop();
                    audioClip3.Stop();
                    audioClip2.Play();
                }
            }

             else if(status.getStatus() == "reeling up"){
                 if (audioClip1.isPlaying || audioClip2.isPlaying)
                 {
                     audioClip1.Stop();
                     audioClip2.Stop();
                     audioClip3.Play();
                 }
            }

            else{
                 if (audioClip2.isPlaying || audioClip3.isPlaying)
                {
                    audioClip2.Stop();
                    audioClip3.Stop();
                    audioClip1.Play();
                }
             }
        }
    }

