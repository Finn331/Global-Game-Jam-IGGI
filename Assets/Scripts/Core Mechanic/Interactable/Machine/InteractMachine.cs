using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractMachine : MonoBehaviour
{
    public Slot slot;
    public GameObject kidnappedText;

    void Start()
    {
        slot = GetComponentInChildren<Slot>();
    }

    void Update()
    {
        if (kidnappedText.activeSelf && Input.GetKey(KeyCode.V))
        {
            if (slot != null)
            {
                // GameObject.Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");
        if (collision.CompareTag("Player"))
        {
            kidnappedText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        kidnappedText.SetActive(false);
    }
}
