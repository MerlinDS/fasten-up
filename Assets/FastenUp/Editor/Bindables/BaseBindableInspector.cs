using FastenUp.Runtime.Binders;
using UnityEditor;
using UnityEngine;

namespace FastenUp.Editor.Bindables
{
    [CustomEditor(typeof(BaseBinder), true)]
    public class BaseBindableInspector : UnityEditor.Editor
    {
        private SerializedProperty _nameProperty;

        private void OnEnable()
        {
            _nameProperty = serializedObject.FindProperty("<Name>k__BackingField");
        }

        private void OnDisable()
        {
            _nameProperty.Dispose();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            OnNameProperty();
            DrawPropertiesExcluding(serializedObject, "m_Script", _nameProperty.name);
            serializedObject.ApplyModifiedProperties();
        }

        private void OnNameProperty()
        {
            var content = new GUIContent(_nameProperty.displayName,
                "Name used for binding. It must be the same as the name of the property in the Mediator.");

            if (EditorGUILayout.PropertyField(_nameProperty, content))
                _nameProperty.stringValue = _nameProperty.stringValue.Trim();

            if (string.IsNullOrEmpty(_nameProperty.stringValue))
                EditorGUILayout.HelpBox("This binder will be ignored! Name must be set!", MessageType.Warning);

            if (_nameProperty.stringValue.StartsWith("#")) 
                EditorGUILayout.HelpBox("This binder will be ignored without any errors.", MessageType.Info);

        }
    }
}