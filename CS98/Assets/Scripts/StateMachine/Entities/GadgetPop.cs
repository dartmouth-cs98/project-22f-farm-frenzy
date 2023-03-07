using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetPop : MonoBehaviour
{
    [SerializeField] private GameObject speed_buff;
    [SerializeField] private GameObject jump_buff;
    GameObject buff_popup;
    private Vector3 offset = new Vector3(1f, 3.5f, 1f);
    
    public void Pop(GadgetManagerScript _GadgetManagerScript, Shopper _shopper)
    {
        string buff = _GadgetManagerScript.getGadget()[0];
        Debug.Log("this is the buff:");
        Debug.Log(buff);
        buff = "HighJump";
        if (buff == "None" || buff == null) return;
        if (buff == "SpeedBoost")
        {
            buff_popup = Instantiate(speed_buff, _shopper.transform.position, Quaternion.identity);
        }
        else
        {
            buff_popup = Instantiate(jump_buff, _shopper.transform.position, Quaternion.identity);
        }

        //buff_popup.transform.Translate(Vector3.down * 10f * Time.deltaTime);
        Destroy(buff_popup, 1.3f);
    }

}
