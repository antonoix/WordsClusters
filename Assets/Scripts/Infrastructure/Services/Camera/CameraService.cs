using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Camera
{
    public class CameraService : IInitializable
    {
        private readonly CameraServiceConfig _config;

        public CameraService(CameraServiceConfig config)
        {
            _config = config;
        }
        
        public void Initialize()
        {
            var cameraView = GameObject.Instantiate(_config.CameraViewPrefab);
            GameObject.DontDestroyOnLoad(cameraView);
        }
    }
}