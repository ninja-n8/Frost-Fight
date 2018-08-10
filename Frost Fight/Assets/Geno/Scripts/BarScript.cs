using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    private float fillAmount;

    [SerializeField] private float lerpSpeed = 2;

    [SerializeField] private Image content;

    [SerializeField] private Text valueText;

    [SerializeField] private Color fullColor;
    [SerializeField] private Color lowColor;
    [SerializeField] private bool lerpColors;

    public NG_Player PlayerController;

    public float FillAmount
    {
        get { return fillAmount; }

        set { fillAmount = value; }
    }

    public Image Content
    {
        get { return content; }

        set { content = value; }
    }

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            string[] tmp = valueText.text.Split(':');
            valueText.text = tmp[0] + ":" + Mathf.FloorToInt(value);
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }

    // Use this for initialization
    void Start()
    {
        PlayerController = FindObjectOfType<NG_Player>();

        if (lerpColors)
        {
            content.color = fullColor;
        }

    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }

    //grabs the fill amount from the content image so we can manipulate it
    private void HandleBar()
    {
        content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);

        if (lerpColors)
            content.color = Color.Lerp(lowColor, fullColor, fillAmount);
    }

    //translates our fuel number to a fill amount that our UI image can understand
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
