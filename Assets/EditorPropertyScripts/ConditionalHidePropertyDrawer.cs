using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;


[CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
public class ConditionalHidePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // get the attribute data 
        ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;

        // enabled / disable the property 
        bool enabled = GetConditionalHideAttributeResult(condHAtt, property);
        bool wasEnabled = GUI.enabled;

        // check if we should draw the property
        if (!condHAtt.HideInInspector || enabled)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }

        // Ensure that the next property that is being drawn uses the correct settings 
        GUI.enabled = wasEnabled;

    }

    // purpose: check if the property should be enabled (bas ed on if sourcePropertyValue has a condition path or not)
    private bool GetConditionalHideAttributeResult(ConditionalHideAttribute condHAtt, SerializedProperty property)
    {
        bool enabled = true;

        string propertyPath = property.propertyPath;
        string conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField);

        SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

        if (sourcePropertyValue != null)
        {
            enabled = sourcePropertyValue.boolValue;
        }
        else
        {
            Debug.LogWarning("Attempting to use a ConditonalHideAttribute but no matching sourcePropertyValue found in object: " + condHAtt.ConditionalSourceField);
         }

        return enabled;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

        if (!condHAtt.HideInInspector || enabled )
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
        else
        {
            // the property is not being drawn
            // We want to undo the spacng added before and after the property
            return -EditorGUIUtility.standardVerticalSpacing;
        }
    }
}
