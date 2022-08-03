using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIElement uiElements;

        private void Start()
        {
            EventsManager.OnGameStart += DisableMainPanel;
            EventsManager.OnGameLose += EnableLosePanel;
            EventsManager.OnGameWin += EnableWinPanel;
            EventsManager.OnReachedEnd+= EnableEndPanel;
        }

        private void OnDisable()
        {
            EventsManager.OnGameStart -= DisableMainPanel;
            EventsManager.OnGameLose -= EnableLosePanel;
            EventsManager.OnGameWin -= EnableWinPanel;
            EventsManager.OnReachedEnd-= EnableEndPanel;
        }
        
        #region Event CallBacks

        private void DisableMainPanel()
        {
            uiElements.DisableMainPanel();
        }

        private void EnableLosePanel()
        {
            uiElements.EnableLosePanel();
        }
        private void EnableWinPanel()
        {
            uiElements.EnableWinPanel();
        }

        private void EnableEndPanel()
        {
            uiElements.EnableEndPanel();
        }

        #endregion
    }
