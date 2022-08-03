using System;
using UnityEngine;


    public class Multiplier : MonoBehaviour
    {
        [SerializeField] private int multiplyValue;
        private ParticleSystem _confettiVFX;

        private void Awake()
        {
            _confettiVFX = GetComponentInChildren<ParticleSystem>();
        }

        public int GetMultiplierValue() => multiplyValue;
        public void SetMultiplier(int val) => multiplyValue = val;

        public void EnableVFX() => _confettiVFX.Play();


    }
