using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoor : MonoBehaviour
{
    [Header("Button Notification")]
    public GameObject buttonInteract; // menggunakan sprite untuk memberi tahukan player bahwa dia bisa berinteraksi dengan object ini

    [Header("Door Collider")]
    public GameObject doorCollider; // collider untuk menutup atau membuka pintu
    private BoxCollider2D boxCollider2D; // collider trigger pintu

    //[Header("Blur")]
    //public GameObject blurOutdoor; // untuk nge blur outdoor
    //public GameObject blurIndoor; // untuk nge blur indoor

    // Start is called before the first frame update
    void Start()
    {
        doorCollider.SetActive(true);
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonInteract.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            doorCollider.SetActive(false);
            buttonInteract.SetActive(false);
            boxCollider2D.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            buttonInteract.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        buttonInteract.SetActive(false);
    }

}
