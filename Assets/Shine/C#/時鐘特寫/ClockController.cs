using UnityEngine;

public class ClockController : MonoBehaviour
{
    [Header("旋轉單位（度）")]
    public float minuteStepDegree = 6f;
    public float hourStepDegree = 30f;

    HandSelectable _selected;

    public void Select(HandSelectable target)
    {
        if (_selected == target) return;

        if (_selected != null)
            _selected.SetSelected(false);

        _selected = target;
        if (_selected != null)
            _selected.SetSelected(true);
    }

    public void RotateLeft()
    {
        RotateSelected(-GetStep());
    }

    public void RotateRight()
    {
        RotateSelected(GetStep());
    }

    void RotateSelected(float delta)
    {
        if (_selected == null) return;
        _selected.Rotate(delta);
    }

    float GetStep()
    {
        if (_selected == null) return 0f;
        return _selected.handType == HandSelectable.HandType.Minute ? minuteStepDegree : hourStepDegree;
    }
}
