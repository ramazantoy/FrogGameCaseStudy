#if UNITY_EDITOR
namespace Editor
{
    using GridSystem;
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(GridBuilder), true)]
    public class GridBuilderEditor : Editor
    {
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