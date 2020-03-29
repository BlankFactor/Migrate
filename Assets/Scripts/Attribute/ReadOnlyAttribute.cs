using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ReadOnlyAttribute : PropertyAttribute
{
    
}

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyAttributeDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string value = null;

        switch (property.propertyType) {
            case SerializedPropertyType.Integer:
                value = property.intValue.ToString();
                 break;
            case SerializedPropertyType.String:
                value = property.stringValue;
                break;
            case SerializedPropertyType.Color:
                value = property.colorValue.ToString();
                break;
            case SerializedPropertyType.Float:
                value = property.floatValue.ToString();
                break;
            case SerializedPropertyType.Vector3:
                value = property.vector3Value.ToString();
                break;
            case SerializedPropertyType.Rect:
                value = property.rectValue.ToString();
                break;
        }

        EditorGUI.LabelField(position, property.name + "\t\t\t" + value);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }
}
