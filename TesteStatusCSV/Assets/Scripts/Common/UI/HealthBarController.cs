using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthBarController : MonoBehaviour {

    public Stats myStats;
    public RectTransform healthBar;
    public Text healthText;
    float healthPercent;
    float curHealth = 1;
    float maxHealth = 1;

    void Awake () {
        myStats = GetComponentInParent<Stats>();
    }

    void OnEnable()
    {
        this.AddObserver(HPChanged, Stats.DidChangeNotification(StatTypes.HP), myStats);
        this.AddObserver(MHPChanged, Stats.DidChangeNotification(StatTypes.MHP), myStats);
    }

    void OnDisable()
    {
        this.RemoveObserver(HPChanged, Stats.DidChangeNotification(StatTypes.HP), myStats);
        this.RemoveObserver(MHPChanged, Stats.DidChangeNotification(StatTypes.MHP), myStats);
    }

    GameObject FindClosestChar()
    {
        HealthBarController[] gos = GameObject.FindObjectsOfType<HealthBarController>();
        if (gos != null)
        {
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (HealthBarController go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go.transform.gameObject;
                    distance = curDistance;
                }
            }
            return closest;
        }
        else
            return null;
    }

    void UpdateHealthBar()
    {
        healthPercent = curHealth / maxHealth;
        Mathf.Clamp01(healthPercent);
        healthBar.localScale = new Vector3(healthPercent, healthBar.localScale.y, healthBar.localScale.z);
        healthText.text = curHealth.ToString();
    }

    void HPChanged (object sender, object args)
    {
        Stats stat = sender as Stats;
        curHealth = stat[StatTypes.HP];
        maxHealth = stat[StatTypes.MHP];
        if (maxHealth != 0)
            UpdateHealthBar();
    }

    void MHPChanged(object sender, object args)
    {
        Stats stat = sender as Stats;
        curHealth = stat[StatTypes.HP];
        maxHealth = stat[StatTypes.MHP];

        if (maxHealth != 0)
            UpdateHealthBar();   
    }
}
