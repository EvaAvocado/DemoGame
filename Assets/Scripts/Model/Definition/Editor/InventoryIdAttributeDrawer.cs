﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(InventoryIdAttribute))]
public class InventoryIdAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var defs = DefsFacade.i.items.ItemsForEditor;
        var ids = new List<string>();

        foreach (var itemDefs in defs)
        {
            ids.Add(itemDefs.Id);
        }

        var index = ids.IndexOf(property.stringValue);

        index = Mathf.Max(EditorGUI.Popup(position, property.displayName, index, ids.ToArray()), 0);
        property.stringValue = ids[index];
    }
}