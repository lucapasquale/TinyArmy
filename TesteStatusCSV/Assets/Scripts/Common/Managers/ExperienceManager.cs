using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExperienceManager : MonoBehaviour{

    public static void AwardExp (int amount, int MonLevel, List<GameObject> party)
    {
        for (int i = 0; i < party.Count; i++)
        {
            Stats pStats = party[i].GetComponent<Stats>();
            Rank pRank = party[i].GetComponent<Rank>();
            float diff = 0.1f * Mathf.Abs(MonLevel - pStats[StatTypes.LVL]);
            diff = Mathf.Clamp(diff, 0, 1);

            int gainExp = (int) (amount * ( 1 - diff));

            pRank.EXP += gainExp;
        }
    }

    public static void AwardExp(int amount, List<GameObject> party)
    {
        for (int i = 0; i < party.Count; i++)
        {
            Rank pRank = party[i].GetComponent<Rank>();
            pRank.EXP += amount;
        }
    }



}
