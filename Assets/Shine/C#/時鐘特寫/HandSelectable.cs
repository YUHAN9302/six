using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandSelectable : MonoBehaviour
{
    public enum HandType { Hour, Minute }
    public HandType handType;

    [Header("�~�[�]�w")]
    public SpriteRenderer handRenderer;
    public Color normalColor = Color.white;
    public Color selectedColor = Color.yellow;

    [Header("����Ѧ�")]
    public ClockController controller;

    bool _selected = false;

    void Reset()
    {
        handRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        // �ƹ��I���ƥ�]�ݦ� Collider2D�^
        controller.Select(this);
    }

    public void SetSelected(bool on)
    {
        _selected = on;
        if (handRenderer != null)
            handRenderer.color = on ? selectedColor : normalColor;
    }

    public void Rotate(float deltaDegree)
    {
        transform.Rotate(0, 0, deltaDegree);
    }
}
