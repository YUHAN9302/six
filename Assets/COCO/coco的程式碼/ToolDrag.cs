using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ToolDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 startPos;

    public string toolName; // "�w", "�_�u", "�w�u", "�s��", "����"


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        startPos = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false; // ���U�誫��i����
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // �즲������q�� ToolManager �P�_
        if (ToolManager.Instance != null)
            ToolManager.Instance.CheckDrop(this);
        else
            ResetPosition();
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = startPos;
    }

    public RectTransform GetRectTransform()
    {
        return rectTransform;
    }
}
