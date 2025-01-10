using UnityEngine;
using UnityEngine.UI;

public class Tutorials : ViewScreen
{
    public static Tutorials Instance;

    public GameObject[] _panels;

    private int num = 0;
    private GameObject _currentPanel;

    public Button _nextBtn;
    public Button _closeBtn;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        _nextBtn.onClick.AddListener(() =>
        {
            NextStep();
        });

        _closeBtn.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    public void RunTutorial()
    {
        Show();
        num = 0;
        NextStep();
    }

    public void NextStep()
    {
        if (num < _panels.Length)
        {
            if (_currentPanel != null) _currentPanel.SetActive(false);
            _currentPanel = _panels[num];
            _currentPanel.SetActive(true);
            num++;
        }
        else
        {
            Hide();
        }
    }
}
