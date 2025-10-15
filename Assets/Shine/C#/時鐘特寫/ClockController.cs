using UnityEngine;
using UnityEngine.UI;

public class ClockController : MonoBehaviour
{
    [Header("旋轉單位（度）")]
    public float minuteStepDegree = 6f; // 每分鐘6度
    public float hourStepDegree = 30f;  // 每小時30度

    private HandSelectable _selected;
    static public int hours, minutes;
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
        UpdateTimeDisplay();
    }

    public void RotateRight()
    {
        RotateSelected(GetStep());
        UpdateTimeDisplay();
    }

    void RotateSelected(float delta)
    {
        if (_selected == null) return;
        _selected.Rotate(delta);
    }

    float GetStep()
    {
        if (_selected == null) return 0f;

        if (_selected.handType == HandSelectable.HandType.Hour)
            return hourStepDegree; // 30度 = 1小時
        else if (_selected.handType == HandSelectable.HandType.Minute)
            return minuteStepDegree; // 6度 = 1分鐘
        else
            return 0f;
    }

    // ✅ 將角度即時轉換成「小時」或「分鐘」
    void UpdateTimeDisplay()
    {
        if (_selected == null) return;

        float zRotation = _selected.transform.localEulerAngles.z;
        // Unity Z軸角度是0~360，順時針反轉

        if (_selected.handType == HandSelectable.HandType.Hour)
        {
            float normalizedAngle = (360f - zRotation) % 360f;

            // 每30度 = 1小時
            float hourValue = normalizedAngle / 30f;
             hours = Mathf.FloorToInt(hourValue) % 12;
           
            Debug.Log($"目前時針角度：{normalizedAngle:F1}° → {hours} 時");
        }
        else if (_selected.handType == HandSelectable.HandType.Minute)
        {

            float normalizedAngle = ((zRotation - 180f) + 360f) % 360f;

            // 每 6 度 = 1 分鐘
            float minuteValue = normalizedAngle / 6f;

            // 四捨五入並轉換為 1~60 的範圍
             minutes = 60-Mathf.RoundToInt(minuteValue);
            if (minutes == 0) minutes = 1; // 讓 0 變成 1 分起算
            if (minutes > 60) minutes = 60;


            Debug.Log(minutes);

        }
    }
}
