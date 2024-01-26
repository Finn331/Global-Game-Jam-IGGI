using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject targetPosition;
    public Inventory inventory;
    private bool isHidden = false;
    public KidnapSystem kidnapSystem;
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
        isHidden = !isHidden;
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        if (kidnapSystem != null)
        {
            kidnapSystem.SetNPCActive(true);
            kidnapSystem.transform.position = targetPosition.transform.position;
        }
        else
        {
            Debug.LogWarning("KidnapSystem reference is null!");
        }

        Debug.Log("Pindah");
    }
}
