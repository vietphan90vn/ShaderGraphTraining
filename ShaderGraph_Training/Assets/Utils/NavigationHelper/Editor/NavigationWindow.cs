using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;

public class NavigationWindow : EditorWindow
{
    [MenuItem("Edit/Navigation/NavigationBackward %#-")]
    static void NavitagionBackward()
    {
        // Check at least 2 object
        if (SelectionHistoryTracer.BackwardCollection.Count < 2)
        {
            return;
        }

        // Choose near to last because, last is current selection
        int lastID = SelectionHistoryTracer.BackwardCollection.Count - 1;
        int nearTolastId = lastID - 1;
        var nearToLastSeletion = SelectionHistoryTracer.BackwardCollection[nearTolastId];

        ExecuteObjectAndWindow(nearToLastSeletion, ref SelectionHistoryTracer.DisableNextTrack);

        // Add to Forward and remove from backward
        SelectionHistoryTracer.ForwardCollection.Add(SelectionHistoryTracer.BackwardCollection[lastID]);
        SelectionHistoryTracer.BackwardCollection.RemoveAt(lastID);
    }

    [MenuItem("Edit/Navigation/NavigationForward %#=")]
    static void NavigationForward()
    {
        // Check at least 2 object
        if (SelectionHistoryTracer.ForwardCollection.Count < 1)
        {
            return;
        }

        // Choose near to last because, last is current selection
        int lastId = SelectionHistoryTracer.ForwardCollection.Count - 1;
        var lastSelection = SelectionHistoryTracer.ForwardCollection[lastId];

        ExecuteObjectAndWindow(lastSelection, ref SelectionHistoryTracer.DisableNextTrack);

        // Add to forward and remove from forward
        SelectionHistoryTracer.BackwardCollection.Add(SelectionHistoryTracer.ForwardCollection[lastId]);
        SelectionHistoryTracer.ForwardCollection.RemoveAt(lastId);
    }

    /// <summary>
    /// This method to execute tracked object and window of it
    /// </summary>
    /// <param name="target">object already tracked</param>
    private static void ExecuteObjectAndWindow(Object target, ref bool disableNextTrack)
    {
        if (target is GameObject)
        {
            EditorApplication.ExecuteMenuItem("Window/General/Hierarchy");
        }
        else if (target is AnimatorState)
        {
            EditorApplication.ExecuteMenuItem("Window/Animation/Animator");
        }
        else
        {
            EditorApplication.ExecuteMenuItem("Window/General/Project");
            EditorUtility.FocusProjectWindow();
        }

        // Disable next track, must call before change Seletion.activeObject
        disableNextTrack = true;
        Selection.activeObject = target;
        EditorGUIUtility.PingObject(target);
    }
}
