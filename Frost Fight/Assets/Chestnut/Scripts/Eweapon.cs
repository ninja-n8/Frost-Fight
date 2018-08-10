using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eweapon : MonoBehaviour {

    public GameObject Me;
    public SnowBall Parent;

    public float throwspeed;

    public SoundManager SM;

    [SerializeField] private NG_BombableSurface bombableObject;

    private Vector3 minExplosionScale;
    [SerializeField] private Vector3 maxExplosionScale;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private bool exploding = false;

	// Use this for initialization
	void Start () {
        Me = this.gameObject;
        Parent = this.gameObject.GetComponent<SnowBall>();
        SM = FindObjectOfType<SoundManager>();

        minExplosionScale = Me.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(boomTime());
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Parent.throwSpeed = 0;
            if (collision.gameObject.GetComponent<NG_BombableSurface>() != null)
            {
                bombableObject = collision.gameObject.GetComponent<NG_BombableSurface>();
                if (exploding)
                    bombableObject.BeingBombed = true;
            }
        }

        /*if (collision.gameObject.GetComponent<NG_BombableSurface>() != null)
        {
            bombableObject = collision.gameObject.GetComponent<NG_BombableSurface>();
            if (exploding)
                bombableObject.BeingBombed = true;
        }*/
    }

    public IEnumerator boomTime()
    {
        yield return new WaitForSeconds(.5f);
        StartCoroutine(boom(minExplosionScale, maxExplosionScale, 1.0f));
        exploding = true;
        yield return new WaitForSeconds(.5f);
        Destroy(Me);
    }

    public IEnumerator boom(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * lerpSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            Me.transform.GetComponent<SpriteRenderer>().material.color = Color.red;
            Me.transform.localScale = Vector3.Lerp(a, b, i);
            Parent.throwSpeed = 0;
            yield return null;
        }
    }
}
