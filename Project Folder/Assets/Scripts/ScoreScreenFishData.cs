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
	public float gradeScore;
	private float score;
	public float p_score;
	public float rarity;
	public float maxWeight;
	public float weight;
	public float distance;

	private Sprite gradeA;
	private Sprite gradeB;
	private Sprite gradeC;

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
        Debug.Log("-----------------");
        Debug.Log ("Fish number is " + fishNumber);
		Debug.Log ("fManager.caughtFish.count" + fManager.caughtFish.Count);
        Debug.Log("-----------------");
        if (fManager) { Debug.Log("fmanager OK"); }
        else { Debug.Log("fmanager FAILED!!"); }
        if (fManager.caughtFish.Count >= fishNumber) {
			Fish fish = fManager.caughtFish [fishNumber-1];

			string fishIconPath = "Prefabs/Fish/Icons/" + fish.getIcon ();
			string stringToLoad = fishIconPath.Substring (0, fishIconPath.Length - 1);


			Texture2D tex = Resources.Load<Texture2D> (stringToLoad);



			if (tex != null)
				fishIcon.sprite = Sprite.Create (tex, new Rect (0, 0, tex.width, tex.height), new Vector2 (0.5f, 0.5f));
			else
				Debug.Log ("Error loading fish icon: " + fish.getIcon ());

			fishNameTxt.text = fish.getName ();
			fishWeightTxt.text = fish.getWeight () + " ";

			distance = fish.getDistance();
			maxWeight = fish.getMaxW ();
			weight = fish.getWeight ();
			rarity = fish.getRarity();




			//Score for this fish
			gradeScore = weight / maxWeight;
			Debug.Log ("Fish Grade is" + gradeScore);

			score = (rarity+ (rarity * (gradeScore))) + ((distance/70)*100);


			if (gradeScore <= 0.30f) {
				Debug.Log ("GRADE C");		
				string gIcon = "Prefabs/Fish/GradeIcons/GradeC";
				Texture2D tex2 = Resources.Load<Texture2D> (gIcon);

				if (tex2 != null)
					gradeIcon.sprite = Sprite.Create (tex2, new Rect (0, 0, tex2.width, tex2.height), new Vector2 (0.5f, 0.5f));
				else
					Debug.Log ("Error loading fish icon: C grade");

			} 

			else if (gradeScore > 0.30f && gradeScore <= 0.90f) {
				Debug.Log ("GRADE B");
				string gIcon = "Prefabs/Fish/GradeIcons/GradeB";
				Texture2D tex2 = Resources.Load<Texture2D> (gIcon);
			
				if (tex2 != null)
					gradeIcon.sprite = Sprite.Create (tex2, new Rect (0, 0, tex2.width, tex2.height), new Vector2 (0.5f, 0.5f));
				else
					Debug.Log ("Error loading fish icon: Grade B");

			} 

			else if (gradeScore > 0.90f) {
				Debug.Log ("GRADE C");
				string gIcon = "Prefabs/Fish/GradeIcons/GradeA";
				Texture2D tex2 = Resources.Load<Texture2D> (gIcon);


				if (tex2 != null)
					gradeIcon.sprite = Sprite.Create (tex2, new Rect (0, 0, tex2.width, tex2.height), new Vector2 (0.5f, 0.5f));
				else
					Debug.Log ("Error loading fish icon: Grade A");
			}
		} else
        {
           
        }
	}

	public float GetScore(){
		return score;
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
