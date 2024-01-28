using UnityEngine;

public class KidnapSystemV2 : MonoBehaviour
{
    public string npcTag = "NPC";
    public string[] machineTags = { "Machine", "Machine2", "Machine3" }; // Tambahkan tag-tag mesin yang valid
    private GameObject carriedNPC;

    public GameObject emptyBag;
    public GameObject fullBag;

    // Tambahkan variabel status Machine untuk setiap tag mesin
    public bool machineOccupied = false;
    public bool machine2Occupied = false;
    public bool machine3Occupied = false;

    void Update()
    {
        // Cek input pemain untuk mengangkat dan menyatu
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (carriedNPC != null)
            {
                // Jika pemain sudah mengangkat objek NPC, cek apakah sedang bersentuhan dengan mesin yang belum terisi
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1.0f);
                foreach (var collider in hitColliders)
                {
                    if (IsMachineTag(collider.tag) && !IsMachineOccupied(collider.tag))
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

    private bool IsMachineTag(string tag)
    {
        // Check if the tag is one of the specified machine tags
        foreach (var machineTag in machineTags)
        {
            if (tag.Equals(machineTag))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsMachineOccupied(string tag)
    {
        // Return the corresponding machineOccupied status based on the tag
        switch (tag)
        {
            case "Machine":
                return machineOccupied;
            case "Machine2":
                return machine2Occupied;
            case "Machine3":
                return machine3Occupied;
            default:
                return false;
        }
    }

    public void CarryNPC(GameObject npc)
    {
        emptyBag.SetActive(false);
        fullBag.SetActive(true);
        // Set objek NPC sebagai child dari pemain, nonaktifkan Rigidbody dan Collider untuk menghindari konflik fisika
        npc.transform.parent = transform;
        npc.GetComponent<SpriteRenderer>().enabled = false;
        npc.GetComponent<Rigidbody2D>().isKinematic = true;
        npc.GetComponent<Collider2D>().enabled = false;
        npc.GetComponent<NPCPatrol>().enabled = false;
        carriedNPC = npc;
    }

    private void MergeWithMachine(GameObject machine)
    {
        // Hanya lanjutkan jika pemain mengangkat objek NPC dan objek Machine tidak null
        if (carriedNPC != null && machine != null)
        {
            fullBag.SetActive(false);
            emptyBag.SetActive(true);

            carriedNPC.GetComponent<SpriteRenderer>().enabled = true;
            carriedNPC.GetComponent<Collider2D>().enabled = true;
            // Set parent dari objek NPC menjadi objek Machine
            carriedNPC.transform.parent = machine.transform;

            // Atur posisi dan rotasi objek NPC menjadi sesuai dengan objek Machine (opsional)
            carriedNPC.transform.position = machine.transform.position;
            carriedNPC.transform.rotation = machine.transform.rotation;

            // Hapus Collider dan komponen lain yang tidak diperlukan dari objek NPC
            //Destroy(carriedNPC.GetComponent<Collider2D>());

            // Set status Machine terisi berdasarkan tag
            SetMachineOccupied(machine.tag);

            // Reset carriedNPC menjadi null agar pemain dapat mengangkat objek NPC yang lain
            carriedNPC = null;
        }
    }

    private void SetMachineOccupied(string tag)
    {
        // Set machineOccupied status berdasarkan tag
        switch (tag)
        {
            case "Machine":
                machineOccupied = true;
                break;
            case "Machine2":
                machine2Occupied = true;
                break;
            case "Machine3":
                machine3Occupied = true;
                break;
        }
    }

    private bool AreMachinesEmpty()
    {
        // Return true if all machines are not occupied
        return !machineOccupied && !machine2Occupied && !machine3Occupied;
    }

    void LateUpdate()
    {
        // Check if all machines are empty, and set the status to false
        if (AreMachinesEmpty())
        {
            machineOccupied = false;
            machine2Occupied = false;
            machine3Occupied = false;
        }
    }

    public void GameOver()
    {
        // Reset semua status Machine
        machineOccupied = false;
        machine2Occupied = false;
        machine3Occupied = false;

        // Hapus semua NPC yang ada di dalam scene
        GameObject[] npcs = GameObject.FindGameObjectsWithTag(npcTag);
        foreach (var npc in npcs)
        {
            Destroy(npc);
        }

        // Hapus semua objek NPC yang ada di dalam tas
        if (carriedNPC != null)
        {
            Destroy(carriedNPC);
        }

        // Reset carriedNPC menjadi null agar pemain dapat mengangkat objek NPC yang lain
        carriedNPC = null;

        // Tampilkan tas kosong
        emptyBag.SetActive(true);
    }
}
