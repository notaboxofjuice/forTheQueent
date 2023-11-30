using UnityEngine;
public class FollowCam : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float Offset = 10f;
    private void LateUpdate()
    {
        Transform _target = target.transform;
        Vector3 _offset = new Vector3(0, Offset, Offset);
        transform.position = Vector3.Slerp(transform.position, _target.position + _offset, 0.1f);
        transform.LookAt(_target);
    }
}