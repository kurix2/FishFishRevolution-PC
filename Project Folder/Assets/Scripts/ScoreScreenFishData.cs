using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScreenFishData : MonoBehaviour {

    private FishManager fManager;

    public int fishNumber;

    public Image fishIcon;
    public Text fishNameTxt;
    public Text fishWeightTxt;
    public Image gradeIcon;

    void Awake()
    {
        fManager = FishManager.FishM;
    }

    void Start()
    {
        fManager = FishManager.FishM;

       // Setup();

       

        //Setup();
    }


    public void Setup()
    {
        fManager = FishManager.FishM;
        Fish fish = fManager.caughtFish[fishNumber];

        string fishicon = fish.getIcon();
        string loadthis = "Prefabs/Fish/Icons/" + fishicon;
        string wtf = loadthis.Substring(0, loadthis.Length - 1);

        Texture2D tex = Resources.Load<Texture2D>(wtf);
        if (tex != null)
            fishIcon.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        else
            Debug.Log("Error loading fish icon: " + fish.getIcon());

        fishNameTxt.text = fish.getName();
        fishWeightTxt.text = fish.getWeight() + "kg";
    }


    void Update()
    {
       // Debug.Log("fishcount" + fManager.caughtFish.Count);
       // if (fManager.caughtFish.Count >= fishNumber)
        //    Setup();
    }

    void OnGUI()
    {

    }

}
