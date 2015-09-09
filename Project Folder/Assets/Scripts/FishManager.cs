using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishManager : MonoBehaviour
{
    public static FishManager FishM;

    public TextAsset FishList;
    public List<Fish> fish = new List<Fish>();
    public List<Fish> caughtFish = new List<Fish>();

    // Sprite for caught fish counter
    public Texture2D fishCountSprite;
    public Texture2D splashSprite;

    private float fishOffsetX = 175f;
    private float fishOffsetY = 135f;

   public GUISkin guiStyle;

    public NetworkView nView;
    Fish hookedFish;

    void Awake()
    {
        if (FishM == null)
        {
            FishM = this;
        }
    }

    void Start()
    {
     
        readFishList();

    }


    void Update()
    {
        if (Input.GetKeyDown("space"))
            hookFish();

        if (caughtFish.Count == 3 && RodStatus.rodstatus.getStatus() != "gameover")
            RodStatus.rodstatus.setStatus("gameover");
    }

    public void OnGUI()
    {

        GUI.skin = guiStyle;
        for (int i = 0; i < caughtFish.Count; i++)
        {
            //GUI.Label(new Rect(10 + (fishCountSprite.width * i), 10, fishCountSprite.width, fishCountSprite.height), fishCountSprite);
        }
        if (RodStatus.rodstatus != null) {
        //if (caughtFish.Count == 3 || RodStatus.rodstatus.getStatus() == "gameover")
        if (CameraAnimationScript.CamAnimSript.scene == "GameOver")
        {
          /*  GUI.Label(new Rect(Screen.width / 2 - splashSprite.width / 2, Screen.height / 2 - splashSprite.height / 2, splashSprite.width, splashSprite.height), splashSprite);

            for (int i = 0; i < caughtFish.Count; i++)
            {
                //Texture2D fishIcon = Resources.Load("Prefabs/Fish/Icons/RandomFishIcon") as Texture2D;
                //string fishIconToLoad = "Prefabs/Fish/Icons/" + caughtFish[i].getIcon();
                // BigMouthBassIcon


                string test1 = "Prefabs/Fish/Icons/" + caughtFish[i].getIcon();
             

              //  Debug.Log("1 " + test1);
               // Debug.Log("2 " + test1);


                //string actualPathtoLoad = "Prefabs/Fish/Icons/BigMouthBassIcon";
                // Debug.Log("ICON " + caughtFish[i].getIcon());
                Texture2D fishIcon = fishCountSprite; //Resources.Load(actualPathtoLoad) as Texture2D;



                //Texture2D fishIcon = Resources.Load("Prefabs/Fish/Icons/" + caughtFish[i].getIcon()) as Texture2D;

                GUI.Label(new Rect(Screen.width / 2 - fishOffsetX, Screen.height / 2 - fishOffsetY + ((fishCountSprite.height + 25) * i), fishCountSprite.width, fishCountSprite.height), fishIcon);
                GUI.Label(new Rect(Screen.width / 2 - fishOffsetX + fishCountSprite.width + 10, Screen.height / 2 - fishOffsetY + ((fishCountSprite.height + 25) * i) + 0, 400, 60), caughtFish[i].getName());
                GUI.Label(new Rect(Screen.width / 2 - fishOffsetX + fishCountSprite.width + 10, Screen.height / 2 - fishOffsetY + ((fishCountSprite.height + 25) * i) + 35, 400, 60), caughtFish[i].getWeight() + "kg");

            }*/

        }
        }


    }

    // Puts the fish listed in the CSV into a List
    private void readFishList()
    {
        string[] lines = FishList.text.Split("\n"[0]);

        // Loop starts at 1, 0 is only for headers
        for (int i = 1; i < lines.Length; i++)
        {

            string[] row = lines[i].Split(","[0]);

            int id = int.Parse(row[0]);
            string name = row[1];
            string prefab = row[2];
            int rarity = int.Parse(row[3]);
            float minWeight = float.Parse(row[4]);
            float maxWeight = float.Parse(row[5]);
            int movePattern = int.Parse(row[6]);
            float speed = float.Parse(row[7]);
            float endurance = float.Parse(row[8]);
            string icon = row[9];

            fish.Add(new Fish(id, name, prefab, rarity, minWeight, maxWeight, movePattern, speed, endurance, icon));
        }
    }

    public Fish hookFish()
    {

        Random.seed = System.Environment.TickCount;

        int randomFish = Random.Range(0, fish.Count);

        hookedFish = fish[randomFish];
        hookedFish.randomize(); // not needed anymore?

        Debug.Log("Min: " + hookedFish.getMinW());
        Debug.Log("Max: " + hookedFish.getMaxW());

        float randomWeight = Mathf.Round(Random.Range(hookedFish.getMinW(), hookedFish.getMaxW()) * 100f) / 100f;
        hookedFish.setWeight(randomWeight);

       // Debug.Log("Random Weight: " + randomWeight);
       // Debug.Log("Fish Weight: " + hookedFish.getWeight());


        // Remove me, for debugging only
        keepFish(hookedFish);

        return hookedFish;
    }



    public void keepFish(Fish caught)
    {



        caughtFish.Add(caught);
        TackleBoxUI.tackleBox.addFish();
        Debug.Log("You caught a: " + caughtFish[caughtFish.Count - 1].getName() + " Weight: " + caughtFish[caughtFish.Count - 1].getWeight());
       

       // return fishString;
    }


}
