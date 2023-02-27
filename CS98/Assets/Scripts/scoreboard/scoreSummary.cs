using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreSummary : MonoBehaviour
{
    private List<PlayerControllerRagdoll> allPlayers = new List<PlayerControllerRagdoll>();
    public (PlayerControllerRagdoll, int) most_knockouts;
    public (PlayerControllerRagdoll, int) most_seed_planted;
    public (PlayerControllerRagdoll, int) most_fruit_traded;
    public (PlayerControllerRagdoll, int) most_fruit_scored;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject hips in GameObject.FindGameObjectsWithTag("Player"))
        {
            allPlayers.Add(hips.GetComponentInParent<PlayerControllerRagdoll>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (PlayerControllerRagdoll p in allPlayers)
        {
            if (p.knockouts > most_knockouts.Item2) {
                most_knockouts.Item1 = p;
                most_knockouts.Item2 = p.knockouts;
            }
            if (p.seed_planted > most_seed_planted.Item2)
            {
                most_seed_planted.Item1 = p;
                most_seed_planted.Item2 = p.seed_planted;
            }
            if (p.fruit_trade > most_fruit_traded.Item2)
            {
                most_fruit_traded.Item1 = p;
                most_fruit_traded.Item2 = p.fruit_trade;
            }
            if (p.scored_fruits > most_fruit_scored.Item2)
            {
                most_fruit_scored.Item1 = p;
                most_fruit_scored.Item2 = p.scored_fruits;
            }
        }
    }
}
