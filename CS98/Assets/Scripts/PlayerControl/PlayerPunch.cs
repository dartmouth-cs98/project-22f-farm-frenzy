using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{

    public float punchingRange = 3.0f;
    public Rigidbody fist;
    public Animator animator;
    public float ThrowingForce = 100f;
    [SerializeField] public Rigidbody self_rb;

    // punch effct

    public void Punch()
    {
            animator.SetBool("Idle", false);
            animator.SetBool("Walking", false);
            animator.SetBool("Punching", true);

            RaycastHit hitInfo;

        if (Physics.Raycast(fist.transform.position, fist.transform.forward, out hitInfo, punchingRange))
        {
            Debug.Log(hitInfo.transform.name);
            Collider collide = hitInfo.transform.GetComponent<Collider>();
            Debug.Log(collide.name);
            PlayerControllerRagdoll[] hitObj = hitInfo.transform.GetComponentsInParent<PlayerControllerRagdoll>();
            //PlayerControllerRagdoll[] hitObj = hitInfo.transform.GetComponentsInParent<PlayerControllerRagdoll>();
            if (hitObj.Length > 0)
            {
                //Debug.Log(hitObj[0].name);
                //hitObj[0].getStun(3);
                Rigidbody enemy = hitObj[0].rb;
                //Debug.Log(enemy.name);
                
                if (enemy != null && enemy != self_rb)
                {
                    // NEED TO TEST, NOT WORKING REALLY.
                    Debug.Log("atttttack");
                    enemy.isKinematic = false;
                    enemy.AddForce(fist.transform.forward * ThrowingForce, ForceMode.Impulse);
                }
            }

        }
        StartCoroutine(Punchwaiter());

    }

    IEnumerator Punchwaiter()
    {
        //Wait for half seconds
        yield return new WaitForSecondsRealtime(1f);

        animator.SetBool("Idle", true);
        animator.SetBool("Punching", false);
    }
}
