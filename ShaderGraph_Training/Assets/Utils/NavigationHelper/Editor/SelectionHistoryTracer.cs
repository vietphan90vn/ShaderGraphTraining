using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;

/// <summary>
/// This class using for tracker selection to object or game object
/// </summary>
[InitializeOnLoad]
public static class SelectionHistoryTracer
{
    /// <summary>
    /// Max number of navigation backward and forward
    /// </summary>
    public const int MaxLevel = 10;

    /// <summary>
    /// Container store backward objects
    /// </summary>
    public static List<Object> BackwardCollection = new List<Object>(MaxLevel);

    /// <summary>
    /// Container store forward objects
    /// </summary>
    public static List<Object> ForwardCollection = new List<Object>(MaxLevel);

    public static bool DisableNextTrack = false;

    /// <summary>
    /// Constructor to track selection change
    /// </summary>
    static SelectionHistoryTracer()
    {
        Selection.selectionChanged += TrackSelectionChange;
    }

    /// <summary>
    /// Process selection change
    /// </summary>
    static void TrackSelectionChange()
    {
        if (DisableNextTrack)
        {
            DisableNextTrack = false;
            return;
        }

        // Clear forward collection when user make a selection
        if (ForwardCollection.Count > 0)
        {
            BackwardCollection.AddRange(ForwardCollection);
            ForwardCollection.Clear();
        }

        if (Selection.activeObject != null &&
            ((BackwardCollection.Count > 0 &&
            BackwardCollection[BackwardCollection.Count - 1] != Selection.activeObject) ||
            BackwardCollection.Count == 0))
        {
            if (BackwardCollection.Count == MaxLevel)
            {
                BackwardCollection.RemoveAt(0);
            }

            BackwardCollection.Add(Selection.activeObject);
        }
    }
}
