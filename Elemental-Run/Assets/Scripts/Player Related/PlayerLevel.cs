using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private Image levelFiller;
    [SerializeField] private TextMeshPro playerLevelText;
    [SerializeField] private int playerCurLevel;
    [SerializeField] private GameObject[] modelLevels;
    
    private int _curModelIndex;
    private void Start()
    {
        playerCurLevel = 1;
        _curModelIndex = 0;
        modelLevels[_curModelIndex].SetActive(true);
        EventsManager.OnPositiveEffect += IncrementPlayerLevel;
        EventsManager.OnLevelUpPlayer += NextLevelPlayer;
        EventsManager.OnNegativeEffect += DecrementPlayerLevel;
        EventsManager.OnReachedEnd += HideLevelFiller;
    }

    

    private void OnDisable()
    {
        EventsManager.OnPositiveEffect -= IncrementPlayerLevel;
        EventsManager.OnNegativeEffect -= DecrementPlayerLevel;
        EventsManager.OnLevelUpPlayer -= NextLevelPlayer;
        EventsManager.OnReachedEnd -= HideLevelFiller;
    }


    #region Event Callbacks

    private void NextLevelPlayer()
    {
        levelFiller.DOFillAmount(0, .2f).OnComplete(() =>
        {
            playerLevelText.text="LEVEL "+ (++playerCurLevel);
           
            if (playerCurLevel % 3 != 0) return;
            _curModelIndex++;
            EventsManager.PlayerLevelIncrease();
            foreach (var model in modelLevels)
            {
                model.SetActive(false);
            }
            modelLevels[_curModelIndex].SetActive(true);
        });
    }
    private void IncrementPlayerLevel()
    {
        
        levelFiller.DOFillAmount(levelFiller.fillAmount + 0.2f, .2f).OnComplete(() =>
        {
            if (!(levelFiller.fillAmount >= 1f)) return;
            playerLevelText.text="LEVEL "+ (++playerCurLevel);
            levelFiller.fillAmount = 0;

            if (playerCurLevel % 3 != 0) return;
            _curModelIndex++;
            EventsManager.PlayerLevelIncrease();
            foreach (var model in modelLevels)
            {
                model.SetActive(false);
            }
            modelLevels[_curModelIndex].SetActive(true);
        });
            
    }
    private void DecrementPlayerLevel()
    {
        levelFiller.DOFillAmount(levelFiller.fillAmount - 0.35f, .2f).OnComplete(() =>
        {
            if (levelFiller.fillAmount <= 0)
            {
                playerLevelText.text="LEVEL "+ (--playerCurLevel);
                levelFiller.fillAmount = 1;
                if (CheckForGameFail()) return;
                
                 if (playerCurLevel < 3)
                        _curModelIndex = 0;
                 else if (playerCurLevel >= 3 && playerCurLevel < 6)
                        _curModelIndex = 1;
                 else if (playerCurLevel >= 6)
                        _curModelIndex = 2;
                
                foreach (var model in modelLevels)
                {
                    model.SetActive(false);
                }
                modelLevels[_curModelIndex].SetActive(true);
            }
           
        });
    }

    private void HideLevelFiller()
    {
        levelFiller.transform.parent.DOScale(Vector3.zero, .15f);
    }

    #endregion


    private bool CheckForGameFail()
    {
        if (playerCurLevel >= 1) return false;
        
        levelFiller.transform.parent.DOScaleY(0, .25f);
        EventsManager.GameLose();
        return true;
    }

    public int GetPlayerLevel() => playerCurLevel;
}
