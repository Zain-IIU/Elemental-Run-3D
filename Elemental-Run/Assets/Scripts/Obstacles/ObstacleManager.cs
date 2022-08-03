using System;
using System.Collections;
using DG.Tweening;
using Player_Related;
using UnityEngine;


public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private ObstacleType type;
    [SerializeField] private ObstacleColor obstacleColor; 
    private MeshRenderer[] _renderers;
    public ObstacleElement gateColor;
    private Rigidbody[] _rb;

    
    void Start()
    {
        
        _renderers = GetComponentsInChildren<MeshRenderer>();
        if(type!=ObstacleType.Path)
            SetGateColor();
        if(type==ObstacleType.Wall)
            _rb = GetComponentsInChildren<Rigidbody>();
    }
    private void SetGateColor()
    {
        gateColor.color = obstacleColor.color;
        gateColor.colorName = obstacleColor.elementName;
        foreach (var mesh in _renderers)
        {
            mesh.material.color = gateColor.color;
        }
    }
    

    public ElementName GetObstacleColor() => obstacleColor.elementName;

    private void DestroyWall()
    {
        if (_rb.Length==0) return;
        foreach (var rigidBody in _rb)
        {
            rigidBody.isKinematic = false;
            rigidBody.AddForce(Vector3.forward*20f,ForceMode.Impulse);
        }

        GetComponent<Collider>().enabled = false;
    }

    #region Collision Region for wall obstacle

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Ball ball))
        {
            if (type != ObstacleType.Wall) return;
            
            if(ball.GetBallElement()!=GetObstacleColor()) return;
            EventsManager.PositiveEffect();
            Destroy(other.gameObject); 
            DestroyWall();
        }
        if (other.gameObject.TryGetComponent(out PlayerController player))
        {
            
            if (type != ObstacleType.Wall) return;
            
            DestroyWall();
        }
    }

    #endregion
   
}
