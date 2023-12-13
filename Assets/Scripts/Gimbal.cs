using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gimbal : MonoBehaviour
{
    private Vector2 _inputVector;
    [SerializeField] float _minHeight;
    [SerializeField] float _maxHeight;
    [SerializeField] float _smoothTime;
    private Vector3 _velocity = Vector3.zero;
    void Start()
    {
        transform.parent = null;
    }
    public void MoveCamera(InputAction.CallbackContext context)
    {
        _inputVector = context.ReadValue<Vector2>();
    }
}