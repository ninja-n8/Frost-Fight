using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    public GameObject theseSpikes;
    private NG_StatManager SM;
    private SoundManager sound;
	[SerializeField]
	private float Spawnspeed;
	[SerializeField]
	private float Despawnspeed;

    // Use this for initialization
    void Start () {
        SM = FindObjectOfType<NG_StatManager>();
        sound = FindObjectOfType<SoundManager>();
        StartCoroutine(SpikeHit());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator SpikeHit()
    {
		yield return new WaitForSeconds(Spawnspeed);
        theseSpikes.SetActive(false);
		yield return new WaitForSeconds(Despawnspeed);
        theseSpikes.SetActive(true);
        StartCoroutine(SpikeHit());
    }
    
}
