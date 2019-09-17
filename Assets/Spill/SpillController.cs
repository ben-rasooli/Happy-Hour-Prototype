using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class SpillController : MonoBehaviour
    {
        void OnEnable()
        {
            Destroy(gameObject, _lifeDuration);
        }
        [SerializeField] float _lifeDuration;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Character")
                other.GetComponent<CharacterController>().GetStunned();
        }
    }
}
