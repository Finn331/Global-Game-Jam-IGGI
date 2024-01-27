using UnityEngine;

public class Machine : MonoBehaviour
{
    public GameObject interactingNPC;
    public float countdownTime;
    public bool isCountingDown = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            interactingNPC = other.gameObject;
            isCountingDown = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "NPC")
        {
            interactingNPC = null;
            isCountingDown = false;
        }
    }

    void Update()
    {
        if (isCountingDown)
        {
            countdownTime -= Time.deltaTime;

            if (countdownTime <= 0f)
            {
                DestroyNPC();
            }
        }
    }

    void DestroyNPC()
    {
        if (interactingNPC != null)
        {
            Destroy(interactingNPC);
            interactingNPC = null;
            isCountingDown = false;
            countdownTime = 30f; // Reset countdown time for the next interaction
        }
    }
}