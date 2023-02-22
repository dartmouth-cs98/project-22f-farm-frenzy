using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;
    public Canvas hint;
    public Canvas dialog;
    private bool onDialogRange; // on npc range for dialog
    


    void Start()
    {
        hint.enabled = false;
        dialog.enabled = false;
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
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialog.enabled = false;
        }
    }
}
