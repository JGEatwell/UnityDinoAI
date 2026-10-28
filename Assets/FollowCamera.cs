
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset = new Vector3(0f,10f,0f);
    private Transform cam;

    private void LateUpdate()
    {
        if (cam == null && Camera.main != null)
            cam = Camera.main.transform;

        if (cam == null)
            return;

        transform.SetPositionAndRotation(cam.position + offset, Quaternion.identity);
    }
}
