using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TagToolsWindow : EditorWindow
{
    private static string Tag;
    private static bool IncludeInactive = true;

    [MenuItem("Tools/Tag find window")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        TagToolsWindow window = (TagToolsWindow)EditorWindow.GetWindow(typeof(TagToolsWindow));
        window.Show();
    }

    private void OnGUI()
    {
        Tag = EditorGUILayout.TextField("Search Tag", Tag);
        IncludeInactive = EditorGUILayout.Toggle("Include inactive", IncludeInactive);

        if (GUILayout.Button("Find In Assets"))
        {
            FindTagInAssets();
        }

        if (GUILayout.Button("Find In Scenes"))
        {
            FindTagInSceneObjects();
        }
    }

    private void FindTagInAssets()
    {
        var guids = AssetDatabase.FindAssets("t:GameObject");

        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (asset)
            {
                var gameObjects = asset.GetComponents<Transform>();
                foreach (var gameObject in gameObjects)
                {
                    if (gameObject.tag.Equals(Tag))
                    {
                        Debug.Log($"Find Asset {asset.name} GameObject {gameObject.name} with {Tag} tag in path:{path}", asset);
                    }
                }

            }
        }
    }

    private void FindTagInSceneObjects()
    {
        var guids = AssetDatabase.FindAssets("t:Scene");

        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var scene = EditorSceneManager.GetSceneByPath(path);
            EditorSceneManager.OpenScene(path);

            var sceneGameObjects = FindObjectsOfType<GameObject>(IncludeInactive);

            foreach (var sceneGameObject in sceneGameObjects)
            {
                if (sceneGameObject.tag.Equals(Tag))
                {
                    Debug.Log($"Find Scene gameObject {sceneGameObject.name} with {Tag} tag in scene:{path}");
                }
            }
        }
    }
}