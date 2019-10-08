using UnityEngine;
using System;

namespace Project
{
    public class CharacterDrunkController : MonoBehaviour
    {
        #region --------------------------dependencies
        [SerializeField] private float _maxDeviationAmount;
        public float MaxDeviationAmount { get { return _maxDeviationAmount; } } 

        private CharacterController _characterController;

        private Transform _transform;
        public Transform Transform { get { return _transform; } }
        #endregion

        #region --------------------------interfaces
        public void Aim(Vector3 direction)
        {
            _characterController.Aim(DeviateAim(direction));
        }
        #endregion

        #region --------------------------Unity Messages
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _transform = transform;
        }
        #endregion

        #region --------------------------details
        private Vector3 DeviateAim(Vector3 direction)
        {
            Vector3 newDirection = Vector3.zero;

            float unitCircle = Mathf.Atan2(direction.z, direction.x);
            float newUnitCircle = unitCircle + (Mathf.Deg2Rad * UnityEngine.Random.Range(-1f, 1f) * _maxDeviationAmount);

            newDirection.x = Mathf.Cos(newUnitCircle);
            newDirection.z = Mathf.Sin(newUnitCircle);

            return newDirection;
        }
        #endregion

        #region --------------------------debug
        [Header("Debug")]
        [SerializeField] private bool _showCone;
        public bool ShowCone { get { return _showCone; } }
        #endregion
    }
}