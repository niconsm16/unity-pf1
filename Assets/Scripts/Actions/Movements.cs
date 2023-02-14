using UnityEngine;

namespace Actions
{
    public class Movements
    {
        public static void Bus(float speed,Transform transform)
        {
            transform.position += 
                -transform.forward * (speed * Time.deltaTime);
        }

        public static void Enemy(
            Transform target, 
            Transform enemy, 
            float speed, 
            int action)
        {
            var delta = speed * Time.deltaTime;
            var distance = target.position - enemy.position;
            var newRotation = Quaternion.LookRotation(distance);

            enemy.rotation = 
                Quaternion.Lerp(enemy.rotation, newRotation, delta);

            switch (action)
            {
                case 4:
                    enemy.position += Vector3.forward * (delta * 0.8f);
                    break;
                case 5:
                    enemy.position += Vector3.forward * delta;
                    break;
                case 6:
                    enemy.position += Vector3.forward * (delta * 0.6f);
                    break;
                default:
                    return;
            }
        } 
    }
}