using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObj : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.GetComponent<Collider2D>().name == "Player") {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D hit)
    {
        if (hit.GetComponent<Collider2D>().name == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
