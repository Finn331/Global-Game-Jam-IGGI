using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractMachine : MonoBehaviour
{
    public GameObject kidnappedText;

    public Slot slot;
    public bool isButton;

    void Start()
    {
        if (slot != null)
        {
            // Mendapatkan komponen Button dari skrip Slot
            Button buttonComponent = slot.GetComponent<Button>();

            // Lakukan sesuatu dengan komponen Button
            if (buttonComponent != null)
            {
                // Contoh: Mengubah interactivity tombol
                buttonComponent.interactable = false;
            }
        }
    }

    void Update()
    {
        if (kidnappedText.activeSelf && Input.GetKey(KeyCode.V))
        {

            Debug.Log("Taruh");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Button buttonComponent = slot.GetComponent<Button>();
        Debug.Log("Collided");
        if (collision.CompareTag("Player"))
        {
            isButton = true;
            buttonComponent.interactable = true;
        }
        kidnappedText.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Button buttonComponent = slot.GetComponent<Button>();
        isButton = false;
        buttonComponent.interactable = false;
        kidnappedText.SetActive(false);
    }
}
