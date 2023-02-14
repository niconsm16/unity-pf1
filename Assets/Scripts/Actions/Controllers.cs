using Cinemachine;
using UnityEngine;

namespace Actions
{
    public class Controllers
    {
        private static int _cameraSet = 0;
        public static void Camera(CinemachineVirtualCamera[] cameras)
        {
            if (!Input.GetKeyDown(KeyCode.LeftControl) &&
                !Input.GetKeyDown(KeyCode.RightControl)) return;
            
            var totalCameras = cameras.Length - 1;
            
            _cameraSet =  
                _cameraSet < totalCameras
                    ? _cameraSet + 1 : 1;
            
            foreach (var camera in cameras)
                camera.gameObject.SetActive(false);
            
            cameras[_cameraSet].gameObject.SetActive(true);
        }
        
        public static void Player(float velocity, float rotate, Transform transform)
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var forward = transform.forward;
            var time = Input.GetKey(KeyCode.RightShift) 
                ? Time.deltaTime * (velocity * 1.5f)
                : Time.deltaTime * velocity;
            
            var rotation =
                new Vector3(0, horizontal, 0)
                    .normalized * rotate;

            transform.position +=
                forward * (vertical * time);
            
            transform.rotation *=
                Quaternion.Euler(rotation);
        }
    }
}