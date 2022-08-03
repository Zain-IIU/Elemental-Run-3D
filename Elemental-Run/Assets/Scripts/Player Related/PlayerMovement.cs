using UnityEngine;

[SelectionBase]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private float turningSpeed = 7f;    
        [SerializeField] private float horizontalSpeed;
        [SerializeField] private float lerpTime;
        [SerializeField] private float maxPositionX = 2.5f;
        [SerializeField] private SwerveControl swerve;
        [SerializeField] private Transform playerBody;
        [SerializeField] private float maxRot;
        //private properties
        private float _curSpeed;
        private float _curHorizontalSpeed;
        private float _horizontalValue;
        private float _yRot;
        private float _xMove;
        //-----------------
        private void Start()
        {
            _curSpeed = 0;
            _curHorizontalSpeed = 0;
            turningSpeed = 0;
            EventsManager.OnGameStart += StartMovement;
            EventsManager.OnGameWin += PlayerLost_Won;
            EventsManager.OnGameLose += PlayerLost_Won;
            EventsManager.OnReachedEnd += StopPlayer;
        }

        private void OnDisable()
        {
            EventsManager.OnGameStart -= StartMovement;
            EventsManager.OnGameWin -= PlayerLost_Won;
            EventsManager.OnGameLose -= PlayerLost_Won;
            EventsManager.OnReachedEnd -= StopPlayer;
        }

        public void HandleMovement()
        {
            GatherInput();
            HandleForwardMovement();
            HandleRotation(_horizontalValue);
            ClampMovement();
        }

        
        private void HandleForwardMovement()
        {
            if(GameManager.PlayerHasDied) return;
            transform.Translate(new Vector3(_horizontalValue, 0,
                _curSpeed * Time.deltaTime));
        }

        [SerializeField] private float rotationMultiplier;
        private void HandleRotation(float value)
        {
            if(GameManager.PlayerHasDied) return;
            var rotation = playerBody.transform.localRotation;
            _yRot = maxRot * value * rotationMultiplier ;
            
            _yRot = Mathf.Clamp(_yRot,-maxRot, maxRot);
            var normalRotation = Quaternion.Euler(0, _yRot, 0f);
            playerBody.transform.localRotation = Quaternion.Lerp(rotation, normalRotation, Time.deltaTime * turningSpeed);
        }
        

        private void ClampMovement()
        {
            var localPosition = transform.localPosition;
            localPosition.x = Mathf.Clamp(localPosition.x, -maxPositionX, maxPositionX);
            transform.localPosition = localPosition;
        }

        private void GatherInput()
        {
            if(GameManager.PlayerHasDied) return;
             _xMove = swerve.MoveFactorX * _curHorizontalSpeed; 
             
             _horizontalValue=  Mathf.Lerp(_horizontalValue, _xMove, lerpTime * Time.deltaTime);
        }



        #region Event Callbacks

        private void StartMovement()
        {
            turningSpeed =30f;
            _curSpeed = moveSpeed;
            _curHorizontalSpeed = horizontalSpeed;
        }

        private void StopPlayer()
        {
            maxPositionX = 0;
            GameManager.PlayerHasDied = true;
        }
        private void PlayerLost_Won()
        {
            GameManager.PlayerHasDied = true;
        }
        
        #endregion

    }
