using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextObj : MonoBehaviour
{
    //public GameObject Next_Obj;
    //public GameObject Close_Obj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Next() {
        gameObject.SetActive(false);
        //if(Close_Obj!=null) Close_Obj.SetActive(false);
        //Next_Obj.SetActive(true);
    }
}
