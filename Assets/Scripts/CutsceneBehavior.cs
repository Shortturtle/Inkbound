using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneBehavior : MonoBehaviour
{
    public List<Image> images;
    private int numOfPanels;
    private int counter = 1;
    [SerializeField] private Color transparent;
    private float buffer = 1.3f;
    [SerializeField] private float fadeInTime;
    private float fadeInTimer;

    // Start is called before the first frame update
    void Start()
    {
        numOfPanels = images.Count;
        foreach (var image in images)
        {
            image.color = transparent;
        }

        StartCoroutine(WaitForTrasition());
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeInTimer >= fadeInTime)
        {
            fadeInTimer = 0;
        }
    }

    IEnumerator WaitForTrasition()
    {
        yield return new WaitForSeconds(buffer);

        StartCoroutine(FadeIn(images[counter - 1]));
    }

    IEnumerator FadeIn(Image image)
    {
        while(image.color.a < 1)
        {
            image.color = new Color(image.color.r,image.color.g,image.color.b,(Mathf.Lerp(image.color.a, 1, fadeInTimer/fadeInTime)));
            fadeInTimer += Time.deltaTime;
            yield return null;
        }

        if (counter < numOfPanels)
        {
            counter++;
            fadeInTimer = 0;
            StartCoroutine(FadeIn(images[counter - 1]));
        }
    }
}
