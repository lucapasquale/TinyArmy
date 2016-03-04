using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatPanelController : MonoBehaviour
{
    #region Const
    const string ShowKey = "Show";
    const string HideKey = "Hide";
    #endregion

    #region Fields
    public bool showing;

    [SerializeField]
    StatPanel firstPanel;
    [SerializeField]
    StatPanel secondPanel;
    [SerializeField]
    StatPanel thirdPanel;
    [SerializeField]
    StatPanel fourthPanel;

    Tweener firstTransition;
    Tweener secondTransition;
    Tweener thirdTransition;
    Tweener fourthTransition;
    #endregion

    #region MonoBehaviour
    void Start()
    {
        if (firstPanel.panel.CurrentPosition == null)
            firstPanel.panel.SetPosition(HideKey, false);
        if (secondPanel.panel.CurrentPosition == null)
            secondPanel.panel.SetPosition(HideKey, false);
        if (thirdPanel.panel.CurrentPosition == null)
            thirdPanel.panel.SetPosition(HideKey, false);
        if (fourthPanel.panel.CurrentPosition == null)
            fourthPanel.panel.SetPosition(HideKey, false);
    }

    void OnEnable()
    {
        this.AddObserver(OnStatChange, Stats.DidChangeNotification(StatTypes.LVL));
        this.AddObserver(OnStatChange, Stats.DidChangeNotification(StatTypes.EXP));
        this.AddObserver(OnStatChange, Stats.DidChangeNotification(StatTypes.MHP));
        this.AddObserver(OnStatChange, Stats.DidChangeNotification(StatTypes.HP));
        this.AddObserver(OnStatChange, Stats.DidChangeNotification(StatTypes.DMG));
        this.AddObserver(OnStatChange, Stats.DidChangeNotification(StatTypes.Alive));
    }

    void OnDisable()
    {
        this.RemoveObserver(OnStatChange, Stats.DidChangeNotification(StatTypes.LVL));
        this.RemoveObserver(OnStatChange, Stats.DidChangeNotification(StatTypes.EXP));
        this.RemoveObserver(OnStatChange, Stats.DidChangeNotification(StatTypes.MHP));
        this.RemoveObserver(OnStatChange, Stats.DidChangeNotification(StatTypes.HP));
        this.RemoveObserver(OnStatChange, Stats.DidChangeNotification(StatTypes.DMG));
        this.RemoveObserver(OnStatChange, Stats.DidChangeNotification(StatTypes.Alive));
    }
    #endregion

    #region Event Handler
    void OnStatChange(object sender, object args)
    {
        GameManager gc = GetComponentInParent<GameManager>();
        if (showing)
        {
            HidePanels();
            ShowPanels(gc.party);
        }       
    }
    #endregion

    #region Public
    public void ShowPanels(List<GameObject> players)
    {

        if ((players.Count > 5) || (players.Count <= 0))
        {
            Debug.Log("Party com " + players.Count + "!");
            return;
        }

        
        switch (players.Count)
        {
            case 4:
                fourthPanel.Display(players[3]);
                MovePanel(fourthPanel, ShowKey, ref fourthTransition);
                thirdPanel.Display(players[2]);
                MovePanel(thirdPanel, ShowKey, ref thirdTransition);
                secondPanel.Display(players[1]);
                MovePanel(secondPanel, ShowKey, ref secondTransition);
                firstPanel.Display(players[0]);
                MovePanel(firstPanel, ShowKey, ref firstTransition);
                break;
            case 3:
                thirdPanel.Display(players[2]);
                MovePanel(thirdPanel, ShowKey, ref thirdTransition);
                secondPanel.Display(players[1]);
                MovePanel(secondPanel, ShowKey, ref secondTransition);
                firstPanel.Display(players[0]);
                MovePanel(firstPanel, ShowKey, ref firstTransition);
                break;
            case 2:
                secondPanel.Display(players[1]);
                MovePanel(secondPanel, ShowKey, ref secondTransition);
                firstPanel.Display(players[0]);
                MovePanel(firstPanel, ShowKey, ref firstTransition);
                break;
            case 1:
                firstPanel.Display(players[0]);
                MovePanel(firstPanel, ShowKey, ref firstTransition);
                break;
        }
        showing = true;        
    }

    public void HidePanels()
    {
        MovePanel(firstPanel, HideKey, ref firstTransition);
        MovePanel(secondPanel, HideKey, ref secondTransition);
        MovePanel(thirdPanel, HideKey, ref thirdTransition);
        MovePanel(fourthPanel, HideKey, ref fourthTransition);
        showing = false;
    }
    #endregion

    #region Private
     void MovePanel(StatPanel obj, string pos, ref Tweener t)
    {
        Panel.Position target = obj.panel[pos];
        if (obj.panel.CurrentPosition != target)
        {
            if (t != null && t.easingControl != null)
                t.easingControl.Stop();
            t = obj.panel.SetPosition(pos, true);
            t.easingControl.duration = 0.5f;
            t.easingControl.equation = EasingEquations.EaseOutQuad;
        }
    }
    #endregion
}