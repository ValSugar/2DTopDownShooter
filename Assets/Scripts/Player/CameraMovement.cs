using System;
using UnityEngine;

namespace Player
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _followSpeed;

        private Transform _transform;
        private Vector3 _offset;

        private void Awake()
        {
            _transform = transform;
            _offset = _transform.position - _target.position;
        }

        private void FixedUpdate()
        {
            var targetPosition = _target.position + _offset;
            _transform.position = Vector3.Lerp(_transform.position, targetPosition, _followSpeed * Time.fixedDeltaTime);
        }
    }
}

