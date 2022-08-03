using System;
using System.Collections;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine;


    public class CollisionDetection : MonoBehaviour
    {
        [SerializeField] private BodyTypeManager playerTypeManager;

        [SerializeField] private PlayerParticles playerParticles;
        private bool _hasPassedFromGate;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Finish"))
            {
                EventsManager.PlayerReachedEnd();
            }
            
            if (other.TryGetComponent(out GateManager colorGate))
            {
                if (_hasPassedFromGate) return;
                _hasPassedFromGate = true;
                playerTypeManager.UpdateColor(colorGate.GetGateColor(),colorGate.GetSpriteColor());
                EventsManager.PlayerBodyChanged();
                EventsManager.instance.Haptic(HapticTypes.MediumImpact);
                other.enabled = false;
                other.transform.DOScaleY(-.1f, .3f).SetEase(Ease.InBack);
                StartCoroutine(nameof(ResetGatesManager));
            }
            
            if (other.TryGetComponent(out ObstacleManager obstacleManager))
            {
                if (obstacleManager.GetObstacleColor() == playerTypeManager.GetBodyColor())
                {
                    EventsManager.PositiveEffect();
                    return;
                }
                PlaySuitableVfx(obstacleManager.GetObstacleColor());
                EventsManager.NegativeEffect();
                EventsManager.instance.Haptic(HapticTypes.HeavyImpact);
            }
            
            if (other.gameObject.TryGetComponent(out Ball enemyFire))
            {
                if (enemyFire.GetBallElement() != playerTypeManager.GetBodyColor())
                {
                    other.enabled = false;
                    other.gameObject.SetActive(false);
                   PlaySuitableVfx(enemyFire.GetBallElement());
                    EventsManager.NegativeEffect();
                    EventsManager.instance.Haptic(HapticTypes.HeavyImpact);
                    return;
                }

                print("You can pass");
            }

            if (other.gameObject.TryGetComponent(out PickUpManager pickUpManager))
            {
                pickUpManager.gameObject.SetActive(false);
                pickUpManager.TakeAction(playerTypeManager,playerParticles);
                EventsManager.instance.Haptic(HapticTypes.HeavyImpact);
            }
        }

        private void PlaySuitableVfx(ElementName ballElement)
        {
            switch (ballElement)
            {
                case ElementName.Fire:
                    playerParticles.PlayFireBallVFX();
                    break;
                case ElementName.Earth:
                    playerParticles.PlayEarthBallVFX();
                    break;
                case ElementName.Water:
                    playerParticles.PlayWaterBallVFX();
                    break;
                case ElementName.Ice:
                    playerParticles.PlayIceBallVFX();
                    break;
                case ElementName.Normal:
                    print("Normal");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ballElement), ballElement, null);
            }
        }

        IEnumerator ResetGatesManager()
        {
            yield return new WaitForSeconds(1f);
            _hasPassedFromGate = false;
        }
       
    }
