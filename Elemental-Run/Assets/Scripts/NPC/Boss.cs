using System;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine;

    public class Boss : MonoBehaviour
    {
        [SerializeField] private bool inAir;
        [SerializeField] private CharacterController controller;
        [SerializeField] private float moveSpeed;

        [SerializeField] private Animator bossAnimator;
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");
        [SerializeField] private ParticleSystem finalHitVfx;
        
        
        [Space] [SerializeField] private ScoreManager scoreManager;
        private void Update()
        {
            if (!inAir) return;

            MoveTillEnd();
        }

        private void MoveTillEnd()
        {
            controller.Move(new Vector3(0, 0, moveSpeed *Time.deltaTime));
        }

        

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("EnemyBall"))
            {
                Destroy(other.gameObject);
                inAir = true;
                EventsManager.FinalAttack();
                EventsManager.instance.Haptic(HapticTypes.HeavyImpact);
                bossAnimator.SetTrigger(Hit);
                finalHitVfx.Play();
            }

            if (!other.gameObject.TryGetComponent(out Multiplier multiplier)) return;
            
            if (multiplier.GetMultiplierValue() != scoreManager.GetMultiplierValue()) return;
            
            multiplier.EnableVFX();
            inAir = false;
            bossAnimator.SetTrigger(Die);
            transform.DOLocalMoveY(-0.5f, 0.5f).OnComplete(EventsManager.GameWin);
        }
        
       

    }
