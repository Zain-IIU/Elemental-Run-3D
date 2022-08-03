using DG.Tweening;
using TMPro;
using UnityEngine;



    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI curLevelText;
        private int _curIndex;

        private void Awake()
        {
            SetLevelText();
        }

        
        private void SetLevelText()
        {
            _curIndex = 1;
            if (PlayerPrefs.HasKey("level"))
            {
                print("index not found");
                _curIndex = PlayerPrefs.GetInt("level");
            }
            else
                PlayerPrefs.SetInt("level",_curIndex);
            
            curLevelText.text = "LEVEL # " + _curIndex;
        }

        public void NewLevel()
        {
            _curIndex++;
            print(_curIndex);
            PlayerPrefs.SetInt("level",_curIndex);
            print(PlayerPrefs.GetInt("level"));
        }
        
       
    }
