using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Language))]
public class LanguageEditor : Editor
{
    SerializedProperty Data;
    private void OnEnable()
    {
        Data = serializedObject.FindProperty("Data");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUILayout.BeginVertical("Box");

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Vocabulary", GUILayout.Width(120)))
        {
            for (int i = 0; i < Data.arraySize; i++)
            {
                SerializedProperty data = Data.GetArrayElementAtIndex(i);
                SerializedProperty vocabulary = data.FindPropertyRelative("vocabulary");
                vocabulary.arraySize += 1;
            }
        }
        GUILayout.EndHorizontal();

        if (Data.arraySize > 0)
        {
            SerializedProperty data = Data.GetArrayElementAtIndex(0);
            SerializedProperty vocabulary = data.FindPropertyRelative("vocabulary");

            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            for (int i = 0; i < Data.arraySize; i++)
            {
                if (i == 0) GUILayout.Label("Eng", EditorStyles.largeLabel, GUILayout.Height(20));
                else if (i == 1) GUILayout.Label("Vns", EditorStyles.largeLabel, GUILayout.Height(20));
                else if (i == 2) GUILayout.Label("Ind", EditorStyles.largeLabel, GUILayout.Height(20));
                else if (i > 2) GUILayout.Label("New", EditorStyles.largeLabel, GUILayout.Height(20));
            }
            GUILayout.Label("Del", EditorStyles.largeLabel, GUILayout.Width(30), GUILayout.Height(20));
            GUILayout.EndHorizontal();

            for (int i = 0; i < vocabulary.arraySize; i++)
            {
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                for (int n = 0; n < Data.arraySize; n++)
                {
                    SerializedProperty dataCreate = Data.GetArrayElementAtIndex(n);
                    SerializedProperty Vocabulary = dataCreate.FindPropertyRelative("vocabulary");
                    SerializedProperty VocabularyCreate = Vocabulary.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(VocabularyCreate, GUIContent.none, GUILayout.Height(20));
                }
                if (GUILayout.Button("Del", GUILayout.Width(40), GUILayout.Height(20)))
                {
                    if (EditorUtility.DisplayDialog("Warning!", "Are you sure you want to delete the Vocabulary?", "Yes", "No"))
                    {
                        for (int n = 0; n < Data.arraySize; n++)
                        {
                            SerializedProperty dataDel = Data.GetArrayElementAtIndex(n);
                            SerializedProperty vocabularyDel = dataDel.FindPropertyRelative("vocabulary");
                            vocabularyDel.DeleteArrayElementAtIndex(i);
                        }
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            GUILayout.BeginHorizontal();
            for (int i = 0; i < Data.arraySize; i++)
            {
                if (GUILayout.Button("Delete"))
                {
                    if (EditorUtility.DisplayDialog("Warning!", "Are you sure you want to delete the Language?", "Yes", "No"))
                    {
                        Data.DeleteArrayElementAtIndex(i);
                        continue;
                    }
                }
            }
            if (GUILayout.Button("New", GUILayout.Width(40), GUILayout.Height(20))) Data.arraySize += 1;
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }

        GUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }
}
