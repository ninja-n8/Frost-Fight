using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour {

    public LevelManager levelManager;
    public LevelManager levelComplete;

    // Acts as trigger for the end of the level
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")

        {
            levelComplete.CompleteDemo();
           
        }
    }
    
    
}
