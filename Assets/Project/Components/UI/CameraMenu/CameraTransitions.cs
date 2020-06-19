using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

namespace TankWars.UI
{
    public class CameraTransitions : MonoBehaviour
    {
        public List<Transform> CameraPositions;
        TweenerCore<Vector3, Vector3, VectorOptions> _currentMovingTween;
        TweenerCore<Quaternion, Vector3, QuaternionOptions> _currentRotatingTween;

        public void Move(int index)
        {
            if (index < CameraPositions.Count)
            {
                _currentMovingTween = transform.DOMove(CameraPositions[index].position, 1f).SetEase(Ease.InOutQuad).Play();
                _currentRotatingTween = transform.DORotate(CameraPositions[index].eulerAngles, 1f).SetEase(Ease.InOutQuad).Play();
            }
        }

        private void OnDrawGizmos()
        {
            if (CameraPositions != null)
            {
                foreach(Transform t in CameraPositions)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireSphere(t.position, 0.5f);
                }
            }
        }
    }
}