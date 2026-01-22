#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Fluty.Inspector;

namespace Fluty.Inspector.Editor
{
    [CustomPropertyDrawer(typeof(Fluty_ReadOnlyAttribute))]
    public class Fluty_ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}
#endif