using DG.Tweening;
using UnityEngine;
using MoreMountains.NiceVibrations;


[System.Serializable]
public class UIElement 
{ 
        [SerializeField] private CanvasGroup mainPanel;
        [SerializeField] private CanvasGroup inGamePanel;
        [SerializeField] private CanvasGroup winPanel;
        [SerializeField] private CanvasGroup losePanel;

        [SerializeField] private RectTransform tapPanel;
        
        public void DisableMainPanel()
        {
                DOTween.To(() => mainPanel.alpha, x => mainPanel.alpha = x, 0, .15f).OnComplete(() => 
                        { mainPanel.gameObject.SetActive(false);});
        }

        public void EnableWinPanel()
        {
                DOTween.To(() => inGamePanel.alpha, x => inGamePanel.alpha = x, 0, 0f).OnComplete(() =>
                {
                        inGamePanel.gameObject.SetActive(false);
                        winPanel.gameObject.SetActive(true);
                });
                DOTween.To(() => winPanel.alpha, x => winPanel.alpha = x, 1, 1.5f);
                EventsManager.instance.Haptic(HapticTypes.Success);
                //# todo: tiny sauce (this is end  of game with win)
        }
        public void EnableLosePanel()
        {
                DOTween.To(() => inGamePanel.alpha, x => inGamePanel.alpha = x, 0, .05f).OnComplete(() =>
                {
                        inGamePanel.gameObject.SetActive(false);
                        losePanel.gameObject.SetActive(true);
                });
                DOTween.To(() => losePanel.alpha, x => losePanel.alpha = x, 1, .2f);
                EventsManager.instance.Haptic(HapticTypes.Failure);
                //# todo: tiny sauce (this is end  of game with loss)
        }

        public void EnableEndPanel()
        {
                DOTween.To(() => inGamePanel.alpha, x => inGamePanel.alpha = x, 0, .05f).OnComplete(() =>
                {
                        inGamePanel.gameObject.SetActive(false);
                        tapPanel.DOScale(Vector3.one, .15f);
                });
                
        }


}
