using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour {

    public GameObject box;
    public GameObject FallBox;
    public Vector2 spawnValues;
    public float maxValuesRangeX;
    public int boxCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < boxCount; i++)
            {
                Vector2 spawnPosition = new Vector2(Random.Range(spawnValues.x, maxValuesRangeX), spawnValues.y);
                Quaternion spawnRotation = Quaternion.identity;
                if (i % 7 == 0)
                {
                    Instantiate(box, spawnPosition, spawnRotation);
                }
                else
                {
                    Instantiate(FallBox, spawnPosition, spawnRotation);
                }
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}
