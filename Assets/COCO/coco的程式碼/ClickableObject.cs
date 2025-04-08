using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    [Header("如果有對話就打勾")]
    public bool isDialogue;
    [Header("對話物件")]
    public GameObject DialogueObj;
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
        if (isDialogue) {
            DialogueObj.SetActive(isDialogue);
            DialogueObj.GetComponent<DialogueUI>().Reset();
        }
        
    }
}
