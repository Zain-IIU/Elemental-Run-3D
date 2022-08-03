using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    [SerializeField] private Transform target;

    [SerializeField] private float followSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position =
            Vector3.Lerp(transform.position, (target.position + offset), Time.deltaTime * followSpeed);
    }
}
