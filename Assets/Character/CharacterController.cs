using UnityEngine;

namespace Project
{
   public class CharacterController : MonoBehaviour
   {
      #region ------------------------------dependencies
      [SerializeField] CharacterSettings _settings;
      [SerializeField] GameObject _hittingBottlePrefab;
      [SerializeField] GameObject _spillingBottlePrefab;
      [SerializeField] Collider _collider;
      Rigidbody _rigidbody;
      Transform _transform;
      #endregion

      #region ------------------------------interface
      public void Move(Vector2 direction)
      {
         float speed = _settings.Speed * Time.deltaTime;
         var velocity = new Vector3(direction.x * speed, 0, direction.y * speed);
         _rigidbody.velocity = velocity;

         if (velocity.sqrMagnitude > 0.1)
            _rigidbody.MoveRotation(Quaternion.LookRotation(velocity, _transform.up));
      }

      public void ThrowBottleStraight(Vector3 direction)
      {
         var throwOrigin = _transform.position + _settings.ThrowOffset;
         var bottle = Instantiate(_hittingBottlePrefab, throwOrigin, Quaternion.identity).GetComponent<HittingBottleController>();
         bottle.SetOwnerCollider(_collider);
         bottle.Fly(direction);
      }

      public void ThrowBottleArch(Vector3 direction)
      {
         var throwOrigin = _transform.position + _settings.ThrowOffset;
         var bottle = Instantiate(_spillingBottlePrefab, throwOrigin, Quaternion.identity).GetComponent<SpillingBottleController>();
         bottle.Fly(direction);
      }

      public void TakeDamage()
      {
         if (_hp > 0)
            _hp--;

         print(_hp);
      }
      int _hp;
      #endregion

      #region ------------------------------Unity messages
      void Awake()
      {
         _rigidbody = GetComponent<Rigidbody>();
         _transform = transform;
      }

      private void Start()
      {
         _hp = _settings.InitalHitPoint;
      }
      #endregion
   }
}
