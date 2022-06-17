/*using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(CustomButton), true)] 
[CanEditMultipleObjects]
public class CustomButtonEditor : ButtonEditor
{
   public override void OnInspectorGUI()
   {
      EditorGUILayout.PropertyField(serializedObject.FindProperty("_normalState"));
      EditorGUILayout.PropertyField(serializedObject.FindProperty("_pressedState"));
      serializedObject.ApplyModifiedProperties();
      
      base.OnInspectorGUI();
   }
}*/