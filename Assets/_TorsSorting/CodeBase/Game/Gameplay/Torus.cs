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

        public void SetSkin(SkinData skinData)
        {
            _meshRenderer.materials = skinData.Materials;
            _meshFilter.mesh = skinData.Mesh;

            if (Color == 0) return;
            
            foreach (Material material in _meshRenderer.materials) 
                material.color = _colorMap.GetColor(Color);
        }

        public void StartDrag(float yPos)
        {
            _animation.Kill();
            _animation = DOTween.Sequence();
            Vector3 targetPos = transform.position;
            targetPos.y = yPos;
            
            transform.DOMove(targetPos, _animTime);
        }

        public void StopDrag(Vector3 stopPosition)
        {
            _animation.Kill();
            _animation = DOTween.Sequence();
            
            Vector3 tempPos = stopPosition;
            tempPos.y = transform.position.y;
            float time = Vector3.Distance(transform.position, tempPos) / _zxSpeed;
            
            _animation.Append(transform.DOMove(tempPos, time)).Append(transform.DOMove(stopPosition, _animTime));
        }
    }
}