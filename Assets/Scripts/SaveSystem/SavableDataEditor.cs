using UnityEditor;
using UnityEngine;

namespace SaveSystem
{
#if UNITY_EDITOR
    [CustomEditor(typeof(SavableData),true)]
    public class SavableDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set ID"))
            {
                SavableData savableData = (SavableData)target;
                savableData.SetID();
                EditorUtility.SetDirty(savableData); 
            }
        }
    }
#endif
   
}