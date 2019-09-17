using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class CharacterController : MonoBehaviour
    {
        #region ------------------------------dependencies
        [SerializeField] CharacterSettings _settings;
        [SerializeField] GameObject _hittingBottlePrefab;
        [SerializeField] GameObject _spillingBottlePrefab;
        [SerializeField] List<Collider> _colliders;
        Rigidbody _rigidbody;
        Transform _transform;
        #endregion

        #region ------------------------------interface
        public void Move(Vector3 direction)
        {
            if (_isStunned)
                return;

            float speed = _settings.Speed * Time.deltaTime;
            var velocity = direction * speed;
            _rigidbody.velocity = velocity;

            if (velocity.sqrMagnitude > 0.1)
                _rigidbody.MoveRotation(Quaternion.LookRotation(velocity, _transform.up));
        }

        public void Aim(Vector3 direction)
        {
            if (_isStunned)
                return;

            if (direction == Vector3.zero)
                return;

            _facing = direction;

            _rigidbody.MoveRotation(Quaternion.LookRotation(_facing, _transform.up));
        }

        public void Throw()
        {
            if (_isStunned)
                return;

            var throwOrigin = _transform.position + _settings.ThrowOffset;
            var bottle = Instantiate(_hittingBottlePrefab, throwOrigin, Quaternion.identity).GetComponent<HittingBottleController>();
            bottle.SetOwnerCollider(_colliders);
            bottle.Fly(_facing);
        }

        public void Toss()
        {
            if (_isStunned)
                return;

            var throwOrigin = _transform.position + _settings.ThrowOffset;
            var bottle = Instantiate(_spillingBottlePrefab, throwOrigin, Quaternion.identity).GetComponent<SpillingBottleController>();
            bottle.Fly(_facing);
        }

        public void TakeDamage()
        {
            if (_hp > 0)
                _hp--;

            print(gameObject.name + ": " + _hp);
        }
        int _hp;

        public void TakeHit(Vector3 velocity)
        {
            _rigidbody.AddForce(velocity, ForceMode.VelocityChange);
        }

        public void GetStunned()
        {
            _isStunned = true;
            _rigidbody.velocity = Vector3.zero;
            Invoke(nameof(removeStunned), _settings.StunDuration);
        }
        #endregion

        #region ------------------------------Unity messages
        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;
        }

        void Start()
        {
            _hp = _settings.InitalHitPoint;
        }
        #endregion

        #region ------------------------------details
        bool _isStunned;
        Vector3 _facing;

        void removeStunned()
        {
            _isStunned = false;
        }
        #endregion
    }
}
