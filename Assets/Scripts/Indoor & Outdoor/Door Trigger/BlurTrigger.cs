using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurTrigger : MonoBehaviour
{
    [Header("Blur")]
    public GameObject blurIndoor;
    public GameObject blurOutdoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Indoor")
        {
            blurOutdoor.SetActive(true);
            blurIndoor.SetActive(false);
        }
        else if (collision.tag == "Outdoor")
        {
            blurOutdoor.SetActive(false);
            blurIndoor.SetActive(true);
        }
    }
}
