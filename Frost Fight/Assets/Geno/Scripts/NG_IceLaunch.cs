using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NG_IceLaunch : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private NG_CharacterController2D controller2D;
    [SerializeField] private GameObject pillar;

    private Vector3 minScale;
    [SerializeField] private Vector3 maxScale;
    [SerializeField] private float lerpSpeed;

    [SerializeField] private Vector2 launchPower;
    [SerializeField] private bool launching = false;
    [SerializeField] private bool launched = false;
    [SerializeField] private bool launchRight;


    public GameObject Pillar
    { get { return pillar; } set { pillar = value; } }
    public Vector2 LaunchPower
    { get { return launchPower; } set { launchPower = value; } }
    public bool Launching
    { get { return launching; } set { launching = value; } }
    public bool Launched
    { get { return launched; } set { launched = value; } }
    public bool LaunchRight
    { get { return launchRight; } set { launchRight = value; } }

    // Use this for initialization
    void Start()
    {
        controller2D = FindObjectOfType<NG_CharacterController2D>();
        minScale = pillar.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller2D.collisions.below)
        {
            launched = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player = collision.gameObject;
            if (player.transform.position.x > pillar.transform.position.x)
            {
                pillar.transform.eulerAngles = new Vector3(pillar.transform.rotation.x, pillar.transform.rotation.y, 45);
                launchRight = false;
            }
            else if(player.transform.position.x < pillar.transform.position.x)
            {
                pillar.transform.eulerAngles = new Vector3(pillar.transform.rotation.x, pillar.transform.rotation.y, -45);
                launchRight = true;
            }
            StartCoroutine(Launch(minScale, maxScale, 1.0f));
            launching = true;
        }
    }

    public IEnumerator OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(Launch(maxScale, minScale, 1.0f));
            launching = false;
            launched = true;
            pillar.transform.eulerAngles = new Vector3(pillar.transform.rotation.x, pillar.transform.rotation.y, 0);
            yield return new WaitForSeconds(0.5f);
            Destroy(this.gameObject);
        }
    }

    IEnumerator Launch(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * lerpSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            pillar.transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
