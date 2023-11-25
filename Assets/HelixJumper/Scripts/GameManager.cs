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
        PanelState(panel, true);
        _currentPanel = panel;
    }

    private void CreateHelix()
    {
        float yPos = 0;
        for (var i = 0; i < ScoreManager.instance.levelGoal; i++)
        {
            if (i == ScoreManager.instance.levelGoal - 1)
            {
                var finish = Instantiate(finishHelix, transform);
                finish.transform.position = new Vector3(0, yPos, 0);
            }
            else
            {
                var helixPlatform = Instantiate(HelixPlatformPrefab, transform);
                helixPlatform.transform.position = new Vector3(0, yPos, 0);
                yPos -= 3.5f;
            }
        }
    }

    private static void PanelState(CanvasGroup panel, bool state)
    {
        if (state)
        {
            panel.gameObject.SetActive(true);
            panel.DOFade(1, 0.25f);
            panel.blocksRaycasts = true;
        }
        else
        {
            panel.DOFade(0, .25f).OnComplete(() =>
            {
                panel.gameObject.SetActive(false);
                panel.blocksRaycasts = false;
            });
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
        if (YandexGame.savesData.currentLevel is 3 or 7 or 11)
        {
            if (YandexGame.EnvironmentData.promptCanShow && !YandexGame.savesData.promptDone)
            {
                YandexGame.PromptShow();
                return;
            }
        }
        if (YandexGame.savesData.currentLevel is 2 or 6 or 10)
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