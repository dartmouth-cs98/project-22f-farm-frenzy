using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class TriggerDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI textComponent2;

    public GameObject button1;
    public GameObject button2;


    public string[] lines;
    public string[] characters;
    public float textSpeed;
    private int index;
    public Canvas hint;
    public Canvas dialog;
    public Canvas characterBox;
    private bool onDialogRange; // on npc range for dialog
    private bool dpadPressed = false;
    public bool isPaused;


    
    void Start()
    {
        hint.enabled = false;
        dialog.enabled = false;

        button1.SetActive(false);
        button2.SetActive(false);
        
        textComponent2.text = string.Empty;
        textComponent.text = string.Empty;
    }

    public void playerInteract() {

        dpadPressed = !dpadPressed;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.inputString != "") Debug.Log(Input.inputString);
        if (onDialogRange && dpadPressed)
        {
            Time.timeScale = 0f;
            isPaused = true;
            
            if (dialog.enabled == false ) {
                dialog.enabled = true;
                hint.enabled = false;
                textComponent2.text = string.Empty;
                textComponent.text = string.Empty;
                
                StartDialogue();
            }
            if (dialog.enabled == true)
            {
                if (dpadPressed) //Input.GetMouseButtonDown(0))
                {
                    dpadPressed = false;
                    if (textComponent.text == lines[index])
                    {
                        NextLine();
                    }
                    else
                    {
                        StopAllCoroutines();
                        textComponent2.text = characters[index];
                        textComponent.text = lines[index];
                        
                        //dialog.enabled = false;
                    }
                }
            }
        }

        Time.timeScale = 1f;
        isPaused = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hint.enabled = true;
            onDialogRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            hint.enabled = false;
            
            
            onDialogRange = false;
        }
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in characters[index].ToCharArray())
        {
            textComponent2.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        } 
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent2.text = string.Empty;
            textComponent.text = string.Empty;  
            StartCoroutine(TypeLine());
            if (index == 4){
                button1.SetActive(true);
                button2.SetActive(true);
                
            }
            if (index == 6){
                dialog.enabled = false;
            }
        }
        else
        {
            dialog.enabled = false;
            button1.SetActive(false);
            button2.SetActive(false);
        }
    }
    
    public void TaskOnClick1(){

        index = 4;

        button1.SetActive(false);
        button2.SetActive(false);
        
        NextLine();
    }
    public void TaskOnClick2(){
        index = 6;

        button1.SetActive(false);
        button2.SetActive(false);

        NextLine();
    }


}
