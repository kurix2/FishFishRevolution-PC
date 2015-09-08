using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TackleBoxUI : MonoBehaviour {

    public static TackleBoxUI tackleBox;

    private int fishCount;
    private int fishMax = 3;

    public GameObject container;
    public Text uiText;

    private Vector3 startPos;
    private Vector3 offscreenPos;

    private bool showingUI;


    void Awake()
    {
        if (tackleBox == null)
            tackleBox = this;
    }


	void Start ()
    {
        startPos = container.transform.position;
        offscreenPos = new Vector3(container.transform.position.x - 150, container.transform.position.y, container.transform.position.z);
        container.transform.position = offscreenPos;

        fishCount = 0;

        //show();
    }

    void Update()
    {
        if (CameraAnimationScript.CamAnimSript.scene == "Game" && !showingUI)
            Show();
        else if (CameraAnimationScript.CamAnimSript.scene != "Game" && showingUI)
            Hide();
            
    }

    public void Show()
    {
        showingUI = true;
        iTween.MoveTo(container, iTween.Hash("position", startPos, "time", 0.5f, "easeType", iTween.EaseType.easeOutSine));
    }

    public void Hide()
    {
        showingUI = false;
        iTween.MoveTo(container, iTween.Hash("position", offscreenPos, "time", 0.5f, "easeType", iTween.EaseType.easeInSine));
    }


     
    public void addFish()
    {
        fishCount++;
        uiText.text = fishCount + "/" + fishMax;
        iTween.ScaleTo(container, iTween.Hash("scale", new Vector3(1.3f, 1.3f, 1f), "time", 0.2f, "easeType", iTween.EaseType.easeOutSine));
        iTween.ScaleTo(container, iTween.Hash("scale", new Vector3(1f, 1f, 1f), "time", 0.2f, "easeType", iTween.EaseType.easeInSine, "delay", 0.3f));
        //uiText.fontSize = 1;
    }
}
