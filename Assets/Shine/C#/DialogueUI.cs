using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueUI : MonoBehaviour
{
     DialogueManager dialogueManager; // �qExcelŪ�������
    public Text speakerText;
    public Text contentText;
    public Button nextButton;

    public int currentLine = 0;
    [Header("��ܵ��� �@�P�n����������")]
    public GameObject CloseObj;

    public Image dialogueImage;
    public Image dialogueImageBig;   // F��G��ܤj��
    public AudioSource audioSource;
    void Start()
    {
        dialogueManager = this.GetComponent<DialogueManager>();
        nextButton.onClick.AddListener(ShowNextLine);
        currentLine = 0;
        ShowNextLine(); // �@�}�l��ܲĤ@�y
    }

    void ShowNextLine()
    {
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
            gameObject.SetActive(false);
            if (CloseObj != null)
                CloseObj.SetActive(false);
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
