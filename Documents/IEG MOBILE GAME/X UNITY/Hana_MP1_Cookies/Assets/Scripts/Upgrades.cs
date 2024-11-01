using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class Upgrades : MonoBehaviour
{
    public int initialCost; //initial cost of upgrade
    public int costIncrement; //increment amount for each purchase
    public int reward;
    public int maxCost; //maximum cost of item

    public bool isPerClick; // eg: 1 cursor = +1 cookie click
    public bool isPerIdle; // eg: 1 grandma = 5 auto cookie

    public GameObject errorBuyBanner;
    public TMP_Text costText; // UI price tag
    public TMP_Text ownUpgradeItem; //UI number of owned upgrade items

    private int currentCost;
    private int purchaseCount; // count number of purchase

    public Button maxBtn;


    void Start()
    {
        LoadUpgradeData();
    }

    // Update is called once per frame
    public void UpgradeItem()
    {
        if(isPerClick)
        {
            if(GameManager.cookies >= currentCost)
            {
                GameManager.cookies -= currentCost;
                GameManager.multiplier += reward;

                //increment cost for next buy
                if(currentCost + costIncrement <= maxCost)
                {
                    currentCost += costIncrement;
                    purchaseCount++;
                }
                else
                {
                    currentCost = maxCost;
                    //change button to max
                    maxBtn.interactable = false;
                }

                UpdateUI();
            }
            else
            {
                ShowErrorBanner();
            }
        }
        else if(isPerIdle)
        {
            if (GameManager.cookies >= currentCost)
            {
                GameManager.cookies -= currentCost;
                GameManager.idleIncrement += reward;

                //increment cost for next buy
                if (currentCost + costIncrement <= maxCost)
                {
                    currentCost += costIncrement;
                    purchaseCount++;
                }
                else
                {
                    currentCost = maxCost;
                    //change button to max
                    maxBtn.interactable = false;
                }
                UpdateUI();
            }
            else
            {
                ShowErrorBanner();
            }
        }
    }

    void ShowErrorBanner()
    {
        errorBuyBanner.SetActive(true);
        Invoke("OffBanner", 3);
    }

    void OffBanner()
    {
        errorBuyBanner.SetActive(false);
    }

    void UpdateUI()
    {
        costText.text = "" + NumberFormater.FormatNumber(currentCost);

        if(purchaseCount > 0)
        {
            ownUpgradeItem.text = "x" + purchaseCount;
        }
    }

    public int GetCurrentCost()
    {
        return currentCost;
    }

    public void SetCurrentCost(int cost)
    {
        currentCost = cost;
        UpdateUI();
    }

    public int GetPurchaseCount()
    {
        return purchaseCount;
    }

    public void SetPurchaseCount(int count)
    {
        purchaseCount = count;
        UpdateUI();
    }

    public void LoadUpgradeData()
    {
        SaveManager saveManager = FindAnyObjectByType<SaveManager>();
        GameData data = saveManager.LoadGame();

        if(data != null)
        {
            UpgradeManager upgradeManager = FindAnyObjectByType<UpgradeManager>();
            upgradeManager.LoadUpgrades(data);
        }
        else
        {
            //initialize cost
            currentCost = initialCost;
            purchaseCount = 0;
            UpdateUI();
        }
    }
}
