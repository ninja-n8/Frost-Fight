using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NG_UnlockPopup : MonoBehaviour
{
    [SerializeField] GameObject unlockPedestal;
    Text popupText;

    bool rockballUnlocked = false;
    bool iceballUnlocked = false;
    bool boomerangUnlocked = false;

    public bool RockballUnlocked
    { get { return rockballUnlocked; } set { rockballUnlocked = value; } }
    public bool IceballUnlocked
    { get { return iceballUnlocked; } set { iceballUnlocked = value; } }
    public bool BoomerangUnlocked
    { get { return boomerangUnlocked; } set { boomerangUnlocked = value; } }

    // Use this for initialization
    void Start()
    {
        popupText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rockballUnlocked)
        {
            StartCoroutine(ProjectileText("Rockball Unlocked!"));
            rockballUnlocked = false;
        }
        else if (iceballUnlocked)
        {
            StartCoroutine(ProjectileText("Iceball Unlocked!"));
            iceballUnlocked = false;
        }
        else if (boomerangUnlocked)
        {
            StartCoroutine(ProjectileText("Boomerang Unlocked!"));
            boomerangUnlocked = false;
        }
    }

    IEnumerator ProjectileText(string unlockedProj)
    {
        popupText.enabled = true;
        popupText.text = unlockedProj;
        yield return new WaitForSeconds(5.0f);
        popupText.text = "";
        popupText.enabled = false;
        yield return null;
    }
}
