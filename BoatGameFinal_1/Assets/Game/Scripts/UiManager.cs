using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }


    [SerializeField] Image imgBarFish;
    [SerializeField] Text txtScore;
    [SerializeField] Text txtProgress;
    [SerializeField] Text txtHealth;
    [SerializeField] GameObject panelLevelComplete;
    [SerializeField] GameObject panelLevelGameOver;
    [SerializeField] GameObject panelHintCatchFish;
    [SerializeField] GameObject panelHintShoot;
    [SerializeField] GameObject bossBar;
    [SerializeField] Image bossBarFill;

    [SerializeField] GameObject panelPause;

    int currentScore = 0;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

    }


    void Start()
    {
        
    }

    public void SetHealth(int health)
    {
        txtHealth.text = "Health " + health;
    }


    public void CatchingFish()
    {
        imgBarFish.gameObject.SetActive(true);

        imgBarFish.fillAmount = 0;
        imgBarFish.DOFillAmount(1f, 3f).SetEase(Ease.Linear);

    }

    public void EndCatchingFish()
    {
        imgBarFish.gameObject.SetActive(false);        

    }

    public void AddScore()
    {
        currentScore++;
        txtScore.text = currentScore.ToString();

    }

    public void ProgressBar(int currentFishes,int totFishes)
    {
        txtProgress.text = currentFishes + " / " + totFishes;

    }

    public void BossBarFill(int currentHealth)
    {
        bossBarFill.DOFillAmount(currentHealth / 100f, .27f);
    }

    public void SetPanelHintCatchFish(bool val)
    {
        panelHintCatchFish.SetActive(val);
    }

    public void SetPanelHintShoot(bool val)
    {
        panelHintShoot.SetActive(val);
    }

    public void SetBossBar(bool val)
    {
        bossBar.SetActive(val);

        if (val) bossBarFill.fillAmount = 1;

    }

    public void OnLevelComplete()
    {
        panelLevelComplete.SetActive(true);
    }

    public void OnLevelFail()
    {
        panelLevelGameOver.SetActive(true);
    }


    public void BtnNextLevel()
    {
        SetHealth(100);
        panelLevelComplete.SetActive(false);
        GameManager.instance.StartNewLevel();
    }

    public void BtnRestartLevel()
    {
        SetHealth(100);
        panelLevelGameOver.SetActive(false);
        GameManager.instance.RestartLevel();
    }

    public void BtnPause()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void BtnResume()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1;

    }

}
