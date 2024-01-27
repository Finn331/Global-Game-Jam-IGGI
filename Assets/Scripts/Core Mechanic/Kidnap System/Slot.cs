using System;
using System.Collections;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject emptyBag;
    public GameObject fullBag;
    public Inventory inventory;
    public int i;

    public void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }
    }
    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        emptyBag.SetActive(true);
        fullBag.SetActive(false);
        Debug.Log("Pindah");
    }
}
