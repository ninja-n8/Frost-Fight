using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NG_ThrowArm : MonoBehaviour
{
    [SerializeField] private NG_CircleMenu circleMenu;
    [SerializeField] private int rotationOffset = 90;

    // Use this for initialization
    void Start()
    {
        circleMenu = FindObjectOfType<NG_CircleMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!circleMenu.SelectingAmmo)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();

            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
        }
    }
}
