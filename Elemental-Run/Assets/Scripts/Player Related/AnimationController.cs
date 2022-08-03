using System.Collections;
using UnityEngine;


    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator playerAnimator;

        #region Animation Hashing
        private static readonly int GameStart = Animator.StringToHash("GameStart");
        private static readonly int Fail = Animator.StringToHash("Fall");
        private static readonly int Cheer = Animator.StringToHash("Cheer");
        private static readonly int Stop = Animator.StringToHash("Stop");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Throw = Animator.StringToHash("Throw");

        #endregion

        private void Start()
        {
            EventsManager.OnGameStart += StartMoveAnim;
            EventsManager.OnGameLose += FailAnim;
            EventsManager.OnPlayerBodyChanged += PlayCheerAnim;
            EventsManager.OnReachedEnd += PlayEndAnimation;
            EventsManager.OnNegativeEffect += PlayHitAnimation;
            EventsManager.OnThrow += PlayAttackAnimation;

        }

        private void OnDisable()
        {
            EventsManager.OnGameStart -= StartMoveAnim;
            EventsManager.OnGameLose -= FailAnim;
            EventsManager.OnPlayerBodyChanged -= PlayCheerAnim;
            EventsManager.OnReachedEnd -= PlayEndAnimation;
            EventsManager.OnNegativeEffect -= PlayHitAnimation;
            EventsManager.OnThrow -= PlayAttackAnimation;
        }


        #region Event Callbacks

        private void StartMoveAnim()
        {
            playerAnimator.SetTrigger(GameStart);
        }

        private void FailAnim()
        {
            playerAnimator.SetTrigger(Fail);
        }

        private void PlayCheerAnim()
        {
            playerAnimator.SetTrigger(Cheer);
        }

        private void PlayEndAnimation()
        {
            playerAnimator.SetTrigger(Stop);
        }
        private void PlayHitAnimation()
        {
            playerAnimator.SetTrigger(Hit);
        }
        
        #endregion

        public void PlayAttackAnimation()
        {
            playerAnimator.SetTrigger(Throw);
        }
        
    }
