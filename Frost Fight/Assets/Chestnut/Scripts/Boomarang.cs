using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomarang : MonoBehaviour
{
    public SnowBall Sno;
    public Tweapon bossShot;
    public GameObject child;
    [SerializeField] GameObject tornadoPlat;
    public bool boss;
    public NG_StatManager stat;

    // Use this for initialization
    void Start()
    {
        Sno = FindObjectOfType<SnowBall>();
        //Sno = this.gameObject.GetComponent<SnowBall>();
        StartCoroutine(rang());
        stat = FindObjectOfType<NG_StatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //child.transform.parent = this.gameObject.transform;
        if (child == null)
            Debug.Log("Boomerang has not grabbed a pickup");
        if (tornadoPlat == null)
            Debug.Log("TornadoPlat not assigned.");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 13)
        {
            child = other.gameObject;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && boss)
        {
            stat.Health.CurrentVal--;
        }
    }

    public IEnumerator rang()
    {
        float tmpSpeed = Sno.throwSpeed;

        yield return new WaitForSeconds(.3f);
        Sno.throwSpeed = 0;
        if (this.gameObject.name == "Boomarang2(Clone)")
            Debug.Log("This is the boss boomerang");
        else
            Instantiate(tornadoPlat, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
        //(Instantiate(tornadoPlat, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), transform.rotation) as GameObject).transform.parent = this.transform;
        yield return new WaitForSeconds(0.1f);
        //GameObject.Destroy(this.gameObject.transform.GetChild(0).gameObject);
        Sno.throwSpeed = tmpSpeed * -1;
        yield return new WaitForSeconds(.4f);
        child = null;
    }
}
