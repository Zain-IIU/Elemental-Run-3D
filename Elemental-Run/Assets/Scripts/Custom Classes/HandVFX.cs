using UnityEngine;

[System.Serializable]
    public class HandVFX
    {
        [SerializeField] private GameObject leftHandVfx;
        [SerializeField] private GameObject rightHandVfx;

        public void EnableVFX(bool toSet)
        {
            leftHandVfx.SetActive(toSet);
            rightHandVfx.SetActive(toSet);
        }
    }
