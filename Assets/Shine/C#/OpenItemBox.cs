using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenItemBox : MonoBehaviour
{
    [Tooltip("5 �ӹϤ����]�C�檺�� 0 �Ӥl����|�Q���/���á^")]
    public GameObject[] itemImages;

    [Tooltip("�����C�檺��T����/�������O")]
    public GameObject[] itemInfo;

    private const int SLOT_COUNT = 5;

    private void OnEnable()
    {


        if (SetAndGetSaveData.SelectID == 0)
        {
            ToggleAllSlots(false);
            return;
        }
        var saveMgr = FindObjectOfType<SaveManager>();
        if (saveMgr == null)
        {
            ToggleAllSlots(false);
            Debug.LogWarning("[OpenItemBox] �䤣�� SaveManager�C");
            return;
        }

        var (_, _, _, items, _) = saveMgr.LoadPlayerState(SetAndGetSaveData.SelectID);
        if (items == null)
            items = new List<string>();

        while (items.Count < SLOT_COUNT)
            items.Add(string.Empty);

        for (int i = 0; i < itemImages.Length; i++)
        {
            var slot = itemImages[i];
            if (slot == null) continue;

            string itemName = (i < items.Count) ? items[i] : "";
            bool hasItem = !string.IsNullOrEmpty(itemName);

            if (slot.transform.childCount > 0)
            {
                var icon = slot.transform.GetChild(0).gameObject;
                if (icon != null)
                {
                    icon.SetActive(hasItem);

                    if (hasItem)
                    {
                        var image = icon.GetComponent<Image>();
                        if (image != null)
                        {
                            Sprite itemSprite = LoadIconSpriteForItem(itemName);
                            if (itemSprite != null)
                            {
                                image.sprite = itemSprite;
                            }
                            else
                            {
                                Debug.LogWarning($"�䤣������Ϥ��G{itemName}");
                            }
                        }
                    }
                }
            }
        }

        Debug.Log("[OpenItemBox] �ثe�D��G" + string.Join(",", items));
    }

    public void OpenItemInfo(int id)
    {
        if (itemImages == null || itemInfo == null) return;
        if (id < 0 || id >= itemImages.Length || id >= itemInfo.Length) return;

        var slot = itemImages[id];
        if (slot == null) return;

        if (slot.transform.childCount > 0)
        {
            var icon = slot.transform.GetChild(0).gameObject;
            if (icon != null && icon.activeSelf)
            {
                if (itemInfo[id] != null)
                    itemInfo[id].SetActive(true);
            }
        }
    }

    private void ToggleAllSlots(bool enable)
    {
        for (int i = 0; i < itemImages.Length; i++)
        {
            var slot = itemImages[i];
            if (slot == null) continue;

            if (slot.transform.childCount > 0)
            {
                var icon = slot.transform.GetChild(0).gameObject;
                if (icon != null) icon.SetActive(enable);
            }
        }
    }

    /// <summary>
    /// �ھڹD��W�ٸ��J�����ϥܡ]�ݩ�b Resources/Icons �ؿ��^
    /// </summary>
    private Sprite LoadIconSpriteForItem(string itemName)
    {
        // �����D��W�ٻP�Ϥ��ɦW
        switch (itemName)
        {
            case "�ѥ�":
                return Resources.Load<Sprite>("Icons/book");
            case "�H��":
                return Resources.Load<Sprite>("Icons/letter");
            case "����":
                return Resources.Load<Sprite>("Icons/doll");
            // �i�~���X�R
            default:
                return null;
        }
    }
}
