using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {
	public float speed;
	public float chaseRange;
	private Transform target;
	public CheckpointManager CPM;
    public SoundManager SM;

    //Reference to Player StatManager -NG
    private NG_StatManager playerStats;
    private NG_EnemyStatManager enemyStats; //Reference to Enemy stats -NG

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
        CPM = FindObjectOfType<CheckpointManager>();
        SM = FindObjectOfType<SoundManager>();

        //Get NG_StatManager & NG_EnemyStatManager -NG
        playerStats = FindObjectOfType<NG_StatManager>();
        enemyStats = GetComponent<NG_EnemyStatManager>();
	}
	
	// Update is called once per frame
	void Update () {
		float distancetoTarget = Vector2.Distance (transform.position, target.position);
		if (distancetoTarget < chaseRange) {
			Vector2 targetDir = target.position - transform.position;
			transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
		}
	}
	public void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
            SM.hurt();
            //CPM.PlayerSpawn ();
            playerStats.Damage();

            //GameObject.Destroy (this.gameObject);
            enemyStats.EnemyHealth.CurrentVal = 0;
		}
	}
}
