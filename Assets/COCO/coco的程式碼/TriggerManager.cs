using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public static TriggerManager Instance;
    private HashSet<string> clickedObjects = new HashSet<string>();
    private HashSet<string> allClickableObjects = new HashSet<string>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterObject(string objectName)
    {
        allClickableObjects.Add(objectName);
    }

    public void RecordClick(string objectName)
    {
        clickedObjects.Add(objectName);
    }

    public bool AreAllClicked()
    {
        return clickedObjects.Count == allClickableObjects.Count;
    }
}
