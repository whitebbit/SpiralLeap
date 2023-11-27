using System;
using System.Collections;
using System.Collections.Generic;
using _3._Scripts;
using _3._Scripts.AD;
using _3._Scripts.Architecture;
using _3._Scripts.Architecture.Scriptable;
using _3._Scripts.Game;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;

public class GameManager : Singleton<GameManager>
{
    public GameObject HelixPlatformPrefab;
    [SerializeField] private Finish finishHelix;
    [SerializeField] private Cylinder cylinder;
    [Header("Panels")] [SerializeField] private CanvasGroup playPanel;
    [SerializeField] private CanvasGroup menuPanel;
    [SerializeField] private CanvasGroup losePanel;
    [SerializeField] private CanvasGroup shopPanel;
    [SerializeField] private CanvasGroup winPanel;
    [SerializeField] private CanvasGroup mapPanel;
    [SerializeField] private CanvasGroup premiumPanel;
    [Header("Buttons")] [SerializeField] private Button shopButton;
    [SerializeField] private Button premiumButton;
    public CanvasGroup MenuPanel => menuPanel;
    public CanvasGroup LosePanel => losePanel;
    public CanvasGroup PlayPanel => playPanel;
    public CanvasGroup WinPanel => winPanel;
    public CanvasGroup MapPanel => mapPanel;
    public CanvasGroup PremiumPanel => premiumPanel;

    private CanvasGroup _currentPanel;
    public static bool isStarted { get; private set; }
    public static Theme CurrentTheme;

    private List<Tween> _tweens = new List<Tween>();

    protected override void Awake()
    {
        base.Awake();
        LoadLevel();
    }

    private void Start()
    {
        _currentPanel = menuPanel;
        Time.timeScale = 1f;
        isStarted = false;

        CreateHelix();
        shopButton.onClick.AddListener(() =>
        {
            AudioManager.instance.PlayOneShot(AudioManager.instance.Config.UIClick);
            ChangePanel(shopPanel);
        });
        premiumButton.onClick.AddListener(() =>
        {
            AudioManager.instance.PlayOneShot(AudioManager.instance.Config.UIClick);
            ChangePanel(premiumPanel);
        });
        premiumButton.transform.parent.gameObject.SetActive(!YandexGame.savesData.premium);

        GetLoad();
        YandexGame.StickyAdActivity(true);
        OnSceneLoad();
    }

    private static void LoadLevel()
    {
        if (CurrentTheme == null)
            CurrentTheme = Configuration.instance.themesHolder.GetRandomTheme();

        RenderSettings.skybox = CurrentTheme.Skybox;
        Instantiate(CurrentTheme.Background);
    }

    public void ChangePanel(CanvasGroup panel)
    {
        PanelState(_currentPanel, false);
        _currentPanel = panel;
        PanelState(panel, true);
    }

    private void CreateHelix()
    {
        float yPos = 0;
        for (var i = 0; i < ScoreManager.instance.levelGoal; i++)
        {
            if (i == ScoreManager.instance.levelGoal - 1)
            {
                var finish = Instantiate(finishHelix, cylinder.transform);
                finish.transform.position = new Vector3(0, yPos, 0);
            }
            else
            {
                var helixPlatform = Instantiate(HelixPlatformPrefab, cylinder.transform);
                helixPlatform.transform.position = new Vector3(0, yPos, 0);
                yPos -= 3.5f;
            }
        }
    }

    private void PanelState(CanvasGroup panel, bool state)
    {
        foreach (var tween in _tweens)
        {
            tween.Pause();
            tween.Kill();
        }

        _tweens.Clear();

        if (state)
        {
            var open = panel.DOFade(1, 0.25f).OnKill(() =>
            {
                panel.blocksRaycasts = true;
                panel.alpha = 1;
                panel.gameObject.SetActive(true);
            });
            _tweens.Add(open);
            panel.gameObject.SetActive(true);
            panel.blocksRaycasts = true;
        }
        else
        {
            panel.blocksRaycasts = false;
            var close = panel.DOFade(0, .25f)
                .OnComplete(() => { panel.gameObject.SetActive(false); })
                .OnKill(() =>
                {
                    panel.blocksRaycasts = false;
                    panel.alpha = 0;
                    panel.gameObject.SetActive(false);
                });
            _tweens.Add(close);
        }
    }

    public void StartGame()
    {
        if (isStarted) return;
        isStarted = true;
        ChangePanel(playPanel);
        AudioManager.instance.PlayOneShot(AudioManager.instance.Config.UIClick);
    }

    public void NextLevel()
    {
        YandexGame.savesData.currentLevel += 1;
        YandexGame.SaveProgress();
        SceneManager.LoadScene("Main");
        CurrentTheme = null;
    }

    private void GetLoad()
    {
        try
        {
            var panels = new List<CanvasGroup>
            {
                shopPanel,
                playPanel,
                losePanel,
                winPanel,
                mapPanel,
                premiumPanel
            };
            foreach (var panel in panels)
            {
                panel.alpha = 0;
                panel.blocksRaycasts = false;
                panel.gameObject.SetActive(false);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }


    private void OnSceneLoad()
    {
        if (YandexGame.savesData.currentLevel is 3 || YandexGame.savesData.currentLevel % 10 == 0)
        {
            if (YandexGame.EnvironmentData.reviewCanShow)
            {
                YandexGame.ReviewShow(true);
                return;
            }
        }

        YandexAD.ShowInterstitial();
    }
}