using UnityEngine;
using UnityEngine.InputSystem;
public class Gimbal : MonoBehaviour
{
    [SerializeField] GameObject _camera;
    private Vector2 _dollyVector;
    private float _pedestal;
    [SerializeField] float _minHeight;
    [SerializeField] float _maxHeight;
    [SerializeField] float _smoothTime;
    private Vector3 _velocity = Vector3.zero;
    void Start()
    {
        transform.parent = null;
    }
    private void FixedUpdate()
    {
        Dolly();
        Pedestal();
    }
    private void Dolly()
    {
        Vector3 targetPosition = transform.position;
        targetPosition += new Vector3(_dollyVector.x, 0, _dollyVector.y);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }
    private void Pedestal()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.y += _pedestal;
        targetPosition.y = Mathf.Clamp(targetPosition.y, _minHeight, _maxHeight);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }
    public void DollyCamera(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        _dollyVector = context.ReadValue<Vector2>();
        _dollyVector *= -1;
    }
    public void PedestalCamera(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        _pedestal = context.ReadValue<float>();
    }
}