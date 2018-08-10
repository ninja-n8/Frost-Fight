using UnityEngine;
using UnityEngine.SceneManagement;

public class End1Trigger : MonoBehaviour {

    public LevelManager levelManager;
    
   

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            SceneManager.LoadScene("Level 2");
    }
}
