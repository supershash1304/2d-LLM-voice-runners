using System Collections;
using System Collections Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject Ground1, Ground2, Ground3;
    bool hasGround = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnGround()
    {
        int randomNum = Random.Range(1, 4);
        if(randomNum == 1)
        {
            Instantiate(Ground1, new Vector3(Transform.positon.x + 3,))
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            hasGround = true;
        }
    }
    private void OnTriggerExit2D(collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            hasGround = false;
        }
    }
}
