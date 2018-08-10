using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NG_Store : MonoBehaviour
{
    public GameObject UpgradeMenuUI;
    [SerializeField] bool shopping = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!shopping)
            {
                if (Input.GetKeyDown(KeyCode.E))
                    BeginShopping();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                    DoneShopping();
            }
        }
    }

    public void BeginShopping()
    {
        Time.timeScale = 0f;
        UpgradeMenuUI.SetActive(true);
        shopping = true;
    }
    public void DoneShopping()
    {
        Time.timeScale = 1f;
        UpgradeMenuUI.SetActive(false);
        shopping = false;
    }
}
