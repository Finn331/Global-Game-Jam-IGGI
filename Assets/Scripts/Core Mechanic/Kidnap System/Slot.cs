using System;
using System.Collections;
using UnityEngine;

public class Slot : MonoBehaviour
{
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
        Debug.Log("Pindah");
    }
}
