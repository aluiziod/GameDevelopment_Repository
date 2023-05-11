using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public static int CurrentLevel
    {

        get => PlayerPrefs.GetInt("CurrentLevel", 0);
        set
        {
            PlayerPrefs.SetInt("CurrentLevel", value);
            PlayerPrefs.Save();
        }
    }

   


    [SerializeField] GameObject[] levels;

    public int CurrentLevelFishes = 0;
    public int CurrentKilledFishes = 0;
    public int CurrentScore = 0;


    bool isBoosLevel = false;
    void Start()
    {
        Application.targetFrameRate = 60;
        InitLevel();

    }
    void InitLevel()
    {
        var old = GameObject.FindGameObjectsWithTag("Destroyable");
        foreach (var item in old)
        {
            Destroy(item);
        }

        var levelIndex = GetLevelIndex();
        var lvl = Instantiate(levels[levelIndex],
            levels[levelIndex].transform.position, levels[levelIndex].transform.rotation);

        CurrentLevelFishes = lvl.GetComponent<LevelSpec>().TotalEnemies;
        isBoosLevel = lvl.GetComponent<LevelSpec>().isBossLevel;
        CurrentKilledFishes = 0;

        if (isBoosLevel) UiManager.Instance.SetBossBar(true);
        else UiManager.Instance.SetBossBar(false);

        lvl.tag = "Destroyable";
        lvl.SetActive(true);
       

        UiManager.Instance.ProgressBar(CurrentKilledFishes, CurrentLevelFishes);


        OnLevelStart();
    }

    public void OnFishKilled()
    {
        CurrentScore += Random.Range(10, 16);
        CurrentKilledFishes++;
      
        UiManager.Instance.ProgressBar(CurrentKilledFishes,CurrentLevelFishes);

        if (CurrentKilledFishes >= CurrentLevelFishes)
        {
           
            
            OnLevelClear();
        }

    }

    public void OnEnemyKilled()
    {
        if (!isBoosLevel) return;

        OnLevelClear();

    }


    /// <summary>
    /// After 15 Levels repeat 
    /// </summary>
    /// <returns> Current Level Index </returns>
    int GetLevelIndex()
    {

        return CurrentLevel % levels.Length;

    }



    /// <summary>
    /// Trigger when Level Start
    /// </summary>
    void OnLevelStart()
    {
        print("Level Start");



        ///        MASGameEvents.instance.LevelEvent(MASGameEvents.LevelEvents.LevelStarted, CurrentLevel);
    }



    /// <summary>
    ///  Trigger when Level Clear
    /// </summary>
    public void OnLevelClear()
    {
        print("Level Clear");
        FindObjectOfType<Movement>().SetMove(false);

        MAliGMethods.Wait(() => {

            UiManager.Instance.OnLevelComplete();


        }, 1.5f, this);

        // MASGameEvents.instance.LevelEvent(MASGameEvents.LevelEvents.LevelCompleted, CurrentLevel);
    }



    /// <summary>
    /// Trigger when Level Failed
    /// </summary>
    public void OnLevelFailed()
    {
        print("Level Failed");
        FindObjectOfType<Movement>().SetMove(false);



        MAliGMethods.Wait(() => {

            UiManager.Instance.OnLevelFail();

        }, .5f, this);

        // MASGameEvents.instance.LevelEvent(MASGameEvents.LevelEvents.LevelFailed, CurrentLevel);
    }


    public void StartNewLevel()
    {
        CurrentLevel++;
        InitLevel();
    }

    public void RestartLevel()
    {
        CurrentScore = 0;

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        InitLevel();
    }

    public void PlayStartLevel()
    {
        CurrentScore = 0;

        InitLevel();
    }



}