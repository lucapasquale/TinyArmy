using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatPanel : MonoBehaviour
{
    public Panel panel;
    public Sprite backgroundSprite;
    public Text nameLabel;
    public Text hpLabel;
    public Text dmgLabel;
    public Text aSpeedLabel;
    public Text lvLabel;

    public void Display(GameObject obj)
    {
        Stats stats = obj.GetComponent<Stats>();
        Rank ranks = obj.GetComponent<Rank>();
        if (stats)
        {
            nameLabel.text = string.Format("{0} {1}", stats.classType.ToString(), stats[StatTypes.Alive] == 1 ? "" : " - DEAD");

            hpLabel.text = string.Format("HP {0} / {1}", stats[StatTypes.HP], stats[StatTypes.MHP]);

            dmgLabel.text = string.Format("DAMAGE: {0}", stats[StatTypes.DMG]);

            aSpeedLabel.text = string.Format("A. Speed: {0}", stats[StatTypes.ASpeed]);

            lvLabel.text = string.Format("LV: {0}  -  {1}%", stats[StatTypes.LVL], ranks.ExpPercent.ToString("F2"));
        }
    }
}