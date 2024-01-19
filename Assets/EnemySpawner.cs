using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject whiteSpider;
    [SerializeField] private GameObject redSpider;
    [SerializeField] private GameObject greenSpider;
    [SerializeField] private GameObject blueSpider;

    [SerializeField] private float whiteSpiderInterval = 2.5f;
    [SerializeField] private float colorSpiderInterval = 8f;

    [SerializeField] private int whiteSpiderCount = 20;
    [SerializeField] private int colorfulSpiderCount = 10;
    
    private int totalEnemyCount;
    private int spawnedEnemyCount;
    void Start()
    {
        totalEnemyCount = whiteSpiderCount + colorfulSpiderCount;
        StartCoroutine(SpawnWhiteSpiders());
        StartCoroutine(SpawnColorfulSpiders());
    }

    private IEnumerator SpawnWhiteSpiders()
    {
        for (int i = 0; i < whiteSpiderCount; i++)
        {
            yield return new WaitForSeconds(whiteSpiderInterval);

            if (FindCollisions(transform.position) < 1)
            {
                Instantiate(whiteSpider, transform.position, Quaternion.identity);
                CheckDestroySpawner();
            }
        }
    }

    
    private IEnumerator SpawnColorfulSpiders()
    {
        for (int i = 0; i < colorfulSpiderCount; i++)
        {
            yield return new WaitForSeconds(colorSpiderInterval);

            if (FindCollisions(transform.position) < 1)
            {
                GameObject randomColorSpider = GetRandomColorSpider();
                Instantiate(randomColorSpider, transform.position, Quaternion.identity);
                CheckDestroySpawner();
            }
        }
    }

    
    private GameObject GetRandomColorSpider()
    {
        GameObject[] colorfulSpiders = { redSpider, greenSpider, blueSpider };
        return colorfulSpiders[Random.Range(0, colorfulSpiders.Length)];
    }

    private int FindCollisions(Vector3 pos)
    {
        Collider[] hits = Physics.OverlapSphere(pos, 50f);
        return hits.Length;
    }
    private void CheckDestroySpawner()
    {
        spawnedEnemyCount++;

        if (spawnedEnemyCount >= totalEnemyCount)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        // Any additional update logic can be added here if needed
    }
}
