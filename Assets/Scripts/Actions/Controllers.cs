using Cinemachine;
using UnityEngine;

namespace Actions
{
    public class Controllers
    {
        private static int _cameraSet;
        public static void Camera(CinemachineVirtualCamera[] cameras)
        {
            if (!Input.GetKeyDown(KeyCode.LeftControl) &&
                !Input.GetKeyDown(KeyCode.RightControl)) return;
            
            var totalCameras = cameras.Length - 1;
            
            _cameraSet = _cameraSet < totalCameras
                    ? _cameraSet + 1 : 1;
            
            foreach (var camera in cameras)
                camera.gameObject.SetActive(false);
            
            cameras[_cameraSet].gameObject.SetActive(true);
        }

        public static void Player(
            float velocity, 
            float rotate, 
            Transform transform, 
            LayerMask layer)
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

            if (Input.GetKeyDown(KeyCode.Backspace))
                BusOnTarget();

            void BusOnTarget()
            {
                var mira = Physics.Raycast(
                    transform.position,
                    transform.forward, 
                    out var bus,
                    25f, layer);

                if (mira) Debug.Log
                    ($"Target en la mira: " + 
                    $"{bus.collider.name}\n" + 
                    "Distancia: " +
                    $"{decimal.Round((decimal)bus.distance * 6)} mts.");
                else Debug.Log
                    ("El objetivo no está en la mira");
            }
        }
    }
}