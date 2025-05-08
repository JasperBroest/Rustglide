using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomPropertyDrawer(typeof(ProTubeSettings))]
public class ProTubeSettingsDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Draw the label for the field
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Get all ProTubeSettings assets in the project
        var settingsAssets = AssetDatabase.FindAssets("t:ProTubeSettings")
            .Select(guid => AssetDatabase.LoadAssetAtPath<ProTubeSettings>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(asset => asset != null)
            .ToList();

        // Get the names of the assets for the dropdown
        var options = settingsAssets.Select(asset => asset.name).ToList();
        options.Insert(0, "None"); // Add a "None" option at the top

        // Get the currently selected asset
        var currentIndex = property.objectReferenceValue != null
            ? settingsAssets.IndexOf(property.objectReferenceValue as ProTubeSettings) + 1
            : 0;

        // Draw the dropdown
        int selectedIndex = EditorGUI.Popup(position, currentIndex, options.ToArray());

        // Update the property value based on the selected index
        if (selectedIndex != currentIndex) // Only update if the selection changed
        {
            property.serializedObject.Update(); // Update the serialized object

            if (selectedIndex == 0)
            {
                property.objectReferenceValue = null; // None selected
            }
            else
            {
                property.objectReferenceValue = settingsAssets[selectedIndex - 1];
            }

            property.serializedObject.ApplyModifiedProperties(); // Apply the changes
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}