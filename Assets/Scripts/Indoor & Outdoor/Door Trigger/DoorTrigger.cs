using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [Header("Blur")]
    public GameObject blurOutdoor; // untuk nge blur outdoor
    public GameObject blurIndoor; // untuk nge blur indoor

    [Header("Door Collider")]
    public BoxCollider2D frontDoorCollider; // collider trigger pintu
    public GameObject secondCollider;
    public GameObject triggerIndoor;
    public GameObject triggerOutdoor;


    // Start is called before the first frame update
    void Start()
    {
        blurOutdoor.SetActive(true);
        blurIndoor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Outdoor")
        {
            blurOutdoor.SetActive(false);
            blurIndoor.SetActive(true);
            frontDoorCollider.enabled = true;
            secondCollider.SetActive(true);
            triggerIndoor.SetActive(true);
            triggerOutdoor.SetActive(false);
        }

        if (collision.tag == "Indoor")
        {
            blurOutdoor.SetActive(true);
            blurIndoor.SetActive(false);
            frontDoorCollider.enabled = true;
            secondCollider.SetActive(true);
            triggerOutdoor.SetActive(true);
            triggerIndoor.SetActive(false);
        }
    }
}
