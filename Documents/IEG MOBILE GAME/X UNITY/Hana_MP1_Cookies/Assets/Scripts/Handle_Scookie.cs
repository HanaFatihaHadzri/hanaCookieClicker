using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Handle_Scookie : MonoBehaviour
{
    public float fadeDuration = 1f; // fadeout
    public float flySpeed = 100f;

    private Image smallCookie_Img;
    private float startAlpha;
    private RectTransform rectTransform;

    void Awake()
    {
        smallCookie_Img = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        startAlpha = smallCookie_Img.color.a;
    }

    void OnEnable()
    {
        StartCoroutine(FlyDownAndFadeOut());
    }

    IEnumerator FlyDownAndFadeOut()
    {
        float elapsedTime = 0f;
        Color color = smallCookie_Img.color;

        while(elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            //fly cookie
            rectTransform.anchoredPosition += Vector2.down * flySpeed * Time.deltaTime;

            //fade out methods
            color.a = Mathf.Lerp(startAlpha, 0, elapsedTime / fadeDuration);
            smallCookie_Img.color = color;
            
            yield return null;
        }

        Destroy(gameObject); //destroy after fade out
    }
}
