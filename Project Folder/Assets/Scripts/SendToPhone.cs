using UnityEngine;
using System.Collections;

public class SendToPhone : MonoBehaviour
{

    private bool listSent;
    private NetworkView nView;

    // Use this for initialization
    void Start()
    {
        nView = GetComponent<NetworkView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FishManager.FishM.caughtFish.Count == 3 && !listSent)
        {
            listSent = true;
            SendCaughtFish();
            Debug.Log("Sending caughtFish List");
        }
    }

    private void SendCaughtFish()
    {
        string fishString = "";

        foreach (Fish fish in FishManager.FishM.caughtFish)
        {
            fishString += fish.getName() + "," + fish.getWeight() + "\n";
        }

        nView.RPC("SendList", RPCMode.All, fishString);
    }

    [RPC]
    private void SendList(string f)
    {
        Debug.Log("Sending caught fish to phone");
    }
}
