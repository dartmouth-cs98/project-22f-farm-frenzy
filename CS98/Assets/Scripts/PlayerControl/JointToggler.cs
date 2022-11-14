//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class JointToggler : MonoBehaviour
//{
//    [SerializeField] private ConfigurableJoint joint;
//    private Rigidbody connectedBody;

//    private void Awake()
//    {
//        joint = joint ? joint : GetComponent<ConfigurableJoint>();
//        if (joint) connectedBody = joint.connectedBody;
//        else Debug.LogError("No joint found.", this);
//    }

//    public void OnEnable() { joint.connectedBody = connectedBody; }

//    public void OnDisable()
//    {
//        joint.connectedBody = null;
//        connectedBody.WakeUp();
//        Debug.Log("disabled the hip");
//    }
//}