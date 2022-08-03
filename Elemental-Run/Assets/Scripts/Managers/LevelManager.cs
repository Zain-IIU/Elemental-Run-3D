using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] levels;

    [SerializeField] private bool startDisabled;
    [SerializeField] private ProgressBar progressBar;
    private static int currentLevel
    {

        get => PlayerPrefs.GetInt("CurrentLevel", 0);
        set
        {
            PlayerPrefs.SetInt("CurrentLevel", value);
            PlayerPrefs.Save();
        }
    }

    private void Awake()
    {
        if (!startDisabled) return;
        foreach (var level in levels)  level.SetActive(false);
        Application.targetFrameRate = 60;
        LoadGame();
    }

   

    private void LoadGame()
    {        
        var index = PlayerPrefs.GetInt("LevelIndex");
        currentLevel = index > levels.Length ? Random.Range(0, levels.Length) : GetLevelIndex();
        
        //# todo: tiny sauce (this is start of game)
        // TinySauce.OnGameStarted(index.ToString());
        
        levels[currentLevel].SetActive(true);
    }

    int GetLevelIndex()
    {
        return currentLevel % levels.Length;
    }
    
    [ContextMenu("Load Next Level")]
    public void IncrementLevelIndex()
    {
        currentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
       // EventsManager.NextLevel();
       progressBar.NewLevel();
        PlayerPrefs.Save();
        Debug.Log("Loading Next level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    [ContextMenu("Restart Level")]
    public void ReplayLevel()
    {
        Debug.Log("Loading same level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}