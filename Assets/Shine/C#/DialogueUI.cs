using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueUI : MonoBehaviour
{
     DialogueManager dialogueManager; // 從Excel讀取的資料
    public Text speakerText;
    public Text contentText;
    public Button nextButton;

    public int currentLine = 0;
    [Header("對話結束 一同要關閉的物件")]
    public GameObject[] CloseObj;
    [Header("對話結束 一同要開啟的物件")]
    public GameObject[] OpenObj;
    [Header("該對話結束以後可以找尋的物品")]
    public GameObject[] NextIteams;
    public Image dialogueImage;
    public Image dialogueImageBig;   // F欄：對話大圖
    public AudioSource audioSource;
    void Start()
    {
        dialogueManager = this.GetComponent<DialogueManager>();
        nextButton.onClick.AddListener(ShowNextLine);
        currentLine = 0;
        ShowNextLine(); // 一開始顯示第一句
    }

    void ShowNextLine()
    {
        Debug.Log(currentLine);
        Debug.Log(dialogueManager.dialogueLines.Count);

        Debug.Log(currentLine < dialogueManager.dialogueLines.Count);
        if (currentLine < dialogueManager.dialogueLines.Count)
        {
            var line = dialogueManager.dialogueLines[currentLine];
            speakerText.text = line.speaker;
            contentText.text = line.content;

            if (dialogueImage != null && line.image != null)
                dialogueImage.sprite = line.image;
            if (dialogueImageBig != null && line.bigImage != null)
                dialogueImageBig.sprite = line.bigImage;
            if (audioSource != null && line.audio != null)
                audioSource.PlayOneShot(line.audio);

            currentLine++;
        }
        else
        {
            if (NextIteams.Length > 0)
            {
                for (int i = 0; i < NextIteams.Length; i++)
                {
                    NextIteams[i].AddComponent<BoxCollider2D>();
                    NextIteams[i].GetComponent<BoxCollider2D>().isTrigger = true;
                }
            }
            gameObject.SetActive(false);
            if (CloseObj.Length > 0)
            {
                for (int i = 0; i < CloseObj.Length; i++)
                {
                    CloseObj[i].SetActive(false);
                }
            }
            if (OpenObj.Length > 0)
            {
                for (int i = 0; i < OpenObj.Length; i++)
                {
                    OpenObj[i].SetActive(true);
                }
            }
        }
    }

    public void Reset()
    {
        currentLine = 0;

        if (dialogueManager.dialogueLines.Count > 0)
        {
            var line = dialogueManager.dialogueLines[currentLine];
            speakerText.text = line.speaker;
            contentText.text = line.content;
        }

    }
}
