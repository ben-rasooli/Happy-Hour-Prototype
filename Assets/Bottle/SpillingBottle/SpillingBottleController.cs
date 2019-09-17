using System.Collections;
using UnityEngine;

namespace Project
{
    public class SpillingBottleController : MonoBehaviour
    {
        #region ------------------------------dependencies
        Transform _transform;
        [SerializeField] GameObject _spillPrefab;
        #endregion

        #region ------------------------------interface
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
            if (other.gameObject.tag == "Floor")
            {
                Instantiate(_spillPrefab, _transform.position, Quaternion.identity);
                StopCoroutine(nameof(flyOverTime));
                Destroy(gameObject);
            }
        }
        #endregion

        #region ------------------------------details
        IEnumerator flyOverTime(Vector3 direction)
        {
            Vector3 startPos = _transform.position;
            Vector3 endPos = startPos + direction * _archSize.x;
            endPos.y = -0.1f; // to make sure bottle reaches the ground
            float trajectoryHeight = _archSize.y;
            var startTime = Time.time;

            while (true)
            {
                var currentTime = Time.time - startTime;
                float t = currentTime * _speed;
                Vector3 currentPos = Vector3.Lerp(startPos, endPos, _movingCurve.Evaluate(t));

                // give a curved trajectory in the Y direction
                currentPos.y += trajectoryHeight * Mathf.Sin(Mathf.Clamp01(t) * Mathf.PI);

                _transform.position = currentPos;
                yield return null;
            }
        }
        [SerializeField] float _speed;
        [SerializeField] Vector2 _archSize;
        [SerializeField] AnimationCurve _movingCurve;
        #endregion
    }
}
