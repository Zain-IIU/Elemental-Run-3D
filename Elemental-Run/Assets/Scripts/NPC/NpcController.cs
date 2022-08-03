using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Transform))]
[SelectionBase]
public class NpcController : MonoBehaviour
{
    [Header("Elemental NPC")] [Space]
    [SerializeField]
    private ObstacleColor enemyType;
    public ObstacleElement enemyColor;
    [SerializeField] private Animator animator;
    [SerializeField] private SkinnedMeshRenderer bodyRenderer;

    [Space] [Header("Attacking Player")] [SerializeField]
    private Ball ballPrefab;
     private BodyTypeManager _goal;
     private Transform _attackTarget;
    [SerializeField] private Transform checkPos;
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform detectedSign;
     private  Vector3 _lookAtGoal;
    [SerializeField]  private  Transform handPos;
     
    [Space]

    [SerializeField] private bool startAttacking;
    [SerializeField] private bool isDead;
    [SerializeField] private bool throwAttack;
    [SerializeField] private bool stopThrow;

    [Space]
    [Header("RagDoll")] 
    [SerializeField] private Rigidbody[] rb;
    [SerializeField] private Collider[] colliders;
    [SerializeField] private GameObject headVFX;
    
    #region Animation Hashing

     private static readonly int Throw = Animator.StringToHash("StartThrowing");
     [SerializeField]  private  Sensor sensor;
     private static readonly int Cheer = Animator.StringToHash("Cheer");

     #endregion



     [SerializeField] private GameObject pickUpPrefab;
     
   
     private void Start()
     {
         _goal = FindObjectOfType<BodyTypeManager>();
         _attackTarget = GameObject.FindGameObjectWithTag("HitTarget").transform;
         SetNpcColor();
         rb = GetComponentsInChildren<Rigidbody>();
         colliders = transform.GetChild(0).GetComponentsInChildren<Collider>();
         EnableRagDoll(false);
     }

     private void EnableRagDoll(bool toEnable)
     {
         detectedSign.DOScale(Vector3.zero, .15f);
         headVFX.SetActive(!toEnable);
         isDead = toEnable;
         animator.enabled = !toEnable;
         foreach (var rigidbody1 in rb)
         {
             rigidbody1.isKinematic = !toEnable;
             
             if(toEnable)
                 rigidbody1.AddForce(Vector3.forward*20f,ForceMode.Impulse);
         }

         foreach (var collider1 in colliders)
         {
             collider1.enabled = toEnable;
         }

         if (!toEnable) return;
         GetComponent<Collider>().enabled = false;
         EventsManager.PositiveEffect();
     }

     private void Update()
     {
         if (isDead) return;
         
         
         if (GameManager.PlayerHasDied && !stopThrow)
         {
             stopThrow = true;
             animator.SetBool(Throw,false);
         }

         if (sensor.DetectPlayer(checkPos.position, detectionRadius, playerLayer, _goal,enemyColor.colorName) && !startAttacking)
         {
             startAttacking = true;
             detectedSign.DOScale(Vector3.one, .15f);
         }

         else if(!sensor.DetectPlayer(checkPos.position, detectionRadius, playerLayer, _goal, enemyColor.colorName) )
         {
             if (startAttacking)
             {
                 startAttacking = false;
                 animator.SetBool(Throw,startAttacking);
             }

             if (sensor.isSameColor)
                 SameColorEffect();
             
                 
         }


         if(!startAttacking) return;

         LookAtPlayer();
         ChasePlayer();
     }

     private void SameColorEffect()
     {
         isDead = true;
         animator.SetTrigger(Cheer);
         transform.DOScaleY(0, .8f).SetEase(Ease.InBack).OnComplete(() =>
         {
             gameObject.SetActive(false);
             var pickUp = Instantiate(pickUpPrefab,transform.position,Quaternion.identity);
             pickUp.transform.DOLocalMoveY(0, 0).OnComplete(() =>
             {
                 pickUp.transform.DOJump(_attackTarget.position, 2.5f, 1, .5f)
                     .OnComplete(() =>
                     {
                         pickUp.gameObject.SetActive(false);
                         EventsManager.LevelUpPlayer();
                     });
             });
         });
     }
     private void SetNpcColor()
     {
         enemyColor.colorName = enemyType.elementName;
         enemyColor.color = enemyType.color;
         bodyRenderer.material.color = enemyColor.color;
     }

     private void ChasePlayer()
     {
         if (throwAttack) return;
        
         throwAttack = !throwAttack;
         animator.SetBool(Throw,true);

     }

    private void LookAtPlayer()
    {
        var position = _goal.transform.position;
        _lookAtGoal = new Vector3(position.x, 
            transform.position.y, 
            position.z);
        transform.LookAt(_lookAtGoal);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(checkPos.position, detectionRadius);
    }
    
    
    //to be called from animation event
    
    private void ThrowBallAtPlayer()
    {
        Ball ball = Instantiate(ballPrefab, handPos, true);
        ball.SetBallColor(enemyColor);
        ball.transform.DOScale(Vector3.one, 0);
        ball.transform.DOLocalRotate(Vector3.zero, 0);
        ball.transform.DOLocalMove(Vector3.zero, 0).OnComplete(() => { ball.ThrowBall(true,_attackTarget.position,50f); });
    }

    public ElementName GetNpcColor() => enemyColor.colorName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Ball ball))
        {
            if (ball.ToHitPlayer()) return;
            Destroy(ball.gameObject);
            EventsManager.instance.Haptic(HapticTypes.HeavyImpact);
            EnableRagDoll(true);
        }

        if (other.gameObject.TryGetComponent(out BodyTypeManager playerBody))
        {
            if(playerBody.GetBodyColor()==GetNpcColor()) return;
                
            EnableRagDoll(true);
        }
    }
}
