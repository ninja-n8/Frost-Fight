using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NG_UnlockTrigger : MonoBehaviour
{
    [SerializeField] private bool iceballUnlock;
    [SerializeField] private bool rockballUnlock;
    [SerializeField] private bool shotgunUnlock;
    [SerializeField] private bool explosionUnlock;
    [SerializeField] private bool growerUnlock;
    [SerializeField] private bool boomerangUnlock;
    [SerializeField] private bool healthPickupProjUnlock;

    private NG_UpgradeProgress upgradeProgress;
    [SerializeField] private NG_UnlockPopup popup;

    // Use this for initialization
    void Start()
    {
        upgradeProgress = FindObjectOfType<NG_UpgradeProgress>();
        popup = FindObjectOfType<NG_UnlockPopup>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Unlock!");
            if (iceballUnlock)
            {
                upgradeProgress.IceBall = true;
                popup.IceballUnlocked = true;
            }
            if (rockballUnlock)
            {
                upgradeProgress.RockBall = true;
                popup.RockballUnlocked = true;
            }
            if (shotgunUnlock)
                upgradeProgress.Shotgun = true;
            if (explosionUnlock)
                upgradeProgress.Explosion = true;
            if (growerUnlock)
                upgradeProgress.Grower = true;
            if (boomerangUnlock)
            {
                upgradeProgress.Boomerang = true;
                popup.BoomerangUnlocked = true;
            }
            if (healthPickupProjUnlock)
                upgradeProgress.HealthPickupProj = true;

            if (this.gameObject.transform.GetChild(0).name == "Projectile")
                this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
