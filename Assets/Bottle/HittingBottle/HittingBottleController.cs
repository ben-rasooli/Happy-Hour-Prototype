using System.Collections;
using UnityEngine;

namespace Project
{
   public class HittingBottleController : MonoBehaviour
   {
      #region ------------------------------dependencies
      Transform _transform;
      #endregion

      #region ------------------------------interface
      public void SetOwnerCollider(Collider collider)
      {
         _ownerCollider = collider;
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
      }

      void OnTriggerEnter(Collider other)
      {
         if (other == _ownerCollider)
            return;

         if (other.gameObject.tag == "Character")
            other.gameObject.GetComponent<CharacterController>().TakeDamage();

         StopCoroutine(nameof(flyOverTime));
         Destroy(gameObject);
      }
      Collider _ownerCollider;
      #endregion

      #region ------------------------------details
      IEnumerator flyOverTime(Vector3 direction)
      {
         Vector3 startPos = _transform.position;
         Vector3 endPos = startPos + direction * 20;
         var startTime = Time.time;

         while (true)
         {
            var currentTime = Time.time - startTime;
            float t = currentTime * _speed;
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, _movingCurve.Evaluate(t));
            _transform.position = currentPos;
            yield return null;
         }
      }
      [SerializeField] float _speed;
      [SerializeField] AnimationCurve _movingCurve;
      #endregion
   }
}
