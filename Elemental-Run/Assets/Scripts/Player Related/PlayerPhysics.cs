using UnityEngine;

public class PlayerPhysics : PhysicsBaseClass
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float jumpHeight;


    private static readonly int Land = Animator.StringToHash("Land"); 
    

    public void GravityForPlayer()
    {
        ApplyGravity();
        playerAnimator.SetBool(Land, IsGrounded);
    }


    private void MakePlayerJump()
    {
        if (!IsGrounded) return;

        Velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityAmount);
    }


}
