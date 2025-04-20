using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ClickableObject : MonoBehaviour
{
    [Header("�p�G����ܴN����")]
    public bool isDialogue;
    [Header("��ܪ���")]
    public GameObject DialogueObj;
     bool isClick;
    [Header("����p�G�i�H����Ĳ�o�N����")]
    public bool isLoop;
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
      if (GetComponent<ClickableObject>().isClick) {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    private void OnMouseDown()
    {
        if (isLoop)
        {
            if (TriggerManager.Instance != null)
            {
                TriggerManager.Instance.RecordClick(gameObject.name);
            }

            if (isDialogue)
            {
                DialogueObj.SetActive(isDialogue);
                DialogueObj.GetComponent<DialogueUI>().Reset();
            }
        }
        else
        {
            if (!isClick)
            {
                isClick = true;



                if (TriggerManager.Instance != null)
                {
                    TriggerManager.Instance.RecordClick(gameObject.name);
                }

                if (isDialogue)
                {
                    DialogueObj.SetActive(isDialogue);

                    //   DialogueObj.GetComponent<DialogueUI>().Reset();
                }

            }
        }
    }
}
