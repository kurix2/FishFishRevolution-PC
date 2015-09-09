using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour {

    public static SceneFade SceneFader;

    public float fadeSpeed = 1f;
    private bool sceneStarting = true;
    private bool sceneEnding = false;

    private Image image;

    void Awake()
    {
        if (SceneFader == null)
            SceneFader = this;

        image = GetComponent<Image>();
        image.enabled = true;
    }

    void FadeOut()
    {
        image.color = Color.Lerp(image.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    void FadeIn()
    {
        image.color = Color.Lerp(image.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        FadeOut();

        if (image.color.a <= 0.05f)
        {
            image.color = Color.clear;
            image.enabled = false;
            sceneStarting = false;
        }
    }

    public void EndScene()
    {
        image.enabled = true;
        sceneEnding = true;
        FadeIn();

        if (image.color.a >= 0.95f)
        {
            sceneEnding = false;
            Application.LoadLevel(Application.loadedLevel);
        }
    }


	// Update is called once per frame
	void Update () {
        if (sceneStarting)
            StartScene();
        if (sceneEnding)
            EndScene();
	}
}
