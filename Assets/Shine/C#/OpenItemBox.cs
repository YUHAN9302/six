using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenItemBox : MonoBehaviour
{
    public GameObject[] itemImages;           // 5 個圖片欄位
    public GameObject[] itemInfo;
    public void OnEnable()
    {
        if (SetAndGetSaveData.SelectID == 0)
        {
            for (int i = 0; i < itemImages.Length; i++)
            {
                itemImages[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else
        {
            var (_, _, items) = FindObjectOfType<SaveManager>().LoadPlayerPosition(SetAndGetSaveData.SelectID);

            for (int i = 0; i < itemImages.Length; i++)
            {
                if (i < items.Count && !string.IsNullOrEmpty(items[i]))
                {
                    string itemName = items[i];

                    if (itemName != null)
                        itemImages[i].transform.GetChild(0).gameObject.SetActive(true);
                    else
                        itemImages[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    itemImages[i].transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }
    public void OpenItemInfo(int ID) {
        if (itemImages[ID].transform.GetChild(0).gameObject.active)
        {
            itemInfo[ID].SetActive(true);
        }
    }

}
