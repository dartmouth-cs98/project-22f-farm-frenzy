using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class isReadyTrigger : MonoBehaviour
{
    
    [SerializeField] private bool triggerActive = false;
    private bool isReady;
    public TMP_Text player_unready_ui;
    public TMP_Text player_ready_ui;

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            triggerActive = true;
            player_unready_ui.enabled = true;
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            triggerActive = false;
            player_unready_ui.enabled = false;
            player_ready_ui.enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.E)) {
            
            isReady = true;
            Debug.Log("Player is ready");
            
        }

        if (isReady) {
            changeUI();
        }
        
    }

    void changeUI() {
        player_unready_ui.enabled = false;
        player_ready_ui.enabled = true;
    }
}

