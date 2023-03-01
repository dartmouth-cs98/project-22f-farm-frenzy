using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriggerDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI textComponent2;

    public TextMeshProUGUI buttonText1;
    public TextMeshProUGUI buttonText2;

    public Button button1;
    public Button button2;


    public string[] lines;
    public string[] characters;
    public float textSpeed;
    private int index;
    public Canvas hint;
    public Canvas dialog;
    public Canvas characterBox;
    private bool onDialogRange; // on npc range for dialog



    void Start()
    {
        hint.enabled = false;
        dialog.enabled = false;
        // Button btn1 = button1.GetComponent<Button>();
        // Button btn2 = button2.GetComponent<Button>();
        button1.onClick.AddListener(TaskOnClick1);
        button2.onClick.AddListener(TaskOnClick2);
        button1.enabled = false;
        button2.enabled = false;
        buttonText1.enabled = false;
        buttonText2.enabled = false;
        textComponent2.text = string.Empty;
        textComponent.text = string.Empty;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onDialogRange && Input.GetMouseButtonDown(0))
        {
            if (dialog.enabled == false) {
                dialog.enabled = true;
                hint.enabled = false;
                textComponent2.text = string.Empty;
                textComponent.text = string.Empty;
                
                StartDialogue();
            }
            if (dialog.enabled == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
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
        // if (index == 5){
        //     Debug.Log("in");
        //     dialog.enabled = false;
        // }
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
                button1.enabled = true;
                button2.enabled = true;
                buttonText1.enabled = true;
                buttonText2.enabled = true;
            }
            if (index == 5){
                // textComponent2.text = string.Empty;
                // textComponent.text = string.Empty;  
                // StartCoroutine(TypeLine());
                dialog.enabled = false;
            }
        }
        else
        {
            dialog.enabled = false;
        }
    }
    
    void TaskOnClick1(){
        Debug.Log(index);
        //index = 5;
        button1.enabled = false;
        button2.enabled = false;
        buttonText1.enabled = false;
        buttonText2.enabled = false;
    }
    void TaskOnClick2(){
        index = 6;
        button1.enabled = false;
        button2.enabled = false;
        buttonText1.enabled = false;
        buttonText2.enabled = false;
    }


}
