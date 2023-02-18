using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public class Arbitrary
    {
        public static void Materials(string tagName, IReadOnlyList<Material> itemsList)
        {
            var randomList = GameObject.FindGameObjectsWithTag(tagName);
            
            foreach(var item in randomList)
            {
                item.GetComponent<SkinnedMeshRenderer>()
                    .materials[0]
                    .CopyPropertiesFromMaterial
                        (Material(itemsList));
            }
        }

        public static Vector3 Position(Transform pointA, Transform pointB)
        {
            var positionA = pointA.position;
            var positionB = pointB.position;
            
            return new Vector3(
                Random.Range(positionA.x, positionB.x), 0,
                Random.Range(positionA.z, positionB.z));
        }

        public static Material Material(IReadOnlyList<Material> array)
        {
            var value = Random.Range(0, array.Count);
            return array[value];
        }   
    }
}