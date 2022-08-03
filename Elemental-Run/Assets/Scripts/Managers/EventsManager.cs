using System;
using MoreMountains.NiceVibrations;


public class EventsManager : MonoSingleton<EventsManager>
{
    public static event Action OnGameStart;
    
    public static event Action OnGameWin;
    public static event Action OnGameLose;
    public static event Action OnPlayerBodyChanged;
    
    
    public static event Action OnReachedEnd;
    public static event Action OnFinalAttack;
    public static event Action OnNextLevel;
    public static event Action OnPlayerLevelIncrease;

    public static event Action OnNegativeEffect;
    public static event Action OnPositiveEffect;
    public static event Action OnThrow;
    public static event Action OnLevelUpPlayer;

    #region Haptic Callbacks

    public void Haptic(HapticTypes type)
    {
        MMVibrationManager.Haptic(type, false,true, this);
      
    }

    #endregion



    public static void GameStart()
    {
        OnGameStart?.Invoke();
    }
    
    public static void GameWin()
    {
        OnGameWin?.Invoke();
    }

   

    public static void GameLose()
    {
        OnGameLose?.Invoke();
    }

   
    public static void NextLevel()
    {
        print("Next Level");
        OnNextLevel?.Invoke();
    }

    public static void PlayerBodyChanged()
    {
        OnPlayerBodyChanged?.Invoke();
    }

    public static void PlayerLevelIncrease()
    {
        OnPlayerLevelIncrease?.Invoke();
    }

    public static void NegativeEffect()
    {
        OnNegativeEffect?.Invoke();
    }

    public static void PositiveEffect()
    {
        OnPositiveEffect?.Invoke();
    }


    public static void PlayerReachedEnd()
    {
        OnReachedEnd?.Invoke();
    }

    public static void FinalAttack()
    {
        OnFinalAttack?.Invoke();    
    }

    public static void Throw()
    {
        OnThrow?.Invoke();
    }

    public static void LevelUpPlayer()
    {
        OnLevelUpPlayer?.Invoke();
    }
}

