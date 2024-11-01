using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public Upgrades[] upgrades;

    
    public void SaveUpgrades(GameData data)
    {
        List<int> upgradeCosts = new List<int>();
        List<int> purchaseCounts = new List<int>();
        
        foreach(var item in upgrades)
        {
            upgradeCosts.Add(item.GetCurrentCost());
            purchaseCounts.Add(item.GetPurchaseCount());
        }

        data.upgradeCosts = upgradeCosts.ToArray();
        data.purchaseCounts = purchaseCounts.ToArray();
    }

    public void LoadUpgrades(GameData data)
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            if (i < data.upgradeCosts.Length)
            {
                upgrades[i].SetCurrentCost(data.upgradeCosts[i]);
                upgrades[i].SetPurchaseCount(data.purchaseCounts[i]);
            }
        }
    }
}
