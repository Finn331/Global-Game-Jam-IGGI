using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KidnapSystem : MonoBehaviour
{
    [Header("Kidnap System")]
    public GameObject emptyBag;
    public GameObject fullBag;
    public GameObject npc;
    public GameObject kidnappedText;
    public Inventory inventory;
    public GameObject itemButton;

    public bool canPickup = false;


    // Start is called before the first frame update
    void Start()
    {
        // isKidnapped = false;
        emptyBag.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            emptyBag.SetActive(false);
            fullBag.SetActive(true);
            PickupItem();
            Debug.Log("ambil");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");
        if (collision.CompareTag("Player"))
        {
            canPickup = true;
        }
        kidnappedText.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canPickup = false;
        kidnappedText.SetActive(false);
    }

    private void PickupItem()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (!inventory.isFull[i])
            {
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.slots[i].transform, false);
                npc.SetActive(false);
                break;
            }
        }
    }

    public void SetNPCActive(bool isActive)
    {
        if (npc != null)
        {
            npc.SetActive(true);
            Debug.Log("NPC diaktifkan atau dinonaktifkan");
        }
        else
        {
            Debug.LogWarning("NPC reference is null!");
        }
    }
}
