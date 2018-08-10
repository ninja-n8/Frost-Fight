using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NG_BombableSurface : MonoBehaviour
{
    private Color normalColor;
    [SerializeField] private Color bombedColor;
    //[SerializeField] private float colorSpeed;
    private Vector3 minScale;
    [SerializeField] private Vector3 maxScale;
    [SerializeField] private float scaleSpeed;

    [SerializeField] private bool beingBombed;

    public bool BeingBombed
    { get { return beingBombed; } set { beingBombed = value; } }

    // Use this for initialization
    void Start()
    {
        normalColor = this.GetComponent<SpriteRenderer>().color;
        minScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (beingBombed)
            StartCoroutine(Destruction(minScale, maxScale, 1.0f));
    }

    public IEnumerator Destruction(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * scaleSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            this.transform.localScale = Vector3.Lerp(a, b, i);
            this.GetComponent<SpriteRenderer>().color = Color.Lerp(normalColor, bombedColor, i);
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
