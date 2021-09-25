using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(NPCCodeDescriptionAttribute))]
public class NPCCodeDescriptionDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Double the standard property height so there's room for the item code description
        return EditorGUI.GetPropertyHeight(property) * 2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        if (property.propertyType == SerializedPropertyType.Integer)
        {
            EditorGUI.BeginChangeCheck(); // Check for changed values

            // Draw npc code
            var newValue = EditorGUI.IntField(new Rect(position.x, position.y, position.width, position.height / 2), label, property.intValue);

            // Draw the npc description
            EditorGUI.LabelField(new Rect(position.x, position.y + position.height / 2, position.width, position.height / 2), "NPC Name", GetNPCDescription(property.intValue));

            // If npc code value has changed, then update to the new value
            if (EditorGUI.EndChangeCheck())
            {
                property.intValue = newValue;
            }
        }

        EditorGUI.EndProperty();
    }

    private string GetNPCDescription(int npcCode)
    {
        SO_NPCList so_npcList;

        so_npcList = AssetDatabase.LoadAssetAtPath("Assets/Scriptable Object Assets/so_NPCList.asset", typeof(SO_NPCList)) as SO_NPCList;

        List<NPCDetails> npcDetailsList = so_npcList.npcDetails;

        NPCDetails npcDetail = npcDetailsList.Find(x => x.npcCode == npcCode);

        if (npcDetail != null)
        {
            return npcDetail.npcName;
        }
        else
        {
            return "";
        }
    }
}