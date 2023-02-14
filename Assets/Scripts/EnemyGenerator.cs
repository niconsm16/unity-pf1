using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemiesQuantity;
    [SerializeField] private Transform target;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Material[] shirts;
    [SerializeField] private Material[] hairs;
    [SerializeField] private Material[] pants;
    [SerializeField] private Material[] shoes;

    private void Awake()
    { Generate(); }

    private void Generate()
    {
        for (var i = 0; i <= enemiesQuantity; i++)
        {
            var enemy = enemyPrefab.GetComponent<Enemy>();
            enemy.target = target;
            Instantiate(enemy, RandomPosition(), Quaternion.identity);
        }
        RandomMaterials("ShirtColor", shirts);
        RandomMaterials("HairColor", hairs);
        RandomMaterials("PantsColor", pants);
        RandomMaterials("ShoesColor", shoes);
    }
    

    private static void RandomMaterials(string tagName, IReadOnlyList<Material> itemsList)
    {
        var randomList = GameObject.FindGameObjectsWithTag(tagName);
        
        foreach(var item in randomList)
        {
            item.GetComponent<SkinnedMeshRenderer>()
                .materials[0]
                .CopyPropertiesFromMaterial
                    (RandomMaterial(itemsList));
        }
    }
    

    private Vector3 RandomPosition()
    {
        var positionA = pointA.position;
        var positionB = pointB.position;
        
        return new Vector3(
            Random.Range(positionA.x, positionB.x), 0,
            Random.Range(positionA.z, positionB.z));
    }

    
    private static Material RandomMaterial(IReadOnlyList<Material> array)
    {
        var value = Random.Range(0, array.Count);
        return array[value];
    }
}
