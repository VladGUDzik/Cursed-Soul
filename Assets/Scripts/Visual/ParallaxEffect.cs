using UnityEngine;

namespace Visual
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] private float parallaxSpeedX;
        [SerializeField] private float parallaxSpeedY;

        private Transform _cameraTransform;
        private float _startPositionX, _startPositionY;
        private float _spriteSizeX;

        private void Start()
        {
            if (Camera.main != null) _cameraTransform = Camera.main.transform;
            _startPositionX = transform.position.x;
            _startPositionY = transform.position.y;
            _spriteSizeX = GetComponentInChildren<SpriteRenderer>().bounds.size.x;//
        }

        private void Update()
        {
            CheckDist();
            Loop();
        }
        
        private void CheckDist()
        {
            var relativeDistance = _cameraTransform.position.x * parallaxSpeedX;
            var relativeDistanceY = _cameraTransform.position.y * parallaxSpeedY;

            transform.position = new Vector3(_startPositionX + relativeDistance, relativeDistanceY + _startPositionY,
                transform.position.z);
        }

        private void Loop()
        {
            var relativeCameraDistance = _cameraTransform.position.x * (1 - parallaxSpeedX);
            if (relativeCameraDistance > _startPositionX + _spriteSizeX)
            {
                _startPositionX += _spriteSizeX;
            }
            else if (relativeCameraDistance < _startPositionX - _spriteSizeX)
            {
                _startPositionX -= _spriteSizeX;
            }
        }
    }
}