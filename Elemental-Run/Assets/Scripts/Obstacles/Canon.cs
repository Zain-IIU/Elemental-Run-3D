using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private Sensor sensor;
    [SerializeField] private Transform checkPos;
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask playerLayer;
     private BodyTypeManager _playerGoal;
    [SerializeField] private Transform attackBallSpawnPoint;
    [Header("Element")] [Space] [SerializeField]
    private MeshRenderer[] canonMesh;
    [SerializeField]
    private ObstacleColor enemyType;
    public ObstacleElement enemyColor;
    [SerializeField] private Ball attackBall;
    

    [SerializeField] private float timeBwShots;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(checkPos.position, detectionRadius);
    }

    

    private void Awake()
    { 
        _playerGoal = FindObjectOfType<BodyTypeManager>();
        SetCanonColor();
    }

    private void Start()
    {
        StartCoroutine(nameof(StartShooting));
    }
    
    
    private void Update()
    {
        if (GameManager.PlayerHasDied)
        {
            StopAllCoroutines();
            return;
        }

        if (sensor.DetectPlayer(checkPos.position, detectionRadius, playerLayer, _playerGoal,
            enemyColor.colorName)) return;
        
        if (sensor.hasDetected || !sensor.isSameColor) return;
        sensor.hasDetected = true;
        EventsManager.PositiveEffect();
    }

    IEnumerator StartShooting()
    {
        while (true)
        {
            ShootTheBall();
            yield return new WaitForSeconds(timeBwShots);
        }
        // ReSharper disable once IteratorNeverReturns
    }
    
    private void ShootTheBall()
    {
        Ball ball = Instantiate(attackBall, attackBallSpawnPoint, true);
        ball.gameObject.SetActive(false);
        ball.SetBallColor(enemyColor);
        ball.transform.DOScale(Vector3.one *3f, 0);
        ball.transform.DOLocalRotate(Vector3.zero, 0);
        ball.transform.DOLocalMove(Vector3.zero, 0).OnComplete(() =>
        {
            ball.gameObject.SetActive(true);
            ball.ThrowBall(40f,attackBallSpawnPoint,true);
        });
    }

    private void SetCanonColor()
    {
        enemyColor.colorName = enemyType.elementName;
        enemyColor.color = enemyType.color;
        foreach (var bodyRenderer in canonMesh)
        {
            bodyRenderer.material.color = enemyColor.color;
        }
    }
}
