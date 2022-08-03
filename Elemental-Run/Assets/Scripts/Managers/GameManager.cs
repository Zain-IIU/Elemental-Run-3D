using UnityEngine;


    public class GameManager : MonoBehaviour
    {
        private static bool _gameHasStarted;
        public static bool PlayerHasDied;

        private void Start()
        {
            PlayerHasDied = false;
            _gameHasStarted = false;
        }
        //to be called from event 
        public void StartGame()
        {
            _gameHasStarted = true;
            EventsManager.GameStart();
        }

    }
