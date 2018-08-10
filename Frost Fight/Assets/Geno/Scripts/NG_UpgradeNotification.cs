using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NG_UpgradeNotification : MonoBehaviour
{
    [SerializeField] private GameObject fullWalletNotification;
    [SerializeField] private Text fullWalletText;

    [SerializeField] private NG_UpgradeProgress upgradeProgress;

    // Use this for initialization
    void Start()
    {
        upgradeProgress = FindObjectOfType<NG_UpgradeProgress>();

        fullWalletNotification = GameObject.Find("UpgradeNotification");
        //fullWalletText = fullWalletNotification.gameObject.transform.Find("FullWalletText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if((int) upgradeProgress.highestShards % 4 == 0)
        {
            fullWalletNotification.SetActive(true);
        }
        else
        {
            fullWalletNotification.SetActive(false);
        }*/

        Notifications();
    }

    public void Notifications()
    {
        if((int)upgradeProgress.highestHeartCrystals >= 1)
        {
            fullWalletNotification.SetActive(true);
            fullWalletText.text = "Max Life Upgrade Available! Press Esc";
        }
        else
        {
            if ((int)upgradeProgress.highestShards >= 4)
            {
                fullWalletNotification.SetActive(true);
                fullWalletText.text = "Upgrades Available! Press Esc";
            }
            else
            {
                fullWalletNotification.SetActive(false);
            }
        }
    }
}
