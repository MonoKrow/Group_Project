using UnityEngine;

public class GoalScript : MonoBehaviour
{

    public AudioClip WinSound;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Goal Reached.");
            audioSource.PlayOneShot(WinSound);
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PlayerObject")
        {
            Debug.Log("Goal Reached.");
            audioSource.PlayOneShot(WinSound);

        }
    }

}
