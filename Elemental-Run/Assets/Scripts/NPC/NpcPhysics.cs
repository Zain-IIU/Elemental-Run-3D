

    using UnityEngine;

    public class NpcPhysics : PhysicsBaseClass
    {
        [SerializeField] private float timeInAir;

        [SerializeField] private bool inAir;
        protected override void Update()
        {
           
        }
        private void ApplyForce(float forceAmount)
        {
            if (!IsGrounded) return;

            Velocity.y = Mathf.Sqrt(forceAmount * -2 * gravityAmount);
        }


        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("EnemyBall"))
            {
                ApplyForce(7f);
            }
        }
    }
