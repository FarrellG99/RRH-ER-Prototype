using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PlayerDistanceSpawnlevelPart = 200f;
    [SerializeField] private Transform levelPartStart;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private PlayerController player;

    private Vector3 lastEndPosition;

    private void Awake()
    {
        lastEndPosition = levelPartStart.Find("EndPos").position;
    }

    private void Update()
    {
        if (Vector3.Distance(player.GetPosition(), lastEndPosition) < PlayerDistanceSpawnlevelPart)
        {
            //Spawn Another Level
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        int testRand = Random.Range(0, levelPartList.Count);
        Debug.Log(testRand);
        Transform choosenLevelPart = levelPartList[testRand];
        Transform lastLevelPartTransform = SpawnLevelPart(choosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPos").position;
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
