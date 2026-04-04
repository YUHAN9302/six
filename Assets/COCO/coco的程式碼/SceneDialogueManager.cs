using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class DoorDialogue
    {
        public string doorID;
        public GameObject dialogueUI;
    }

    [Header("轉場等待時間")]
    public float transitionDuration = 1.5f;

    [Header("門與對話設定")]
    public List<DoorDialogue> doorDialogues = new List<DoorDialogue>();

    void Start()
    {
        string lastDoorID = 位置紀錄.LastDoorID;

        foreach (var dd in doorDialogues)
        {
            if (dd.dialogueUI != null)
                dd.dialogueUI.SetActive(false);
        }

        foreach (var dd in doorDialogues)
        {
            if (dd.doorID == lastDoorID)
            {
                StartCoroutine(ShowDialogue(dd.dialogueUI));
                break;
            }
        }
    }

    IEnumerator ShowDialogue(GameObject ui)
    {
        yield return new WaitForSecondsRealtime(transitionDuration);
        ui.SetActive(true);
    }
}
