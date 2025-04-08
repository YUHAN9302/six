using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    private void Start()
    {
        if (TriggerManager.Instance != null)
        {
            TriggerManager.Instance.RegisterObject(gameObject.name);
        }
        else
        {
            Debug.LogError("TriggerManager not found! Make sure 'TriggerManager' GameObject exists in the scene.");
        }
    }
    private void Update()
    {
      
    }
    private void OnMouseDown()
    {
        
            if (TriggerManager.Instance != null)
            {
                TriggerManager.Instance.RecordClick(gameObject.name);
            }
        
    }
}
