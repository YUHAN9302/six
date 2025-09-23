using UnityEngine;
[System.Serializable]
public class DialogueLine
{
    public string speaker;
    public string content;
    public string imageFile;
    public string audioFile;
    public string bigImageFile; // ⭐ 新增：對話大圖檔名
    public Sprite image;
    public AudioClip audio;
    public Sprite bigImage; // ⭐ 新增：對話大圖
}
