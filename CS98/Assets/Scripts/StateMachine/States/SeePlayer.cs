using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

internal class SeePlayer : IState
{
    private readonly Shopper _shopper;
    private readonly PlayerDetector _playerDetector;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private GadgetPop _GadgetPop;
    public bool tradeComplete = false;
    private MonoBehaviour mono;
    private float walkRadius = 200f;

    // stop moving, look at the player
    // if player has the correct fruit -> get it and score
    // if not, quit the state...??

    public SeePlayer(Shopper shopper, NavMeshAgent navMeshAgent, PlayerDetector playerDetector, Animator animator, MonoBehaviour monob, GadgetPop gadgetPop)
    {
        _shopper = shopper;
        _playerDetector = playerDetector;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
        mono = monob;
        _GadgetPop = gadgetPop;
    }


    public void Tick()
    {

    }

    public void OnEnter()
    {
        //Debug.Log("state " + "see player");
        // look at the player
        _shopper.transform.LookAt(_playerDetector._detectedPlayer.transform);
        _animator.SetBool("idle", true);
        _animator.SetBool("walk", false);

        trade();
    }

    public void OnExit()
    {
        tradeComplete = false;
    }

    private void trade()
    {
        // check fruit
        Debug.Log("checking fruit...");
        if (_playerDetector._detectedFruit != null)
        {
            _navMeshAgent.enabled = false;
            string playerFruit = _playerDetector._detectedFruit.ToLower();
            if (playerFruit.Contains(_shopper.fruit_wanted))
            {
                // give player buff
                // if success, set the want fruit to be null and give the player rewards
                // give player buff
                Debug.Log("fruit sold!");
                // play sprite
                _shopper.GetComponentInChildren<ChatBubble>().Create("happy");
                // Gives a random buff to the player and gets rid of the fruit
                GameObject fruit = _playerDetector._detectedPlayer.GetComponentInChildren<PickUpObject>().ObjectIwantToPickUp;
                // Drop then destroy, so the player can keep picking up things
                _playerDetector._detectedPlayer.GetComponentInChildren<PickUpObject>().dropItem();
                GameObject.Destroy(fruit);

                // gadet update and pop up
                GadgetManagerScript _gadgetManagerScript = _playerDetector._detectedPlayer.GetComponentInChildren<GadgetManagerScript>();
                _gadgetManagerScript.setRandomGadget();
                _shopper.GetComponent<GadgetPop>().Pop(_gadgetManagerScript, _shopper);

                // score calcualtion
                _playerDetector._detectedPlayer.fruit_trade++;
                //mono.StartCoroutine(testFunction());
                //monoParser(mono);
                //mono.StopCoroutine(testFunction());
                _shopper.fruit_wanted = null;
            }
            else {
                _navMeshAgent.enabled = true;
                
            }
        }
        tradeComplete = true;
        //mono.StopCoroutine(testFunction());
    }

    public void monoParser(MonoBehaviour mono)
    {
        //We can now use StartCoroutine from MonoBehaviour in a non MonoBehaviour script
        //mono.StartCoroutine(testFunction());
        //Debug.Log("Test! stop!!!");
        ////And also use StopCoroutine function
        //mono.StopCoroutine(testFunction());
    }

    IEnumerator testFunction()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("stop here.........");
        _shopper.GetComponentInChildren<ChatBubble>().Create("happy");
        //_navMeshAgent.enabled = false;
    }

}
