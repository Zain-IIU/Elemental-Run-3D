using UnityEngine;

public class SwerveControl : MonoBehaviour
{
    private float _lastFrameFromPosX;
    private float _moveFactorX;
    
    public bool onScreenHold;
    
    public float MoveFactorX => _moveFactorX;

    public bool screenHold
    {
        get => onScreenHold;
        set => onScreenHold = value;
    }


    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFromPosX = Input.mousePosition.x;
            onScreenHold = true;
        } 
        else if (Input.GetMouseButton(0))
        {
            _moveFactorX = Input.mousePosition.x - _lastFrameFromPosX;
            _lastFrameFromPosX = Input.mousePosition.x;
        } 
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0f;
            onScreenHold = false;
        }
#endif

#if UNITY_ANDROID
        if (Input.touches.Length > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _lastFrameFromPosX = Input.GetTouch(0).position.x;
                onScreenHold = true;
            } 
            else if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                _moveFactorX = Input.GetTouch(0).position.x - _lastFrameFromPosX;
                _lastFrameFromPosX = Input.GetTouch(0).position.x;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                _moveFactorX = 0f;
                onScreenHold = false;
            }
        }
#endif

    }
}
