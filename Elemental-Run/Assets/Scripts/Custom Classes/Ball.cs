using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody ballRb;
    [SerializeField] private ObstacleElement ballColor;

    [SerializeField] private MeshRenderer ballRenderer;

    [SerializeField] private GameObject destructionVfx;

    private bool _hitPlayer;
    private void Start()
    {
        Destroy(gameObject,2f);
    }


    public void ThrowBall(bool toHitPlayer,Vector3 player,float forceToThrow)
    {
        _hitPlayer = true;
        transform.parent = null;
        
        ballRb.isKinematic = false;

        var direction = (player - transform.position).normalized;
        
        ballRb.AddForce(direction* forceToThrow, ForceMode.Impulse);
        ballRb.AddTorque(Vector3.right* forceToThrow*2f,ForceMode.Impulse);
    }
    public void ThrowBall(float forceToThrow,Transform parent,bool toThrowStraight)
    {
       
        transform.parent = null;
        
        ballRb.isKinematic = false;

        if(!toThrowStraight)
            ballRb.AddForce(Vector3.forward* forceToThrow, ForceMode.Impulse);
        else
            ballRb.AddForce(parent.forward* forceToThrow, ForceMode.Impulse);
        
        ballRb.AddTorque(Vector3.right* forceToThrow*2f,ForceMode.Impulse);
    }

    public void SetBallColor(ObstacleElement newColor)
    {
        ballColor.color = newColor.color;
        ballColor.colorName = newColor.colorName;
        ballRenderer.material.color = ballColor.color;
    }

    public ElementName GetBallElement() => ballColor.colorName;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroyer") || other.gameObject.CompareTag("Finish"))
        {
            Instantiate(destructionVfx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


    public bool ToHitPlayer() => _hitPlayer;

}

