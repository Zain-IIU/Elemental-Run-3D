using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Player_Related
{ public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        
        private void Update()
        {
            playerMovement.HandleMovement();
        }


       
    }
}