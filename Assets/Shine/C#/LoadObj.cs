using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadObj : MonoBehaviour
{
    public GameObject SaveManager;
    // Start is called before the first frame update
    void Start()
    {
       
        if (GameObject.FindObjectOfType <SaveManager>()== null) {
            Instantiate(SaveManager);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
