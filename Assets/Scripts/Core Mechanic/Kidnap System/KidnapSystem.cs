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
    public bool isKidnapped;


    // Start is called before the first frame update
    void Start()
    {
        isKidnapped = false;
        emptyBag.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (kidnappedText.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            kidnappedText.gameObject.SetActive(false);
            emptyBag.SetActive(false);
            fullBag.SetActive(true);
            isKidnapped = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            kidnappedText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        kidnappedText.SetActive(false);
    }

    public void Kidnap()
    {

    }
    public void Release()
    {
        emptyBag.SetActive(true);
        fullBag.SetActive(false);
    }
}
