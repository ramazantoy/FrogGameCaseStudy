using System;

#if UNITY_EDITOR
namespace Editor
{
    using GridSystem;
    using UnityEngine;
    using UnityEditor;

    /// <summary>
    /// Custom editor for the GridBuilder class, adding buttons for building, removing tiles, and saving level data in the Unity editor.
    /// </summary>
    [CustomEditor(typeof(GridBuilder), true)]
    public class GridBuilderEditor : Editor
    {
        [Obsolete("Obsolete")]
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Build"))
            {
                var gridBuilder = (GridBuilder)target;
                gridBuilder.BuildGrid();
            }

            if (GUILayout.Button("RemoveTiles"))
            {
                var gridBuilder = (GridBuilder)target;
                gridBuilder.RemoveTiles();
            }
            if (GUILayout.Button("SaveLevel"))
            {
                var gridBuilder = (GridBuilder)target;
                gridBuilder.SaveLevelDataOnEditor();
            }
        }
    }
}
#endif