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

            Vector3 RandomValue(float min, float max) => 
                Vector3.forward * (delta * Random.Range(min, max));

            switch (action)
            {
                case 4: enemy.position += RandomValue(0.4f, 0.8f);
                    break;
                case 5: enemy.position += RandomValue(0.7f, 1.2f);
                    break;
                case 6: enemy.position += RandomValue(0.3f, 0.6f);
                    break;
                default: return;
            }
        } 
    }
}