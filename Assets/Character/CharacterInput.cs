using UnityEngine;
using XboxCtrlrInput;

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
        }

        void Start()
        {
            _aimingDirection = _transform.forward;
        }

        void Update()
        {
            var moveDirection = new Vector3(XCI.GetAxis(XboxAxis.LeftStickX, controller), 0, XCI.GetAxis(XboxAxis.LeftStickY, controller));
            _characterController.Move(moveDirection);
            
            // if a value exists
            if (XCI.GetAxis(XboxAxis.RightStickX, controller) != 0 || XCI.GetAxis(XboxAxis.RightStickY, controller) != 0)
                _aimingDirection = new Vector3(XCI.GetAxis(XboxAxis.RightStickX, controller), 0, XCI.GetAxis(XboxAxis.RightStickY, controller));
            
            _characterController.Aim(_aimingDirection.normalized);

            if (XCI.GetButtonDown(XboxButton.RightBumper, controller))
                _characterController.Throw();

            if (XCI.GetButtonDown(XboxButton.LeftBumper, controller))
                _characterController.Toss();
        }
        [SerializeField] XboxController controller;
        Vector3 _aimingDirection;
        #endregion

        #region ------------------------------details
        #endregion
    }
}
