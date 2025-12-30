using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MerchantAndUpgrades : MonoBehaviour
{
    GameObject hook;
    public Fishing fishComponents;
    public int money = 0;
    public int upgradeLevel = 0;
    public TextMeshPro upgradeText;
    public AudioSource sellFish;
    public AudioSource buyUpgrade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("merchant") && fishComponents.caughtFish)
        {
            fishComponents.caughtFish = false;
            money++;
            sellFish.Play();
        }
        if (other.CompareTag("upgradeArea"))
        {
            if (upgradeLevel == 0)
            {
                if (money >= 5)
                {
                    upgradeLevel = 1;
                    fishComponents.timeFishing = 2;
                    upgradeText.text = "Upgrade:" + Environment.NewLine + " Level 2";
                    buyUpgrade.Play();
                }
            }
            else if (upgradeLevel == 1)
            {
                if (money >= 10)
                {
                    upgradeLevel = 2;
                    fishComponents.timeFishing = 1;
                    upgradeText.text = "Upgrade:" + Environment.NewLine + " Level 3";
                    buyUpgrade.Play();

                }
            }
            else
            {
                if (money >= 25)
                {
                    fishComponents.maxLevel = true;
                    upgradeText.text = "Upgrade:" + Environment.NewLine + "Max Level!";
                    buyUpgrade.Play();
                }
            }
        }
    }
}