using System;
using UnityEngine;


    public class PlayerParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem bodyChangeVfx;
        [SerializeField] private ParticleSystem fireBallHitVfx;
        [SerializeField] private ParticleSystem iceBallHitVfx;
        [SerializeField] private ParticleSystem waterBallHitVfx;
        [SerializeField] private ParticleSystem earthBallHitVfx;
        [SerializeField] private ParticleSystem levelUpVfx;
        [SerializeField] private ParticleSystem passedVfx;
        [Space]
        [SerializeField] private GameObject fireGlow;
        [SerializeField] private GameObject iceGlow;
        [SerializeField] private GameObject waterGlow;
        [SerializeField] private GameObject earthGlow;

        [SerializeField] private ElementName playerBody;


        [Header("Hands Glow")]
        
        [SerializeField] private HandVFX fireHand;
        [SerializeField] private HandVFX iceHand;
        [SerializeField] private HandVFX waterHand;
        [SerializeField] private HandVFX earthHand;

        [Header("PickUp Vfx")]
        [SerializeField]  private ParticleSystem icePickUp;
        [SerializeField]  private ParticleSystem waterPickUp;
        [SerializeField]  private ParticleSystem earthPickUp;
        [SerializeField] private ParticleSystem firePickUp;
        private void Start()
        {
            SetHandsGlow();
            EventsManager.OnPlayerBodyChanged += PlayBodyChangeVfx;
            EventsManager.OnPositiveEffect += PlayPassedVfx;
            EventsManager.OnPlayerLevelIncrease += PlayLevelUpVfx;
            EventsManager.OnReachedEnd += EnableEndVfx;
        }

        private void OnDisable()
        {
            EventsManager.OnPlayerBodyChanged -= PlayBodyChangeVfx;
            EventsManager.OnPositiveEffect -= PlayPassedVfx;
            EventsManager.OnPlayerLevelIncrease -= PlayLevelUpVfx;
            EventsManager.OnReachedEnd -= EnableEndVfx;
        }


        #region Event Callbacks

        private void PlayBodyChangeVfx()
        {
            bodyChangeVfx.Play();
             SetHandsGlow();
        }
        private void PlayPassedVfx()
        {
            passedVfx.Play();
        }
        private void PlayLevelUpVfx()
        {
            levelUpVfx.Play();
        }

        private void EnableEndVfx()
        {

            playerBody = GetComponent<BodyTypeManager>().GetBodyColor();
            switch (playerBody)
            {
               case ElementName.Fire:
                   fireGlow.SetActive(true);
                   break;
               case ElementName.Ice:
                   iceGlow.SetActive(true);
                   break;
               case ElementName.Water:
                   waterGlow.SetActive(true);
                   break;
               case ElementName.Earth:
                   earthGlow.SetActive(true);
                   break;
               default:
                   throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        public void PlayFireBallVFX()
        {
            fireBallHitVfx.Play();
        }
        public void PlayIceBallVFX()
        {
            iceBallHitVfx.Play();
        }
        public void PlayWaterBallVFX()
        {
            waterBallHitVfx.Play();
        }
        public void PlayEarthBallVFX()
        {
            earthBallHitVfx.Play();
        }

        public void StopEndVFX()
        {
            waterGlow.SetActive(false);
            iceGlow.SetActive(false);
            fireGlow.SetActive(false);
            earthGlow.SetActive(false);
        }

        public Transform GetEndParticle()
        {
            playerBody = GetComponent<BodyTypeManager>().GetBodyColor();
            switch (playerBody)
            {
                case ElementName.Fire:
                    return fireGlow.transform;
                    
                case ElementName.Ice:
                    return iceGlow.transform;
                    
                case ElementName.Water:
                    return waterGlow.transform;
                   
                case ElementName.Earth:
                    return earthGlow.transform;
                    
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetHandsGlow()
        {
            playerBody = GetComponent<BodyTypeManager>().GetBodyColor();
            switch (playerBody)
            {
                case ElementName.Fire:
                    fireHand.EnableVFX(true);
                    iceHand.EnableVFX(false);
                    waterHand.EnableVFX(false);
                    earthHand.EnableVFX(false);
                    break;
                case ElementName.Ice:
                    fireHand.EnableVFX(false);
                    iceHand.EnableVFX(true);
                    waterHand.EnableVFX(false);
                    earthHand.EnableVFX(false);
                    break;
                case ElementName.Water:
                    fireHand.EnableVFX(false);
                    iceHand.EnableVFX(false);
                    waterHand.EnableVFX(true);
                    earthHand.EnableVFX(false);
                    break;
                case ElementName.Earth:
                    fireHand.EnableVFX(false);
                    iceHand.EnableVFX(false);
                    waterHand.EnableVFX(false);
                    earthHand.EnableVFX(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
       
        public void PlaySuitableVfx(ElementName bodyElement)
        {
            switch (bodyElement)
            {
                case ElementName.Fire:
                   firePickUp.Play();
                    break;
                case ElementName.Earth:
                   earthPickUp.Play();
                    break;
                case ElementName.Water:
                    waterPickUp.Play();                    
                    break;
                case ElementName.Ice:
                   icePickUp.Play();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(bodyElement), bodyElement, null);
            }
        }
        
         
    }
