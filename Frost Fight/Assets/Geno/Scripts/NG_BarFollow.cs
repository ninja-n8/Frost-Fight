using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NG_BarFollow : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemyBar;
    [SerializeField] private float offset;

    public GameObject EnemyBar
    { get { return enemyBar; } set { enemyBar = value; } }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(enemy.transform.position);
        enemyBar.transform.position = new Vector2(targetPos.x, targetPos.y + offset);

        /*if(enemy == null)
        {
            enemyBar.enable = !enemyBar.enable;
        }*/
    }
}
