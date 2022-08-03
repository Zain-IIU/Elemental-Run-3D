using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
   [SerializeField] private Transform hitPoint;

   [SerializeField] private float sensingDistance;

   [SerializeField] private LayerMask enemyLayer;

   [SerializeField] private Vector3 sensorSize;
   
   RaycastHit _mHit;

   private bool _isDetected;
   private bool _hasThrown;
    
   [SerializeField] private BodyTypeManager playerTypeManager;
   
   
   private void Update()
   {
       DetectWall();
   }

   private void DetectWall()
   {
       _isDetected = Physics.BoxCast(hitPoint.position, sensorSize, Vector3.forward, out _mHit, Quaternion.identity,
           sensingDistance,enemyLayer);

       if (!_isDetected) return;
       var obstacleColor=_mHit.collider.gameObject.GetComponent<ObstacleManager>();
       var npcColor=_mHit.collider.gameObject.GetComponent<NpcController>();

       if (obstacleColor)
       { if (obstacleColor.GetObstacleColor() == playerTypeManager.GetBodyColor())
           {
               if(_hasThrown) return;
        
               _hasThrown = true;
               Shoot();
               StartCoroutine(nameof(ResetThrow),2f);
           }
       }

       if (npcColor)
       {
           if (npcColor.GetNpcColor() == playerTypeManager.GetBodyColor()) return;
           
           if(_hasThrown) return;
        
           _hasThrown = true;
           Shoot();
           StartCoroutine(nameof(ResetThrow),1f);
       }
   }


   private void Shoot()
   {
       EventsManager.Throw();
   }

   private void OnDrawGizmos()
   {
       Gizmos.DrawRay(hitPoint.position,Vector3.forward*sensingDistance);
   }

   IEnumerator ResetThrow(float time)
   {
       yield return new WaitForSeconds(time);
       _hasThrown = false;
   }
   
}
