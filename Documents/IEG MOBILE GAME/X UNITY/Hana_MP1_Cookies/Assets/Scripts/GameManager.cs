using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int cookies;
    public static int multiplier;
    public static int idleIncrement;

    public TMP_Text cookiesText;
    public TMP_Text cookiesPerSecText;

    [SerializeField] public Transform btnPosition;
    [SerializeField] public GameObject smallCookiePrefab;
    [SerializeField] public Canvas mainCookieCanvas;

    public Animator clickAnimator;

    void Start()
    {
        // Ensure the Animator component is assigned either via Inspector or code
        if (clickAnimator == null)
        {
            Debug.LogError("Animator not assigned!");
        }

        LoadGameData();

        InvokeRepeating("Cookie", 1, 1);//auto increment
    }

    void Update()
    {
        cookiesText.text = NumberFormater.FormatNumber(cookies) + " cookies";
        cookiesPerSecText.text = CookiePerSec() + " /s";
    }

    public void Cookie()
    {
        cookies += idleIncrement; //auto click @ increment
    }

    public int CookiePerSec()
    {
        return multiplier + idleIncrement;
    }

    public void CookieClick()
    {
        cookies += multiplier; //player click

        if (clickAnimator != null)
        {
            clickAnimator.SetTrigger("Click");
        }

        //convert screen position to canvas position
        Vector2 screenPosition = Input.mousePosition;
        Vector2 worldPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            mainCookieCanvas.transform as RectTransform, screenPosition,
            mainCookieCanvas.worldCamera, out worldPosition);

        //instantiate sCookie at calculated world position
        if (smallCookiePrefab != null)
        {
            GameObject smallCookie = Instantiate(smallCookiePrefab, mainCookieCanvas.transform);
            smallCookie.GetComponent<RectTransform>().localPosition = worldPosition;
            smallCookie.transform.localScale = Vector3.one;
        }
        else
        {
            print("Unassigned prefab : small cookie");
        }

    }

    public int GetTotalCookiesClicked()
    {
        return cookies;
    }

    public int GetCookiesMultiplier()
    {
        return multiplier;
    }

    public int GetCookiesIdleIncrement()
    {
        return idleIncrement;
    }

    public void LoadGameData()
    {
        SaveManager saveManager = FindAnyObjectByType<SaveManager>();
        GameData data = saveManager.LoadGame();

        if(data != null)
        {
            cookies = data.totalCookiesClicked;
            multiplier = data.cookiesMultiplier;
            idleIncrement = data.cookiesIdleIncrement;
        }
        else
        {
            cookies = 0;
            multiplier = 1;
            idleIncrement = 0; //auto click
        }
    }

}
