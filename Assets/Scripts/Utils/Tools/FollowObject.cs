using System.Collections;
using System.Collections.Generic;
using Com.Eimin.Personnal.Scripts.Managers.VirtualsManagers;
using UnityEngine;

namespace Com.Eimin.Personnal.Scripts.Utils.Tools
{
    public class FollowObject : MonoBehaviour
    {
        public Transform transformToFollow;
        public Transform RotationToFollow;
        public bool FollowRotation = false;
        public bool LerpingRotation = false;
        public float LerpingCoef = 1;

        private Transform _transform;

        protected void Awake()
        {
            _transform = GetComponent<Transform>();
            if (RotationToFollow == null) RotationToFollow = transformToFollow;
        }

        protected void LateUpdate()
        {
            if (GameManager.instance.isWin) return;

            _transform.position = transformToFollow.position;
            if (FollowRotation)
            {

                if (LerpingRotation)
                {
                    _transform.rotation = Quaternion.Lerp(_transform.rotation, transformToFollow.rotation,
                        LerpingCoef * Time.fixedDeltaTime);
                    return;
                }

                _transform.rotation = RotationToFollow.rotation;
            }
        }

    }
}