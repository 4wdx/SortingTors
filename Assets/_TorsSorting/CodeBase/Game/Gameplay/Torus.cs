using CodeBase.Configs;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.Gameplay
{
    public class Torus : MonoBehaviour
    {
        [field: SerializeField] public int Color { get; private set; }

        [SerializeField] private float _animTime;
        [SerializeField] private float _zxSpeed;
        [SerializeField] private TorsColors _colorMap;
        
        private MeshRenderer _meshRenderer;
        private MeshFilter _meshFilter;
        private Sequence _animation;
        private float _animProgress;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshFilter = GetComponent<MeshFilter>();
            _animation = DOTween.Sequence();
        }

        public void Initialize(Mesh usedMesh)
        {
            _meshRenderer.material.color = _colorMap.GetColor(Color);
            _meshFilter.mesh = usedMesh;
        }

        public void StartDrag(float yPos)
        {
            _animation.Kill();
            _animation = DOTween.Sequence();
            print("start animation");
            Vector3 targetPos = transform.position;
            targetPos.y = yPos;
            
            transform.DOMove(targetPos, _animTime);
        }

        public void StopDrag(Vector3 stopPosition)
        {
            _animation.Kill();
            _animation = DOTween.Sequence();
            print("stop animation");
            
            Vector3 tempPos = stopPosition;
            tempPos.y = transform.position.y;
            float time = Vector3.Distance(transform.position, tempPos) / _zxSpeed;
            
            _animation.Append(transform.DOMove(tempPos, time)).Append(transform.DOMove(stopPosition, _animTime));
        }
    }
}