using UnityEngine;
using System.Collections.Generic;

public class NPC_spawner : MonoBehaviour
{
    [SerializeField] protected GameObject npcPrefab;
    [SerializeField] protected npc_type type;
    [SerializeField] protected float spawnRate;
    [SerializeField] protected int maxNpcs;
    [SerializeField] protected float minXCoordinate;
    [SerializeField] protected float maxXCoordinate;
    [SerializeField] protected float minYCoordinate;
    [SerializeField] protected float maxYCoordinate;
    void Start()
    {
        SpawnPattern();
    }

    protected virtual void SpawnPattern()
    {
        int howMany = Random.Range(maxNpcs / 2, maxNpcs);
        for (int i = 1; i <= howMany; i++)
        {
            float randomX = Random.Range(minXCoordinate, maxXCoordinate);
            float randomY = Random.Range(minYCoordinate, maxYCoordinate);
            GameObject clone = Instantiate(npcPrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
            SpriteRenderer sr = clone.GetComponentInChildren<SpriteRenderer>();

            if (sr != null)
            {
                sr.sortingLayerName = "Default";
                sr.sortingOrder = 10;
                sr.color = Color.white;
                sr.enabled = true;
            }
            else
            {
                Debug.LogError("NPC prefab has no SpriteRenderer!");
            }
            // StartCoroutine(spawnCooldown());
        }
        //protected virtual IEnumerator spawnCooldown()
        //{
        //        yield return new WaitForSeconds(spawnRate);
        //}
    }
}
