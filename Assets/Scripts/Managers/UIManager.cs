using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject startPanel, winPanel, losePanel;

    protected override void Awake()
    {
        base.Awake();

        EventManager.OnBeginGame += OnBeginPanelState;
        EventManager.OnWin += OnWinPanel;
        EventManager.OnLose += OnLosePanel;
    }

    private void OnBeginPanelState()
    {
        startPanel.SetActive(true);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    public void OnWinPanel() => winPanel.SetActive(true);

    public void OnLosePanel() => losePanel.SetActive(true);

    private void OnDisable()
    {
        EventManager.OnBeginGame -= OnBeginPanelState;
        EventManager.OnWin -= OnWinPanel;
        EventManager.OnLose -= OnLosePanel;
    }
}
