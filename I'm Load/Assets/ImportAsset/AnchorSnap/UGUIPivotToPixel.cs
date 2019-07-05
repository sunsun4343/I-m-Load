using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;

public class UGUIPivotToPixel : Editor
{
    [MenuItem("GameObject/Anchor Snap/Pivot Reset %'", false, 0)]
    private static void PivotResetGameObject()
    {
        PivotReset(Selection.activeGameObject);
    }

    private static void PivotReset(GameObject gameObject)
    {
        RectTransform recTransform = null;
        if (gameObject.GetComponent<RectTransform>() != null)
        {
            recTransform = gameObject.GetComponent<RectTransform>();
        }
        else
        {
            return;
        }

        Undo.RecordObject(recTransform, "Pivot Reset");

        recTransform.anchorMin = new Vector2(0, 1);
        recTransform.anchorMax = new Vector2(0, 1);
        recTransform.pivot = new Vector2(0,1);

    }
}
#endif