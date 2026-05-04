using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float followSpeed = 8.0f;
    [SerializeField] private float cameraZPosition = -10.0f;

    private void LateUpdate()
    {
        if(target == null)
        {
            return;
        }

        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, cameraZPosition);

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
