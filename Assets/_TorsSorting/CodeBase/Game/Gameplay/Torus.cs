using System.Collections;
using CodeBase.Configs;
using UnityEngine;

namespace CodeBase.Game.Gameplay
{
    public class Torus : MonoBehaviour
    {
        [field: SerializeField]public int Color { get; private set; }

        [SerializeField] private float _animTime;
        [SerializeField] private float _followSpeed;
        [SerializeField] private TorsColors _colorMap;
        
        private MeshRenderer _meshRenderer;
        private Vector3 _startPosition;
        private float _animProgress;

        private void Awake()
        {
            _startPosition = transform.position;
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void Initialize(int color)
        {
            Color = color;
            _meshRenderer.material.color = _colorMap.Colors[color];
        }

        public void StartDrag(Transform followingPoint)
        {
            _startPosition = transform.position;
            StopAllCoroutines();
            StartCoroutine(StartDragAnim(followingPoint));
        }

        public void StopDrag(Vector3 stopPosition)
        {
            StopAllCoroutines();
            StartCoroutine(StopDragAnim(stopPosition));
        }

        public void StopDrag()
        {
            StopAllCoroutines();
            StartCoroutine(StopDragAnim(_startPosition));
        }

        private IEnumerator StartDragAnim(Transform followingPoint)
        {
            yield return null;
            Vector3 targetPos = followingPoint.position;
            targetPos.x = transform.position.x;
            targetPos.z = transform.position.z;
            
            float time = _animProgress * _animTime;
            while (time < _animTime)
            {
                time += Time.deltaTime;
                _animProgress = time / _animTime;
                transform.position = Vector3.Lerp(_startPosition, targetPos, _animProgress);
                yield return null;
            }
            _animProgress = 1;
            transform.position = targetPos;
            
            while (Vector3.Distance(transform.position, followingPoint.position) > 0.05f)
            {
                transform.position = Vector3.Lerp(transform.position, followingPoint.position, Time.deltaTime * _followSpeed);
                yield return null;
            }

            while (true)
            {
                transform.position = followingPoint.position;
                yield return null;
            }
        }

        private IEnumerator StopDragAnim(Vector3 targetPos)
        {
            yield return null;
            Vector3 startPos = transform.position;
            Vector3 upPos = targetPos;
            upPos.y = transform.position.y;
            
            float time = _animTime * _animProgress;
            while (time > 0)
            {
                time -= Time.deltaTime;
                _animProgress = time / _animTime;

                if (_animProgress >= 0.5f) // 1-0.5
                    transform.position = Vector3.Lerp(upPos, startPos, (_animProgress - 0.5f) * 2);
                else
                    transform.position = Vector3.Lerp(targetPos, upPos, _animProgress * 2);

                yield return null;
            }
            _animProgress = 0;
            transform.position = targetPos;
        }
    }
}