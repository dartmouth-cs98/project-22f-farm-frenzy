using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnerManager))]
public class SpawnerEditor : Editor
{
    SpawnerManager spawner;

    bool showSpawner;
    bool prefabsEditMode;
    bool editorSettingsMode;

    string l1Space = "    ";
    string l2Space = "       ";
    string l3Space = "            ";


    [MenuItem("MSS/Create Spawner Manager",false, 1)]
    static void Init()
    {
        GameObject newManager = new GameObject("SpawnerManager");
        newManager.AddComponent<SpawnerManager>();

        GameObject container = new GameObject("[Spawners]");
        container.transform.parent = newManager.transform;

    }

    public override void OnInspectorGUI(){
        spawner = (SpawnerManager)target;

        if(spawner != null){
            if(prefabsEditMode == false && editorSettingsMode == false){
                EditorGUILayout.Space();
                DisplaySettings();
                DisplayDebug();
            }

            if(editorSettingsMode == true){
                DisplayEditorSettings();
            }

            if(prefabsEditMode == true){
                DisplayPrefabsDatabase();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

    void DisplaySettings(){
        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();

        if(GUILayout.Button("Create Spawner")){
            spawner.AddSpawner();
        }

        EditorGUILayout.LabelField("");

        if(GUILayout.Button("Editor Settings")){
            editorSettingsMode = true;
        }

        GUILayout.EndHorizontal();
        EditorGUILayout.Space();
        if(spawner.storedSpawnerID != "" && spawner.storedSpawnerID != null){
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Clipboard: " + spawner.storedSpawnerID, EditorStyles.boldLabel);
            if(GUILayout.Button("Clear")){
                spawner.ClearClipboard();
            }
            GUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }

        for(int i = 0; i < spawner.spawnPositions.Length -1; i++){
                EditorGUILayout.BeginHorizontal();
                spawner.showInInspector[i] = EditorGUILayout.Toggle("[+] Spawner " + i.ToString(),spawner.showInInspector[i]);
                if(GUILayout.Button("Delete")){
                    spawner.Remove(i);
                }
                EditorGUILayout.EndHorizontal();
                if(spawner.showInInspector[i] == true){
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    if(GUILayout.Button("Copy Settings")){
                        spawner.Copy(i);
                    }
                    EditorGUILayout.Space();
                    if(spawner.storedSpawnerID != ""){
                        if(GUILayout.Button("Paste Settings")){
                            spawner.Paste(i);
                        }
                    }else{
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    spawner.isActive[i] = EditorGUILayout.Toggle("Active",spawner.isActive[i]);
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    spawner.descriptions[i] = EditorGUILayout.TextField("Description: ", spawner.descriptions[i]);
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    var spawnPositionsProperty = serializedObject.FindProperty("spawnPositions");
                    var spawnPosition = spawnPositionsProperty.GetArrayElementAtIndex(i);
                    spawnPosition.objectReferenceValue = EditorGUILayout.ObjectField("Spawn Position",spawnPosition.objectReferenceValue, typeof(Transform), true);
                    EditorGUILayout.Space();
                    if(spawner.usePrefabsDatabase[i] == false){
                        var prefabsToSpawnProperty = serializedObject.FindProperty("prefabsToSpawn");
                        var prefabToSpawn = prefabsToSpawnProperty.GetArrayElementAtIndex(i);
                        prefabToSpawn.objectReferenceValue = EditorGUILayout.ObjectField("Prefab To Spawn",prefabToSpawn.objectReferenceValue, typeof(GameObject), true);
                    }else{
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Prefabs Tag");
                        spawner.selectedTagIndex[i] = EditorGUILayout.Popup(spawner.selectedTagIndex[i],spawner.prefabTags);
                        EditorUtility.SetDirty(spawner);
                        if(GUILayout.Button("Edit")){
                            prefabsEditMode = true;
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    
                    spawner.usePrefabsDatabase[i] = EditorGUILayout.Toggle(l1Space+"Use Database",spawner.usePrefabsDatabase[i]);
                    EditorGUILayout.Space();
                    if(spawner.usePrefabsDatabase[i] == true){
                        spawner.useRandomOrder[i] = EditorGUILayout.Toggle(l2Space+"Randomize Order",spawner.useRandomOrder[i]);
                    }
                    EditorGUILayout.Space();
                    spawner.waitForStartCommand[i] = EditorGUILayout.Toggle("Wait For Start Command",spawner.waitForStartCommand[i]);
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("- Start Delay -");
                    spawner.startSpawnDelays[i] = EditorGUILayout.Slider(spawner.startSpawnDelays[i], 0f, 200f);
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("- Spawn Delay -");
                    if(spawner.useRandomDelay[i] == false){
                        spawner.spawnDelays[i] = EditorGUILayout.Slider(spawner.spawnDelays[i], 0f, 200f);
                    }else{
                        EditorGUILayout.MinMaxSlider(ref spawner.minSpawnDelay[i],ref spawner.maxSpawnDelay[i], 0.02f, 200f);
                        spawner.minSpawnDelay[i] = EditorGUILayout.DelayedFloatField("Min",spawner.minSpawnDelay[i]);
                        spawner.maxSpawnDelay[i] = EditorGUILayout.DelayedFloatField("Max",spawner.maxSpawnDelay[i]);
                    } 
                    spawner.useRandomDelay[i] = EditorGUILayout.Toggle("Randomize",spawner.useRandomDelay[i]);
                    EditorGUILayout.Space();

                    spawner.advancedSettings[i] = EditorGUILayout.Toggle("+ Advanced Settings",spawner.advancedSettings[i]);
                    EditorGUILayout.Space();
                    if(spawner.advancedSettings[i] == true){
                        EditorGUILayout.LabelField(l1Space + "Prefabs spawned: "+spawner.spawnCounter[i]);
                        EditorGUILayout.Space();
                        EditorGUILayout.BeginHorizontal();
                        spawner.actionAt[i] = EditorGUILayout.Toggle(l1Space + "Action At",spawner.actionAt[i]);
                        if(spawner.actionAt[i] == true){
                            spawner.value[i] = EditorGUILayout.IntField("", spawner.value[i]);
                        }
                        EditorGUILayout.EndHorizontal();
                        if(spawner.actionAt[i] == true){
                            spawner.stop[i] = EditorGUILayout.Toggle(l2Space+"Stop",spawner.stop[i]);
                            spawner.sendMessage[i] = EditorGUILayout.Toggle(l2Space + "Send message",spawner.sendMessage[i]);
                            if(spawner.sendMessage[i] == true){
                                spawner.messages[i] = EditorGUILayout.TextField(l3Space + "Message", spawner.messages[i]);
                                var receiverProperty = serializedObject.FindProperty("receivers");
                                var messageReceiver = receiverProperty.GetArrayElementAtIndex(i);
                                messageReceiver.objectReferenceValue = EditorGUILayout.ObjectField(l3Space + "Receiver",messageReceiver.objectReferenceValue, typeof(GameObject), true);
                                spawner.useIndexParameter[i] = EditorGUILayout.Toggle(l3Space + "Use Index Parameter", spawner.useIndexParameter[i]);
                            }
                            EditorGUILayout.Space();
                        } 
                        EditorGUILayout.Space();                 
                        spawner.destroyPrefabs[i] = EditorGUILayout.Toggle(l1Space + "Destroy Prefabs",spawner.destroyPrefabs[i]);
                        if(spawner.destroyPrefabs[i] == true){
                            spawner.timeToLive[i] = EditorGUILayout.FloatField(l2Space + "Time To live",spawner.timeToLive[i]);
                        }
                        EditorGUILayout.Space();
                        spawner.countDestroyed[i] =  EditorGUILayout.Toggle(l1Space+"Count Destroyed",spawner.countDestroyed[i]);
                            if(spawner.countDestroyed[i] == true){
                                EditorGUILayout.Space();
                                EditorGUILayout.LabelField(l2Space + "Prefabs Remaining: "+ spawner.prefabsRemaining[i]);
                                EditorGUILayout.Space();
                                if(spawner.stop[i] == true){
                                    EditorGUILayout.BeginHorizontal();
                                    spawner.continueSpawningAt[i] = EditorGUILayout.Toggle(l2Space + "Continue Spawning At",spawner.continueSpawningAt[i]);
                                    if(spawner.continueSpawningAt[i] == true){
                                        spawner.destroyedValue[i] = EditorGUILayout.IntField("", spawner.destroyedValue[i]);
                                    }
                                    EditorGUILayout.EndHorizontal();
                                }
                            }
                        EditorGUILayout.Space();
                    }

                spawner.offsetSettings[i] = EditorGUILayout.Toggle("+ Offset Settings",spawner.offsetSettings[i]);
                if(spawner.offsetSettings[i] == true){
                    EditorGUILayout.LabelField("- Offset -");
                    if(spawner.useRandomOffset[i] == true){
                        spawner.minOffsets[i] = EditorGUILayout.Vector3Field("Min Offset",spawner.minOffsets[i]);
                        spawner.maxOffsets[i] = EditorGUILayout.Vector3Field("Max Offset",spawner.maxOffsets[i]);
                    }else{
                        spawner.spawnOffsets[i] = EditorGUILayout.Vector3Field("",spawner.spawnOffsets[i]);
                        }

                        spawner.useRandomOffset[i] = EditorGUILayout.Toggle("Randomize",spawner.useRandomOffset[i]);
                        EditorGUILayout.Space();
                        EditorGUILayout.LabelField("- Rotation -");
                        
                        if(spawner.useRandomRotation[i] == true){
                            spawner.minRotation[i] = EditorGUILayout.Vector3Field("Min Rotation",spawner.minRotation[i]);
                            spawner.maxRotation[i] = EditorGUILayout.Vector3Field("Max Rotation",spawner.maxRotation[i]);
                        }else{
                            spawner.spawnRotations[i] = EditorGUILayout.Vector3Field("",spawner.spawnRotations[i]);
                        }
                        spawner.useRandomRotation[i] = EditorGUILayout.Toggle("Randomize",spawner.useRandomRotation[i]);
                        }
                EditorGUILayout.Space();
            }
        }

        EditorGUILayout.Space();
        if(GUILayout.Button("Delete All")){
            spawner.RemoveAll();
        }
        
    }

    void DisplayPrefabsDatabase(){
        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Go Back")){
            prefabsEditMode = false;
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("- Tags -", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Add Tag")){
            spawner.AddTag();
        }

        if(GUILayout.Button("Delete All")){
            spawner.RemoveAllTags();
        }
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();
        for(int i = 0; i < spawner.prefabTags.Length; i++){
            GUILayout.BeginHorizontal();
            spawner.prefabTags[i] = EditorGUILayout.TextField(spawner.prefabTags[i]);
            if(GUILayout.Button("Delete")){
                spawner.RemoveTag(i);
            }
            GUILayout.EndHorizontal();
        }
        
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("- Prefabs -", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Add Prefab")){
            spawner.AddPrefab();
        }

        if(GUILayout.Button("Delete All")){
            spawner.RemoveAllPrefabs();
        }
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();
        for (int i = 0; i < spawner.databasePrefabs.Length -1; i++){
            GUILayout.BeginHorizontal();
            var databasePrefabsProperty = serializedObject.FindProperty("databasePrefabs");
            var databasePrefabToSpawn = databasePrefabsProperty.GetArrayElementAtIndex(i);
            databasePrefabToSpawn.objectReferenceValue = EditorGUILayout.ObjectField("",databasePrefabToSpawn.objectReferenceValue, typeof(GameObject), true);
            spawner.tagIndex[i] = EditorGUILayout.Popup(spawner.tagIndex[i],spawner.prefabTags);
            if(GUILayout.Button("Delete")){
                spawner.RemovePrefab(i);
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
    }

    void DisplayEditorSettings(){
        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Go Back")){
            editorSettingsMode = false;
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Editor Settings", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        spawner.activeSpawnerColor = EditorGUILayout.ColorField("Active Spawner Color",spawner.activeSpawnerColor);
        spawner.inactiveSpawnerColor = EditorGUILayout.ColorField("Inactive Spawner Color",spawner.inactiveSpawnerColor);
    }

    void Edit(int index){
        prefabsEditMode = true;
    }

    void DisplayDebug(){
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("debugMode"));
        if(GUILayout.Button("Equalize Variables")){
            spawner.EqualizeVariables();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
    }
}

