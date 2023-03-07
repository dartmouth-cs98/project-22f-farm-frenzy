using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreSummary : MonoBehaviour
{
    private List<PlayerControllerRagdoll> allPlayers = new List<PlayerControllerRagdoll>();
    public (PlayerControllerRagdoll, int) most_knockouts;
    public (PlayerControllerRagdoll, int) most_seed_planted;
    public (PlayerControllerRagdoll, int) most_fruit_traded;
    public (PlayerControllerRagdoll, int) most_fruit_scored;

    public TMP_Text knockout_txt;
    public TMP_Text seed_txt;
    public TMP_Text trade_txt;
    public TMP_Text score_txt;

    void Update() {
        PlayerControllerRagdoll knockoutPlayer = most_knockouts.Item1;
        PlayerControllerRagdoll seedPlayer = most_seed_planted.Item1;
        PlayerControllerRagdoll tradePlayer = most_fruit_traded.Item1;
        PlayerControllerRagdoll scorePlayer = most_fruit_scored.Item1;
        GameObject knockoutHat = new GameObject();
        GameObject seedHat = new GameObject();
        GameObject tradeHat = new GameObject();
        GameObject scoreHat = new GameObject();
        knockoutHat.name = "TEST1";
        seedHat.name = "TEST2";
        tradeHat.name = "TEST3";
        scoreHat.name = "TEST4";

        // if (knockoutPlayer.transform.GetChild(4).gameObject != null) {
        //     knockoutHat = knockoutPlayer.transform.GetChild(4).gameObject;
        // } else {
        //     knockoutHat.name = "No Hat";
        // }
        // if (seedPlayer.transform.GetChild(4).gameObject != null) {
        //     seedHat = seedPlayer.transform.GetChild(4).gameObject;
        // } else {
        //     seedHat.name = "No Hat";
        // }
        // if (tradePlayer.transform.GetChild(4).gameObject != null) {
        //     tradeHat = tradePlayer.transform.GetChild(4).gameObject;
        // } else {
        //     tradeHat.name = "No Hat";
        // }
        // if (scorePlayer.transform.GetChild(4).gameObject != null) {
        //     scoreHat = scorePlayer.transform.GetChild(4).gameObject;
        // } else {
        //     scoreHat.name = "No Hat";
        // }

        knockout_txt.text = knockoutHat.name + " knocked out " + most_knockouts.Item2.ToString("0") + " duck(s)";
        seed_txt.text = seedHat.name + " gathered " + most_knockouts.Item2.ToString("0") + " seed(s)";
        trade_txt.text = tradeHat.name + " traded " + most_knockouts.Item2.ToString("0") + " fruit(s)";
        score_txt.text = scoreHat.name + " scored " + most_knockouts.Item2.ToString("0") + " time(s)";
    }

    // Start is called before the first frame update
    void getScores()
    {
        foreach (GameObject hips in GameObject.FindGameObjectsWithTag("Player"))
        {
            allPlayers.Add(hips.GetComponentInParent<PlayerControllerRagdoll>());
        }

        foreach (PlayerControllerRagdoll p in allPlayers)
        {
            if (p.knockouts > most_knockouts.Item2)
            {
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
