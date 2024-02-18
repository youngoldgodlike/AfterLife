using Cysharp.Threading.Tasks;
using Movement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Character.Stealth
{
    public class DrawCircleStealth : MonoBehaviour
    {
        [SerializeField] private MineCharacterController _characterController;
        [SerializeField] private MyPlayer _player;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private LayerMask _obstructions;
        [SerializeField] private float _defaultRadius = 3.5f;
        [SerializeField] private float _runRadius, _moveRadius,_hideRadius;
        [SerializeField] private bool _isRender;
        [Range(0.01f, 1f)] [SerializeField] private float _step;

        private LineRenderer _lineRenderer;
        private bool _canChange = true;
        private float _localRadius = 3.5f;
        private SphereCollider _collider;

        private Vector3 position => transform.position;
        
        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _collider = GetComponent<SphereCollider>();
            DrawCircle(50, _localRadius);
        }

        private void Update()
        {
            if (!_isRender) {
                _lineRenderer.material.color = Color.clear;
            }
                
            if (!_canChange) return;
            
            _localRadius = Mathf.Lerp(_localRadius, _defaultRadius, _step);
            DrawCircle(50, _localRadius);
            

            if (Mathf.Approximately(_localRadius, _defaultRadius))
                _canChange = false;
        }

        private void ChangeColorTo(Color color) => _lineRenderer.material.color = color;
        
        public void ChangeRadius(float radius)
        {
            _canChange = true;
            _defaultRadius = radius;
        }

        private void DrawCircle(int steps, float radius)
        {
            _collider.radius = radius;
            _lineRenderer.positionCount = steps;

            for (int currentStep = 0; currentStep < steps; currentStep++)
            {
                var circumferenceProgress = (float) currentStep / steps;
                
                var currentRadian = circumferenceProgress * 2 * Mathf.PI;
                
                var xScaled = Mathf.Cos(currentRadian);
                var yScaled = Mathf.Sin(currentRadian);

                var x = xScaled * radius;
                var y = yScaled * radius;
                
                var currentPosition = new Vector3(x, y,0);
                
                _lineRenderer.SetPosition(currentStep, currentPosition);
            }
        }

        public void OnStayInGrass()
        {
            if (_characterController.IsMoving && _player.IsPressedMoveAxes) {
                ChangeRadius(_characterController.IsRun ? _runRadius : _moveRadius);
            }
            else
                ChangeRadius(_hideRadius);
        }
        
        private bool IsInSight(Transform target) {
            var dir = target.position - position;
            var distance = Vector3.Distance(position, target.position);
            var inSight = !Physics.Raycast(position, dir, distance, _obstructions);
            
            return inSight;
        }

        private async UniTask CheckingEnemyVisibility(Transform enemy) {
            float dist;
            do {
                await UniTask.WaitForSeconds(0.2f);
                dist = Vector3.Distance(enemy.position, position);

                if (IsInSight(enemy)) {
                    enemy.GetComponent<IEnemyTrigger>().Visit(transform);
                    ChangeColorTo(Color.red);
                    return;
                }
                
            } while (dist < _collider.radius + 0.7f);
        }
        
        private void OnTriggerEnter(Collider other) {
            if (!other.TryGetComponent<IEnemyTrigger>(out IEnemyTrigger enemyVisitor)) return;
            if (!IsInSight(other.transform)) {
                CheckingEnemyVisibility(other.transform).Forget();
                return;
            }
            
            ChangeColorTo(Color.red);
            enemyVisitor.Visit(transform);
        }

        private void OnTriggerExit(Collider other) {
            if (!other.TryGetComponent<IEnemyTrigger>(out IEnemyTrigger enemyVisitor)) return;

            ChangeColorTo(Color.yellow);
        }
    }
}
