using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public NG_Player player;
    public GameObject shootPoint;
    public GameObject bullet;
    public int shootdist;
    public int shootChance;
    public bool boss;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindObjectOfType<NG_Player>();
        if (!boss)
        {
            StartCoroutine(ShootTime());
        }
        //
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = player.transform.position - transform.position;
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        shootChance = Random.Range(1, 50);

        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (boss && dist <= shootdist && shootChance == 23)
        {
            Shoot();
        }
    }

    public IEnumerator ShootTime()
    {
        yield return new WaitForSeconds(2);
        Shoot();
        StartCoroutine(ShootTime());
    }

    public void Shoot()
    {
        GameObject.Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
    }
}
