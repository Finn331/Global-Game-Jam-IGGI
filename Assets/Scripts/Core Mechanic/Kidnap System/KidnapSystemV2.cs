using UnityEngine;

public class KidnapSystemV2 : MonoBehaviour
{
    public string npcTag = "NPC";
    public string machineTag = "Machine";
    private GameObject carriedNPC;

    void Update()
    {
        // Cek input pemain untuk mengangkat dan menyatu
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (carriedNPC != null)
            {
                // Jika pemain sudah mengangkat objek NPC, cek apakah sedang bersentuhan dengan "Machine"
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1.0f);
                foreach (var collider in hitColliders)
                {
                    if (collider.CompareTag(machineTag))
                    {
                        MergeWithMachine(collider.gameObject);
                        break;
                    }
                }
            }
            else
            {
                // Jika pemain belum mengangkat objek NPC, cek apakah sedang bersentuhan dengan objek NPC
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1.0f);
                foreach (var collider in hitColliders)
                {
                    if (collider.CompareTag(npcTag))
                    {
                        CarryNPC(collider.gameObject);
                        break;
                    }
                }
            }
        }
    }

    public void CarryNPC(GameObject npc)
    {
        // Set objek NPC sebagai child dari pemain, nonaktifkan Rigidbody dan Collider untuk menghindari konflik fisika
        npc.transform.parent = transform;
        npc.GetComponent<SpriteRenderer>().enabled = false;
        npc.GetComponent<Rigidbody2D>().isKinematic = true;
        npc.GetComponent<Collider2D>().enabled = false;
        carriedNPC = npc;
        
    }

    private void MergeWithMachine(GameObject machine)
    {
        // Hanya lanjutkan jika pemain mengangkat objek NPC dan objek Machine tidak null
        if (carriedNPC != null && machine != null)
        {
            // Set parent dari objek NPC menjadi objek Machine
            carriedNPC.transform.parent = machine.transform;

            // Atur posisi dan rotasi objek NPC menjadi sesuai dengan objek Machine (opsional)
            carriedNPC.transform.position = machine.transform.position;
            carriedNPC.transform.rotation = machine.transform.rotation;

            // Hapus Collider dan komponen lain yang tidak diperlukan dari objek NPC
            Destroy(carriedNPC.GetComponent<Collider2D>());

            // Reset carriedNPC menjadi null agar pemain dapat mengangkat objek NPC yang lain
            carriedNPC = null;
        }
    }
}
