using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NG_CircleMenu : MonoBehaviour
{
    public List<MenuButton> buttons = new List<MenuButton>();
    private Vector2 mousePosition;
    private Vector2 fromVector2M = new Vector2(0.5f, 1.0f);
    private Vector2 centerOfCircle = new Vector2(0.5f, 0.5f);
    private Vector2 toVector2M;

    [SerializeField] private GameObject ammoSelectUI;
    [SerializeField] private bool selectingAmmo = false;

    [SerializeField] private int menuItems;
    [SerializeField] private int curMenuItem;
    [SerializeField] private int oldMenuItem;

    private GameObject curMenuDisplay;
    [SerializeField] private Image curProjectileSprite;
    [SerializeField] Sprite snowballSprite;
    [SerializeField] Sprite iceballSprite;
    [SerializeField] Sprite rockballSprite;
    [SerializeField] Sprite boomerangSprite;

    private NG_Player playerControl;
    private NG_UpgradeProgress upgradeProgress;
    private PauseMenu pauseMenu;

    public bool SelectingAmmo
    { get { return selectingAmmo; } set { selectingAmmo = value; } }

    // Use this for initialization
    void Start()
    {
        playerControl = FindObjectOfType<NG_Player>();
        upgradeProgress = FindObjectOfType<NG_UpgradeProgress>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        //ammoSelectUI = GameObject.Find("AmmoSelectUI");

        menuItems = buttons.Count;
        foreach(MenuButton button in buttons)
        {
            button.buttonImage.color = button.idleColor;
        }
        curMenuItem = 0;
        oldMenuItem = 0;
        curMenuDisplay = GameObject.Find("CurProjectileFrame").transform.GetChild(0).gameObject;
        curProjectileSprite = curMenuDisplay.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentMenuItem();
        CurrentSymbol();

        if (PauseMenu.GameIsPaused == false)
        {
            if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                //if (selectingAmmo)
                    //MenuOff();
                //else
                MenuOn();
            }

            if (Input.GetKeyUp(KeyCode.R) || Input.GetKeyUp(KeyCode.Mouse1))
            {
                if (selectingAmmo)
                    MenuOff();
            }

            if (selectingAmmo)
            {
                //if (Input.GetButtonDown("Fire1"))
                //ButtonAction();
                if (Input.mousePresent)
                    ButtonAction();
            }
        }
        else
        {
            ammoSelectUI.SetActive(false);
            selectingAmmo = false;
        }

        CheckForUpgrades();

        //Just for testing. Remove before building -NG
        AmmoSwap();
    }

    public void MenuOn()
    {
        ammoSelectUI.SetActive(true);
        Time.timeScale = 0.0f;
        selectingAmmo = true;
    }

    public void MenuOff()
    {
        ammoSelectUI.SetActive(false);
        Time.timeScale = 1.0f;
        selectingAmmo = false;
    }

    public void GetCurrentMenuItem()
    {
        mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        toVector2M = new Vector2(mousePosition.x / Screen.width, mousePosition.y / Screen.height);

        float angle = (Mathf.Atan2(fromVector2M.y - centerOfCircle.y, fromVector2M.x - centerOfCircle.x) - Mathf.Atan2(toVector2M.y - centerOfCircle.y, toVector2M.x - centerOfCircle.x)) * Mathf.Rad2Deg;

        if (angle < 0)
            angle += 360;

        curMenuItem = (int)(angle / (360 / menuItems));

        if(curMenuItem != oldMenuItem)
        {
            buttons[oldMenuItem].buttonImage.color = buttons[oldMenuItem].idleColor;
            oldMenuItem = curMenuItem;
            buttons[curMenuItem].buttonImage.color = buttons[curMenuItem].highlightColor;
        }
    }

    public void ButtonAction()
    {
        buttons[curMenuItem].buttonImage.color = buttons[curMenuItem].pressColor;
        if(curMenuItem == 0)
        {
            playerControl.curBall = playerControl.snowBall;
            curProjectileSprite.GetComponent<Image>().sprite = snowballSprite;
        }
        if(curMenuItem == 1)
        {
            if (upgradeProgress.IceBall == true)
            {
                playerControl.curBall = playerControl.iceBall;
                curProjectileSprite.GetComponent<Image>().sprite = iceballSprite;
            }
            else
            {
                return;
            }
        }
        if (curMenuItem == 2)
        {
            if (upgradeProgress.RockBall == true)
            {
                playerControl.curBall = playerControl.rockBall;
                curProjectileSprite.GetComponent<Image>().sprite = rockballSprite;
            }
            else
            {
                return;
            }
        }
        if (curMenuItem == 3)
        {
            if (upgradeProgress.Boomerang == true)
            {
                playerControl.curBall = playerControl.boomerang;
                curProjectileSprite.GetComponent<Image>().sprite = boomerangSprite;
            }
            else
            {
                return;
            }
        }
        if (curMenuItem == 4)
        {
            if (upgradeProgress.Explosion == true)
            {
                playerControl.curBall = playerControl.explosion;
            }
            else
            {
                return;
            }
        }
        if (curMenuItem == 5)
        {
            if (upgradeProgress.Grower == true)
            {
                playerControl.curBall = playerControl.grower;
            }
            else
            {
                return;
            }
        }
        if (curMenuItem == 6)
        {
            if (upgradeProgress.Shotgun == true)
            {
                playerControl.curBall = playerControl.shotgun;
            }
            else
            {
                return;
            }
        }
        if (curMenuItem == 7)
        {
            if (upgradeProgress.HealthPickupProj == true)
            {
                playerControl.curBall = playerControl.healthpickupProj;
            }
            else
            {
                return;
            }
        }
    }

    public void CurrentSymbol()
    {
        if (playerControl.curBall == playerControl.snowBall)
            curProjectileSprite.GetComponent<Image>().sprite = snowballSprite;
        if (playerControl.curBall == playerControl.iceBall)
            curProjectileSprite.GetComponent<Image>().sprite = iceballSprite;
        if (playerControl.curBall == playerControl.rockBall)
            curProjectileSprite.GetComponent<Image>().sprite = rockballSprite;
        if (playerControl.curBall == playerControl.boomerang)
            curProjectileSprite.GetComponent<Image>().sprite = boomerangSprite;
    }

    public void CheckForUpgrades()
    {
        if(upgradeProgress.IceBall == true)
        {
            buttons[1].buttonSymbol.SetActive(true); //turning on the image on the circle menu to show when it's locked/unlocked -NG
        }
        if (upgradeProgress.RockBall == true)
        {
            buttons[2].buttonSymbol.SetActive(true); //turning on the image on the circle menu to show when it's locked/unlocked -NG
        }
        if (upgradeProgress.Shotgun == true)
        {
            buttons[6].buttonSymbol.SetActive(true); //turning on the image on the circle menu to show when it's locked/unlocked -NG
        }
        if (upgradeProgress.Explosion == true)
        {
            buttons[4].buttonSymbol.SetActive(true); //turning on the image on the circle menu to show when it's locked/unlocked -NG
        }
        if (upgradeProgress.Grower == true)
        {
            buttons[5].buttonSymbol.SetActive(true); //turning on the image on the circle menu to show when it's locked/unlocked -NG
        }
        if (upgradeProgress.Boomerang == true)
        {
            buttons[3].buttonSymbol.SetActive(true); //turning on the image on the circle menu to show when it's locked/unlocked -NG
        }
        if (upgradeProgress.HealthPickupProj == true)
        {
            buttons[7].buttonSymbol.SetActive(true); //turning on the image on the circle menu to show when it's locked/unlocked -NG
        }
    }

    //Just for testing. Remove before building -NG
    private void AmmoSwap()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerControl.curBall = playerControl.snowBall;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (upgradeProgress.RockBall != true)
                return;
            else
                playerControl.curBall = playerControl.rockBall;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (upgradeProgress.IceBall != true)
                return;
            else
                playerControl.curBall = playerControl.iceBall;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //Debug.Log("Ready to place ice launcher.");
            if (upgradeProgress.Boomerang != true)
                return;
            else
                playerControl.curBall = playerControl.boomerang;
        }
    }
}

[System.Serializable] 
public class MenuButton
{
    public string name;
    public Image buttonImage;
    public GameObject buttonSymbol;
    public Color idleColor = Color.white;
    public Color highlightColor = Color.gray;
    public Color pressColor = Color.cyan;
}
