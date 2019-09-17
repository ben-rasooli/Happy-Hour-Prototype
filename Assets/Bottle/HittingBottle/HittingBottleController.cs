using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class HittingBottleController : MonoBehaviour
    {
        #region ------------------------------dependencies
        Transform _transform;
        Rigidbody _rigidbody;
        #endregion

        #region ------------------------------interface
        public void SetOwnerCollider(List<Collider> colliders)
        {
            _ownerColliders = colliders;
        }

        public void Fly(Vector3 direction)
        {
            StartCoroutine(nameof(flyOverTime), direction);
        }
        #endregion

        #region ------------------------------Unity messages
        void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (_ownerColliders.Contains(other))
                return;

            if (other.gameObject.tag == "Character")
            {
                var Character = other.gameObject.GetComponent<CharacterController>();
                Character.TakeHit(_rigidbody.velocity * _hitForceSize);
                Character.TakeDamage();
            }

            StopCoroutine(nameof(flyOverTime));
            Destroy(gameObject);
        }
        List<Collider> _ownerColliders;
        [SerializeField] float _hitForceSize;
        #endregion

        #region ------------------------------details
        IEnumerator flyOverTime(Vector3 direction)
        {
            Vector3 startPos = _transform.position;
            Vector3 endPos = startPos + direction * 20;
            var startTime = Time.time;

            while (true)
            {
                _rigidbody.AddForce(direction * _speed, ForceMode.VelocityChange);
                yield return null;
            }
        }
        [SerializeField] float _speed;
        #endregion
    }
}
