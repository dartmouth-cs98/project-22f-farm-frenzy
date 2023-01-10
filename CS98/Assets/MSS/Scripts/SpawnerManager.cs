////////////////////////////////////////////////////
//                                                //
// SpawnerManager written by Daimon Creative (C)  //
//                                                //
// this script is part of the MMS - Asset Package //
//                                                //
////////////////////////////////////////////////////


//VER. 1.2
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    //Show/Hide in inspector
    public bool[] showInInspector = new bool[0];
    //Enable/Disbale the spawner
    public bool[] isActive = new bool[0];
    //Use this field to add descriptions to your spawners
    public string[] descriptions = new string[0];
    //Spawn-Positions as Transform
    public Transform[] spawnPositions = new Transform[0];
    //Prefab to spawn 
    public GameObject[] prefabsToSpawn = new GameObject[0];
    //Wait for the Reset-Function to be called before spawning
    public bool[] waitForMessage = new bool[0];
    //Wait for seconds before the first spawn
    public float[] startSpawnDelays = new float[0];
    //Delay between every spawn
    public float[] spawnDelays = new float[0];
    //Set a random delay between every spawn
    public bool[] useRandomDelay = new bool[0];
    //Minimum delay in seconds
    public float[] minSpawnDelay = new float[0];
    //Maximum delay in seconds
    public float[] maxSpawnDelay = new float[0];
    
    //How many times does the spawner actually spawn
    public int[] spawnCounter = new int[0];
    //Execute action at counter value?
    public bool[] actionAt = new bool[0];
    //Value that has to be reached to execute action
    public int[] value = new int[0];
    //Stop spawner when reaching given value?
    public bool[] stop = new bool[0];
    //Send message when reaching value?
    public bool[] sendMessage = new bool[0];
    //Receiver to send message to
    public GameObject[] receivers = new GameObject[0];
    //Message as string
    public string[] messages = new string[0];
    //Give index parameter with when sending message
    public bool[] useIndexParameter = new bool[0];

    //Destroy prefabs after spawned? -> this will add the MSSCallback-Script to the spawned object
    public bool[] destroyPrefabs = new bool[0];
    //Timer to destroy the prefabs
    public float[] timeToLive = new float[0];

    //Count destroyed prefabs -> if set true the Callback-Script will be added to the spawned prefab
    public bool[] countDestroyed = new bool[0];
    //Counter value of destroyed prefabs
    public int[] prefabsRemaining = new int[0];
    //Continue spawning when a defined amount of prefabs has been destroyed
    public bool[] continueSpawningAt = new bool[0];
    //Value that has to be reached to continue spawning
    public int[] destroyedValue = new int[0];
    

    //Wait for StartSpawner() to be called
    public bool[] waitForStartCommand = new bool[0];
    //Enable advanced settings
    public bool[] advancedSettings = new bool[0];
    //Enable offset settings
    public bool[] offsetSettings = new bool[0];
    //Additional Offset in Vector3
    public Vector3[] spawnOffsets = new Vector3[0];
    //Spawn prefabs with a random offset
    public bool[] useRandomOffset = new bool[0];
    //Minimum Offset in Vector3
    public Vector3[] minOffsets = new Vector3[0];
    //Maximum Offset in Vector3
    public Vector3[] maxOffsets = new Vector3[0];
    //Additional rotation to current transform
    public Vector3[] spawnRotations = new Vector3[0];
    //Spawn prefabs with random Rotation
    public bool[] useRandomRotation = new bool[0];
    //Min Rotation
    public Vector3[] minRotation = new Vector3[0];
    //Max Rotation
    public Vector3[] maxRotation = new Vector3[0];
    //Spawner GameObjects
    public GameObject[] spawnEntities = new GameObject[0];
    //Can spawn?
    public bool[] readyToSpawn = new bool[0];
    //Timer to count delay-time
    public float[] delayTimer = new float[0];
    //If set true, the spawner will use database settings to spawn prefabs
    public bool[] usePrefabsDatabase = new bool[0];
    //Use random spawn order for prefabs in database
    public bool[] useRandomOrder = new bool[0];
    
    //Database variables
    public GameObject[] databasePrefabs = new GameObject[0];
    public int[] tagIndex = new int[0];
    public string[] prefabTags = new string[1]{"Tag0"}; 
    
    public string[] selectedTags = new string[0];
    public int[] selectedTagIndex = new int[0];

    //Editor Settings variables
    public bool drawGizmos = true;
    public Color activeSpawnerColor = Color.green;
    public Color inactiveSpawnerColor = Color.red;



    //Copied (Stored) variables
    public string storedSpawnerID;
    public int storedIndex;


    private bool canCount = true;
    public GameObject[] lastPrefabSpawned = new GameObject[0];
    public GameObject[] db_prefabsOfTag = new GameObject[0];
    public int[] db_lastIndex = new int[0];
    private GameObject prefabToSpawn;
    public MSSCallback callbackScript;

    //Debug
    [Tooltip("In Debug-Mode the Unity-Console will print out every single spawn with relevant data.")]
    public bool debugMode = false;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < readyToSpawn.Length -1; i++){
                readyToSpawn[i] = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate(){
        for(int i = 0; i < spawnPositions.Length -1; i++){
            if(actionAt[i] == true && spawnCounter[i] == value[i]){
                if(stop[i] == true){
                    readyToSpawn[i] = false;
                }
                
                if(sendMessage[i] == true){
                    if(receivers[i] != null){
                        Debug.Log("Sent");
                        if(useIndexParameter[i] == true){
                            receivers[i].SendMessage(messages[i], i);
                        }else{
                            receivers[i].SendMessage(messages[i]);
                        }
                    }else if(debugMode == true){
                        Debug.Log("[MSS] Receiver at spawner "+i.ToString()+" has not been assigned!");
                    }
                }
            }
            if(readyToSpawn[i] == true && isActive[i] == true){
                if(spawnDelays[i] >= 0f){
                    readyToSpawn[i] = false;
                }
                SetParameters(i);
            }
            if(continueSpawningAt[i] == true && stop[i] == true && prefabsRemaining[i] <= destroyedValue[i] && isActive[i]){
                spawnCounter[i] = prefabsRemaining[i];
                StartSpawner(i);
            }
        }

        if(canCount == true){
            for(int j = 0; j < delayTimer.Length; j++){
                if(waitForStartCommand[j] == false){
                    if(startSpawnDelays[j] > 0f){
                        startSpawnDelays[j] -= Time.fixedDeltaTime;
                    }else{
                        startSpawnDelays[j] = 0f;
                        if(delayTimer[j] > Time.fixedDeltaTime){
                            delayTimer[j] -= Time.fixedDeltaTime;
                        }else{
                        readyToSpawn[j] = true;
                        delayTimer[j] = spawnDelays[j];
                        }
                    }
                }
            }
        }
    }

    void SetParameters(int index){
        if(useRandomDelay[index] == true){
            spawnDelays[index] = Random.Range(minSpawnDelay[index], maxSpawnDelay[index]);
        }
        if(useRandomOffset[index] == true){
            float offsetX;
            float offsetY;
            float offsetZ;

            offsetX = Random.Range(minOffsets[index].x, maxOffsets[index].x);
            offsetY = Random.Range(minOffsets[index].y, maxOffsets[index].y);
            offsetZ = Random.Range(minOffsets[index].z, maxOffsets[index].z);

            spawnOffsets[index] = new Vector3(offsetX, offsetY, offsetZ);
        }

        if(useRandomRotation[index] == true){
            float rotationX;
            float rotationY;
            float rotationZ;

            rotationX = Random.Range(minRotation[index].x, maxRotation[index].x);
            rotationY = Random.Range(minRotation[index].y, maxRotation[index].y);
            rotationZ = Random.Range(minRotation[index].z, maxRotation[index].z);

            spawnRotations[index] = new Vector3(rotationX, rotationY, rotationZ);
        }

        if(usePrefabsDatabase[index] == true){
            db_prefabsOfTag = new GameObject[0];
            for(int j = 0; j < databasePrefabs.Length -1; j++){
                if(selectedTagIndex[index] == tagIndex[j]){
                    System.Array.Resize(ref db_prefabsOfTag, db_prefabsOfTag.Length +1);
                    db_prefabsOfTag[db_prefabsOfTag.Length -1] = databasePrefabs[j];
                }
            }
            if(useRandomOrder[index] == true){
                        int randomIndex = Random.Range(0, db_prefabsOfTag.Length);
                        prefabToSpawn = db_prefabsOfTag[randomIndex];
                    }else{
                        if(db_lastIndex[index] < db_prefabsOfTag.Length -1){
                            db_lastIndex[index] ++;
                        }else{
                            db_lastIndex[index] = 0;
                        }
                        prefabToSpawn = db_prefabsOfTag[db_lastIndex[index]];
                    }
                    Spawn(index);
        }else{
            if(prefabsToSpawn[index] != null){
                prefabToSpawn = prefabsToSpawn[index];
                Spawn(index);
            }else{
                if(debugMode == true){
                    Debug.Log("<color=red>[MSS] Failed to spawn prefab at " + spawnEntities[index].name + " - no prefab assigned!</color>");
                }
            }
        }
    }

    void Spawn(int index){
        if(prefabToSpawn != null){
            lastPrefabSpawned[index] = Instantiate(prefabToSpawn, spawnPositions[index].position + spawnOffsets[index], spawnPositions[index].rotation *= Quaternion.Euler(spawnRotations[index]));
            spawnCounter[index] ++;
            callbackScript = null;
            if(countDestroyed[index] == true){
                callbackScript = lastPrefabSpawned[index].AddComponent<MSSCallback>();
                callbackScript.SetSpawnerManager(this);
                callbackScript.SetIndex(index);
                prefabsRemaining[index] ++;
            }
            if(destroyPrefabs[index] == true){
                if(callbackScript == null){
                    callbackScript = lastPrefabSpawned[index].AddComponent<MSSCallback>();
                    callbackScript.SetSpawnerManager(this);
                    callbackScript.SetIndex(index);
                }
                callbackScript.SetTimer(timeToLive[index]);
            }

            if(debugMode == true){
                Debug.Log("<color=green>[MSS] Spawned "+prefabToSpawn.name + " at " + spawnEntities[index].name + " " + spawnPositions[index].position+"</color>");
            }

        }
    }

    public void StartSpawner(int index){
        if(spawnPositions.Length > index){
            waitForStartCommand[index] = false;
            isActive[index] = true;
        }
    }

    public void StopSpawner(int index){
        if(spawnPositions.Length > index){
            waitForStartCommand[index] = true;
            isActive[index] = false;
        }
    }

    public void PrefabDestroyed(int index){
        if(countDestroyed[index] == true && actionAt[index] == true){
            if(prefabsRemaining[index] - 1 <= destroyedValue[index]){
                StartSpawner(index);
            }else{
                
            }
            prefabsRemaining[index] --;
        }
    }

    public void AddSpawner(){
        int length = spawnPositions.Length;
        length ++;
        System.Array.Resize(ref showInInspector, showInInspector.Length +1);
        System.Array.Resize(ref isActive, showInInspector.Length);
        System.Array.Resize(ref descriptions, showInInspector.Length);
        System.Array.Resize(ref spawnPositions, showInInspector.Length);
        System.Array.Resize(ref prefabsToSpawn, showInInspector.Length);
        System.Array.Resize(ref readyToSpawn, showInInspector.Length);
        System.Array.Resize(ref spawnEntities, showInInspector.Length);
        System.Array.Resize(ref startSpawnDelays, showInInspector.Length);
        System.Array.Resize(ref waitForStartCommand, showInInspector.Length);
        System.Array.Resize(ref spawnDelays, showInInspector.Length);
        System.Array.Resize(ref useRandomDelay, showInInspector.Length);
        System.Array.Resize(ref minSpawnDelay, showInInspector.Length);
        System.Array.Resize(ref maxSpawnDelay, showInInspector.Length);
        System.Array.Resize(ref spawnCounter, showInInspector.Length);
        System.Array.Resize(ref offsetSettings, showInInspector.Length);
        System.Array.Resize(ref advancedSettings, showInInspector.Length);
        System.Array.Resize(ref spawnOffsets, showInInspector.Length);
        System.Array.Resize(ref useRandomOffset, showInInspector.Length);
        System.Array.Resize(ref minOffsets, showInInspector.Length);
        System.Array.Resize(ref maxOffsets, showInInspector.Length);
        System.Array.Resize(ref spawnRotations, showInInspector.Length);
        System.Array.Resize(ref useRandomRotation, showInInspector.Length);
        System.Array.Resize(ref minRotation, showInInspector.Length);
        System.Array.Resize(ref maxRotation, showInInspector.Length);
        System.Array.Resize(ref actionAt, showInInspector.Length);
        System.Array.Resize(ref value, showInInspector.Length);
        System.Array.Resize(ref stop, showInInspector.Length);
        System.Array.Resize(ref sendMessage, showInInspector.Length);
        System.Array.Resize(ref receivers, showInInspector.Length);
        System.Array.Resize(ref messages, showInInspector.Length);
        System.Array.Resize(ref useIndexParameter, showInInspector.Length);
        System.Array.Resize(ref countDestroyed, showInInspector.Length);
        System.Array.Resize(ref prefabsRemaining, showInInspector.Length);
        System.Array.Resize(ref continueSpawningAt, showInInspector.Length);
        System.Array.Resize(ref destroyedValue, showInInspector.Length);
        System.Array.Resize(ref destroyPrefabs, showInInspector.Length);
        System.Array.Resize(ref timeToLive, showInInspector.Length);
        System.Array.Resize(ref usePrefabsDatabase, showInInspector.Length);
        System.Array.Resize(ref selectedTags, showInInspector.Length);
        System.Array.Resize(ref selectedTagIndex, showInInspector.Length);
        System.Array.Resize(ref lastPrefabSpawned, showInInspector.Length);
        System.Array.Resize(ref useRandomOrder, showInInspector.Length);
        System.Array.Resize(ref db_lastIndex, showInInspector.Length);


        delayTimer = spawnDelays;
        
        if(spawnPositions.Length > 1){
            int ident = length -2; 
            spawnEntities[ident] = new GameObject("Spawner_"+ident);
            spawnEntities[ident].transform.SetParent(this.gameObject.transform.GetChild(0));
            spawnPositions[ident] = spawnEntities[ident].transform;
        }
    }

    public void RemoveAll(){
        foreach(GameObject entity in spawnEntities){
            DestroyImmediate(entity);
        }

        showInInspector = new bool[0];
        isActive = new bool[0];
        descriptions = new string[0];
        prefabsToSpawn = new GameObject[0];
        spawnEntities = new GameObject[0];
        spawnPositions = new Transform[0];
        readyToSpawn = new bool[0];
        startSpawnDelays = new float[0];
        waitForStartCommand = new bool[0];
        spawnDelays = new float[0];
        useRandomDelay = new bool[0];
        minSpawnDelay = new float[0];
        maxSpawnDelay = new float[0];
        spawnCounter = new int [0];
        offsetSettings = new bool[0];
        advancedSettings = new bool[0];
        spawnOffsets = new Vector3[0];
        useRandomOffset = new bool [0];
        minOffsets = new Vector3[0];
        maxOffsets = new Vector3[0];
        spawnRotations = new Vector3[0];
        useRandomRotation = new bool[0];
        minRotation = new Vector3[0];
        maxRotation = new Vector3[0];
        actionAt = new bool[0];
        value = new int[0];
        stop = new bool[0];
        sendMessage = new bool[0];
        receivers = new GameObject[0];
        messages = new string[0];
        useIndexParameter = new bool[0];
        countDestroyed = new bool[0];
        prefabsRemaining = new int[0];
        continueSpawningAt = new bool[0];
        destroyedValue = new int[0];
        destroyPrefabs = new bool[0];
        timeToLive = new float[0];
        usePrefabsDatabase = new bool[0];
        selectedTags = new string[0];
        selectedTagIndex = new int [0];
        lastPrefabSpawned = new GameObject[0];
        useRandomOrder = new bool[0];
        db_lastIndex = new int[0];
    }

    public void Remove(int index){
        DestroyImmediate(spawnEntities[index]);
        RemoveAt(ref showInInspector, index);
        RemoveAt(ref isActive, index);
        RemoveAt(ref descriptions, index);
        RemoveAt(ref spawnEntities, index);
        RemoveAt(ref spawnPositions, index);
        RemoveAt(ref readyToSpawn, index);
        RemoveAt(ref startSpawnDelays, index);
        RemoveAt(ref waitForStartCommand, index);
        RemoveAt(ref spawnDelays, index);
        RemoveAt(ref useRandomDelay, index);
        RemoveAt(ref minSpawnDelay, index);
        RemoveAt(ref maxSpawnDelay, index);
        RemoveAt(ref spawnCounter, index);
        RemoveAt(ref offsetSettings, index);
        RemoveAt(ref advancedSettings, index);
        RemoveAt(ref spawnOffsets, index);
        RemoveAt(ref useRandomOffset, index);
        RemoveAt(ref minOffsets, index);
        RemoveAt(ref maxOffsets, index);
        RemoveAt(ref spawnRotations, index);
        RemoveAt(ref useRandomRotation, index);
        RemoveAt(ref minRotation, index);
        RemoveAt(ref maxRotation, index);
        RemoveAt(ref actionAt, index);
        RemoveAt(ref value, index);
        RemoveAt(ref stop, index);
        RemoveAt(ref sendMessage, index);
        RemoveAt(ref receivers, index);
        RemoveAt(ref messages, index);
        RemoveAt(ref countDestroyed, index);
        RemoveAt(ref prefabsRemaining, index);
        RemoveAt(ref continueSpawningAt, index);
        RemoveAt(ref destroyedValue, index);
        RemoveAt(ref destroyPrefabs, index);
        RemoveAt(ref timeToLive, index);
        RemoveAt(ref usePrefabsDatabase, index);
        RemoveAt(ref selectedTags, index);
        RemoveAt(ref selectedTagIndex, index);
        RemoveAt(ref lastPrefabSpawned, index);
        RemoveAt(ref useRandomOrder, index);
        RemoveAt(ref db_lastIndex, index);

        if(storedIndex == index){
            storedSpawnerID = "";
        }

        delayTimer = spawnDelays;

        if(spawnEntities[index] != null){
            for(int i = 0; i < spawnEntities.Length -1; i++){
                spawnEntities[i].name = "Spawner_"+i;
            }
        }
    }

    public static void RemoveAt<T>(ref T[] arr, int index){
            for (int a = index; a < arr.Length - 1; a++){
                arr[a] = arr[a + 1];
                }
                    System.Array.Resize(ref arr, arr.Length - 1);
    }

    void OnDrawGizmos(){
        for(int i = 0; i < spawnPositions.Length -1; i++){
            if(isActive[i] == true){
                Gizmos.color = activeSpawnerColor;
            }else{
                Gizmos.color = inactiveSpawnerColor;
            }

            if(useRandomOffset[i] == true){
                float sideX = maxOffsets[i].x - minOffsets[i].x;
                float sideY = maxOffsets[i].y - minOffsets[i].y;
                float sideZ = maxOffsets[i].z - minOffsets[i].z;

                float gizmoOffsetX = maxOffsets[i].x - sideX/2;
                float gizmoOffsetY = maxOffsets[i].y - sideY/2;
                float gizmoOffsetZ = maxOffsets[i].z - sideZ/2;

                Vector3 gizmoPosition = new Vector3(gizmoOffsetX, gizmoOffsetY, gizmoOffsetZ);

                Gizmos.DrawWireCube(spawnPositions[i].position + gizmoPosition , new Vector3(sideX, sideY, sideZ));
            }else{
                Gizmos.DrawWireSphere(spawnPositions[i].position + spawnOffsets[i], 0.3f);
            }

            if(useRandomRotation[i] == true){
                Vector3 maxLineDirection = spawnPositions[i].rotation * Quaternion.Euler(maxRotation[i]) * Vector3.forward;
                Vector3 minLineDirection = spawnPositions[i].rotation * Quaternion.Euler(minRotation[i]) * Vector3.forward;
                Gizmos.DrawRay(spawnPositions[i].position, maxLineDirection);
                Gizmos.DrawRay(spawnPositions[i].position, minLineDirection);
            }else{
                Vector3 lineDirection =  spawnPositions[i].rotation * Quaternion.Euler(spawnRotations[i]) * Vector3.forward;
                Gizmos.DrawRay(spawnPositions[i].position, lineDirection);
            }
        }
    }

    public void Copy(int index){
        storedSpawnerID = "Spawner "+index.ToString();
        storedIndex = index;
    }

    public void Paste(int index){
        isActive[index] = isActive[storedIndex];
        descriptions[index] = descriptions[storedIndex];
        prefabsToSpawn[index] = prefabsToSpawn[storedIndex];
        spawnEntities[index] = spawnEntities[storedIndex];
        readyToSpawn[index] = readyToSpawn[storedIndex];
        startSpawnDelays[index] = startSpawnDelays[storedIndex];
        waitForStartCommand[index] = waitForStartCommand[storedIndex];
        spawnDelays[index] = spawnDelays[storedIndex];
        useRandomDelay[index] = useRandomDelay[storedIndex];
        minSpawnDelay[index] = minSpawnDelay[storedIndex];
        maxSpawnDelay[index] = maxSpawnDelay[storedIndex];
        spawnCounter[index] = spawnCounter[storedIndex];
        offsetSettings[index] = offsetSettings[storedIndex];
        advancedSettings[index] = advancedSettings[storedIndex];
        spawnOffsets[index] = spawnOffsets[storedIndex];
        useRandomOffset[index] = useRandomOffset[storedIndex];
        minOffsets[index] = minOffsets[storedIndex];
        maxOffsets[index] = maxOffsets[storedIndex];
        spawnRotations[index] = spawnRotations[storedIndex];
        useRandomRotation[index] = useRandomRotation[storedIndex];
        minRotation[index] = minRotation[storedIndex];
        maxRotation[index] = maxRotation[storedIndex];
        actionAt[index] = actionAt[storedIndex];
        value[index] = value[storedIndex];
        stop[index] = stop[storedIndex];
        sendMessage[index] = sendMessage[storedIndex];
        receivers[index] = receivers[storedIndex];
        messages[index] = messages[storedIndex];
        useIndexParameter[index] = useIndexParameter[storedIndex];
        countDestroyed[index] = countDestroyed[storedIndex];
        prefabsRemaining[index] = prefabsRemaining[storedIndex];
        continueSpawningAt[index] = continueSpawningAt[storedIndex];
        destroyedValue[index] = destroyedValue[storedIndex];
        destroyPrefabs[index] = destroyPrefabs[storedIndex];
        timeToLive[index] = timeToLive[storedIndex];
        usePrefabsDatabase[index] = usePrefabsDatabase[storedIndex];
        selectedTags[index] = selectedTags[storedIndex];
        selectedTagIndex[index] = selectedTagIndex[storedIndex];
        useRandomOrder[index] = useRandomOrder[storedIndex];
    }

    public void ClearClipboard(){
        storedSpawnerID = "";
    }

    public void AddPrefab(){
        System.Array.Resize(ref databasePrefabs, databasePrefabs.Length +1);
        System.Array.Resize(ref tagIndex, tagIndex.Length +1);
    }

    public void RemovePrefab(int index){
        RemoveAt(ref databasePrefabs, index);
        RemoveAt(ref tagIndex, index);
    }

    public void RemoveAllPrefabs(){
        databasePrefabs = new GameObject[0];
        tagIndex = new int[0];
    }

    public void AddTag(){
        System.Array.Resize(ref prefabTags, prefabTags.Length +1);
    }

    public void RemoveTag(int index){
        if(prefabTags.Length > 1){
            RemoveAt(ref prefabTags, index);
        }
    }

    public void RemoveAllTags(){
        prefabTags = new string[0];
    }

    public void EqualizeVariables(){
        System.Array.Resize(ref showInInspector, showInInspector.Length);
        System.Array.Resize(ref isActive, showInInspector.Length);
        System.Array.Resize(ref descriptions, showInInspector.Length);
        System.Array.Resize(ref spawnPositions, showInInspector.Length);
        System.Array.Resize(ref prefabsToSpawn, showInInspector.Length);
        System.Array.Resize(ref readyToSpawn, showInInspector.Length);
        System.Array.Resize(ref spawnEntities, showInInspector.Length);
        System.Array.Resize(ref startSpawnDelays, showInInspector.Length);
        System.Array.Resize(ref waitForStartCommand, showInInspector.Length);
        System.Array.Resize(ref spawnDelays, showInInspector.Length);
        System.Array.Resize(ref useRandomDelay, showInInspector.Length);
        System.Array.Resize(ref minSpawnDelay, showInInspector.Length);
        System.Array.Resize(ref maxSpawnDelay, showInInspector.Length);
        System.Array.Resize(ref spawnCounter, showInInspector.Length);
        System.Array.Resize(ref offsetSettings, showInInspector.Length);
        System.Array.Resize(ref advancedSettings, showInInspector.Length);
        System.Array.Resize(ref spawnOffsets, showInInspector.Length);
        System.Array.Resize(ref useRandomOffset, showInInspector.Length);
        System.Array.Resize(ref minOffsets, showInInspector.Length);
        System.Array.Resize(ref maxOffsets, showInInspector.Length);
        System.Array.Resize(ref spawnRotations, showInInspector.Length);
        System.Array.Resize(ref useRandomRotation, showInInspector.Length);
        System.Array.Resize(ref minRotation, showInInspector.Length);
        System.Array.Resize(ref maxRotation, showInInspector.Length);
        System.Array.Resize(ref actionAt, showInInspector.Length);
        System.Array.Resize(ref value, showInInspector.Length);
        System.Array.Resize(ref stop, showInInspector.Length);
        System.Array.Resize(ref sendMessage, showInInspector.Length);
        System.Array.Resize(ref receivers, showInInspector.Length);
        System.Array.Resize(ref messages, showInInspector.Length);
        System.Array.Resize(ref useIndexParameter, showInInspector.Length);
        System.Array.Resize(ref countDestroyed, showInInspector.Length);
        System.Array.Resize(ref prefabsRemaining, showInInspector.Length);
        System.Array.Resize(ref continueSpawningAt, showInInspector.Length);
        System.Array.Resize(ref destroyedValue, showInInspector.Length);
        System.Array.Resize(ref destroyPrefabs, showInInspector.Length);
        System.Array.Resize(ref timeToLive, showInInspector.Length);
        System.Array.Resize(ref usePrefabsDatabase, showInInspector.Length);
        System.Array.Resize(ref selectedTags, showInInspector.Length);
        System.Array.Resize(ref selectedTagIndex, showInInspector.Length);
        System.Array.Resize(ref lastPrefabSpawned, showInInspector.Length);
        System.Array.Resize(ref useRandomOrder, showInInspector.Length);
        System.Array.Resize(ref db_lastIndex, showInInspector.Length);
    }
}
