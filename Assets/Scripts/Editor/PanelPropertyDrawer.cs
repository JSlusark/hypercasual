using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ScreenManager.Panel))]
public class PanelPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        
        // 1. MASK HOVER/SELECTION COLOR
        // We draw a solid, opaque box that matches the Inspector background.
        // This "paints over" the default Unity hover tint.
        Color bgColor = EditorGUIUtility.isProSkin 
            ? new Color(0.22f, 0.22f, 0.22f, 1f)  // Dark Theme Gray
            : new Color(0.8f,  0.8f,  0.8f,  1f); // Light Theme Gray
    
        EditorGUI.DrawRect(position, bgColor);

        // 1. DATA PREP
        SerializedProperty nameProp = property.FindPropertyRelative("name");
        SerializedProperty layerProp = property.FindPropertyRelative("layer");

        string titleText = nameProp != null ? nameProp.enumDisplayNames[nameProp.enumValueIndex] : label.text;
        string tagText = layerProp != null ? layerProp.enumDisplayNames[layerProp.enumValueIndex] : "No Layer";

        // 2. DRAW HEADER (Foldout + Tag)
        Rect headerRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        
        // Foldout
        property.isExpanded = EditorGUI.Foldout(headerRect, property.isExpanded, titleText, true);

        // Draw Tag on the right
        DrawTag(headerRect, tagText, layerProp != null ? layerProp.enumValueIndex : -1);

        // 3. DYNAMICALLY DRAW FIELDS
        if (property.isExpanded)
        {
            EditorGUI.indentLevel++;
            Rect fieldRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);

            SerializedProperty iterator = property.Copy();
            SerializedProperty endProperty = iterator.GetEndProperty();

            bool enterChildren = true;
            while (iterator.NextVisible(enterChildren) && !SerializedProperty.EqualContents(iterator, endProperty))
            {
                enterChildren = false;
                float height = EditorGUI.GetPropertyHeight(iterator, true);
                fieldRect.height = height;

                EditorGUI.PropertyField(fieldRect, iterator, true);
                fieldRect.y += height + EditorGUIUtility.standardVerticalSpacing;
            }
            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }

    private void DrawTag(Rect headerRect, string text, int layerIndex)
    {
        // Style for the tag
        GUIStyle tagStyle = new GUIStyle(EditorStyles.miniButton);
        // tagStyle.name = "LayerTag";
        tagStyle.fontSize = 9;
        tagStyle.alignment = TextAnchor.MiddleCenter;
        tagStyle.fixedHeight = 16;
        
        // Calculate size based on text
        Vector2 textSize = tagStyle.CalcSize(new GUIContent(text));
        float tagWidth = textSize.x + 10;
        Rect tagRect = new Rect(headerRect.xMax - tagWidth, headerRect.y, tagWidth, 16);

        // Save original GUI color
        Color oldColor = GUI.backgroundColor;
        GUI.backgroundColor = GetColorForLayer(layerIndex);
        
        GUI.Box(tagRect, text, tagStyle);
        
        // Restore color
        GUI.backgroundColor = oldColor;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (!property.isExpanded) return EditorGUIUtility.singleLineHeight;

        float totalHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        SerializedProperty iterator = property.Copy();
        SerializedProperty endProperty = iterator.GetEndProperty();

        bool enterChildren = true;
        while (iterator.NextVisible(enterChildren) && !SerializedProperty.EqualContents(iterator, endProperty))
        {
            enterChildren = false;
            totalHeight += EditorGUI.GetPropertyHeight(iterator, true) + EditorGUIUtility.standardVerticalSpacing;
        }
        return totalHeight;
    }

    private Color GetColorForLayer(int index)
    {
        switch (index)
        {
            case 0:  return new Color(0f, 0.7f, 0f);   // Main Menu
            case 1:  return new Color(0.2f,   0.1f, 0.7f); // Character Selection layer
            case 2:  return new Color(0.2f,   0.7f, 0.7f); // Character Profile layer
            case 3:  return new Color(0.5f, 0f,   0f); // Play layer
            default: return new Color(0f,     0f,   0f);
        }
    }
}