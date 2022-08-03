using Newtonsoft.Json.Bson;
using UnityEngine;
[System.Serializable]
    
public class CameraElement
    {
        [SerializeField] private GameObject followCam;
        [SerializeField] private GameObject startCam;
        [SerializeField] private GameObject winCam;
        [SerializeField] private GameObject loseCam;
        [SerializeField] private GameObject endCam;
        [SerializeField] private GameObject hitCam;
        
        public void StartFollowCam()
        {
            followCam.SetActive(true);
            startCam.SetActive(true);
        }

        public void EnableLoseCam()
        {
            followCam.SetActive(false);
            loseCam.SetActive(true);
        }
        public void EnableWinCam()
        {
            followCam.SetActive(false);
            winCam.SetActive(true);
        }
        public void EnableEndCam()
        {
            followCam.SetActive(false);
            endCam.SetActive(true);
        }

        public void EnableHitCam()
        {
            endCam.SetActive(false);
            hitCam.SetActive(true);
        }
    }
