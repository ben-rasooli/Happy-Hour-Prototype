using UnityEngine;
using UnityEditor;

namespace Project
{
    [CustomEditor (typeof (CharacterDrunkController))]
    public class ChacterDrunkControllerEditor : Editor
    {
        private CharacterDrunkController _cTarget;

        private void OnSceneGUI()
        {
            _cTarget = (CharacterDrunkController)target;
            if (_cTarget.ShowCone)
            {
                float unitCircle = Mathf.Atan2(_cTarget.transform.forward.z, _cTarget.transform.forward.x) + (Mathf.Deg2Rad * (_cTarget.MaxDeviationAmount * 2) / 2f);
                Vector3 from = new Vector3(Mathf.Cos(unitCircle), 0f, Mathf.Sin(unitCircle));

                Handles.color = Color.red;
                Handles.DrawSolidArc(_cTarget.transform.position, Vector3.up, from, _cTarget.MaxDeviationAmount * 2, 3f);
            }
        }
    }
}