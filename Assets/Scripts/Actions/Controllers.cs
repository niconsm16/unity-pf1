using System.Linq;
using Cinemachine;
using Managers;
using UI;
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
            LayerMask layer, 
            float initialHealth,
            CanvasController canvas)
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

            if (Input.GetKeyDown(KeyCode.LeftShift))
                UsePowerUp();

            void BusOnTarget()
            {
                var sight = Physics.Raycast(
                    transform.position,
                    transform.forward,
                    out var bus,
                    25f, layer);
                
                var target = sight 
                    ? $"Objetivo en la mira: " + 
                      $"{bus.collider.name}\n" + 
                      "Distancia: " + 
                      $"{decimal.Round((decimal)bus.distance * 6)} mts."
                    :"El objetivo no esta en la mira";
                
                canvas.SetLegendText(target);
                canvas.SetLegendColor(sight);
            }

            void UsePowerUp()
            {
                bool HavePowerUps() 
                    => GameManager.Instance.GetPowerUps().Count != 0;
                
                if (!HavePowerUps()) return;
                
                var lostHealth = initialHealth - GameManager.Instance.GetPlayerHealth();
                var actualPowerUp = GameManager.Instance.GetPowerUps().Last();
                
                var isUsable = actualPowerUp <= lostHealth;
                if (isUsable) GameManager.Instance.SetPlayerDamage(actualPowerUp, false);
                
                GameManager.Instance.SetPowerUps(false);
      
                Debug.Log(isUsable 
                    ? "El powerup se usó correctamente"
                    :"El powerup Se desperdició (tu daño no es lo suficientemente grande)");
                
                Debug.Log("powerup usado: " + actualPowerUp + "pts");
                Debug.Log("Energia actual (con/sin recuperación)" +GameManager.Instance.GetPlayerHealth());

                Debug.Log(!HavePowerUps() 
                    ? "No te quedan más powerups" 
                    : "Los pts del próximo powerup a usar es de: " + 
                      GameManager.Instance.GetPowerUps().Last());
            }
        }
    }
}