using UnityEngine;

namespace Project
{
   public class CharacterInput : MonoBehaviour
   {
      #region ------------------------------dependencies
      CharacterController _characterController;
      Transform _transform;
      #endregion

      #region ------------------------------Unity messages
      void Awake()
      {
         _characterController = GetComponent<CharacterController>();
         _transform = transform;
         camera = Camera.main;
      }

      void Update()
      {
         Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
         _characterController.Move(moveDirection);

         if (Input.GetMouseButtonUp(0))
         {
            Vector3 throwDirection = (getMousePositionOnGround() - _transform.position).normalized;
            // ensure it is a horizontal direction 
            throwDirection.y = 0.0f;
            _characterController.ThrowBottleStraight(throwDirection);
         }

         if (Input.GetMouseButtonUp(1))
         {
            Vector3 throwDirection = (getMousePositionOnGround() - _transform.position).normalized;
            // ensure it is a horizontal direction 
            throwDirection.y = 0.0f;
            _characterController.ThrowBottleArch(throwDirection);
         }
      }
      #endregion

      #region ------------------------------details
      Vector3 getMousePositionOnGround()
      {
         Plane ground = new Plane(Vector3.up, 0);
         Ray ray = camera.ScreenPointToRay(Input.mousePosition);
         float distanceToGround;
         ground.Raycast(ray, out distanceToGround);

         return ray.GetPoint(distanceToGround);
      }
      Camera camera;
      #endregion
   }
}
