using System;
using TMPro;
using UnityEngine;


    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private PlayerLevel playerLevel;
        private int _curScore;
        private int _curMultiplier = 1;
        
        private void Start()
        {
            _curScore = 0;
//            scoreText.text = _curScore.ToString();
            EventsManager.OnReachedEnd += ExplicitlyIdentifyMultiplier;
        }

        private void OnDisable()
        {
            EventsManager.OnReachedEnd -= ExplicitlyIdentifyMultiplier;
        }

        public int GetMultiplierValue() => _curMultiplier;
        private void IncrementScore()
        {
            scoreText.text = (++_curScore).ToString();
        }

       

        private void ExplicitlyIdentifyMultiplier()
        {
            _curMultiplier = playerLevel.GetPlayerLevel();
        }
        
    }
