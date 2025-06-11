using UnityEngine;

public class Collection : MonoBehaviour
{
    public AudioSource collectionSound;
    public AudioSource grugRecieved;
    public bool collectablePickedUp = false;
    public GameObject MarkInHand;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Mark"))
        {
            Destroy(collision.gameObject);
            collectionSound.Play();
            collectablePickedUp = true;
            MarkInHand.SetActive(true);
        }

        if(collision.gameObject.CompareTag("Grug") && collectablePickedUp == true)
        {
            MarkInHand.SetActive(false);
            grugRecieved.Play();
        }
    }
}
