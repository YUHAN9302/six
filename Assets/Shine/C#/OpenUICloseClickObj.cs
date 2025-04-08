using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUICloseClickObj : MonoBehaviour
{
    public List<GameObject> Obj;
    private void Awake()
    {
    

    }
    void OnEnable()
    {
        if (Obj.Count <= 0 && Application.loadedLevelName == "0228") {
            Obj.Clear();
            for (int i = 0; i < GameObject.Find("觸發物件").transform.childCount; i++) {
                Obj.Add(GameObject.Find("觸發物件").transform.GetChild(i).gameObject);
            }
        }
            for (int i = 0; i < Obj.Count; i++) {
            Obj[i].GetComponent<Collider2D>().enabled = false;
        }
    }
    void OnDisable()
    {
        for (int i = 0; i < Obj.Count; i++)
        {
            Obj[i].GetComponent<Collider2D>().enabled = true;
        }
    }
    
   
}
