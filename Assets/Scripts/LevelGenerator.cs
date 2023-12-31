using NarvalDev.Runner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 10f;
    private const float PLAYER_DISTANCE_SPAWN_BACKGROUND_PART = 1000f;

    [SerializeField] private Transform backgroundParent;
    [SerializeField] private Transform backgroundPart;

    [SerializeField] private Transform levelPartsParent;
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartList;


    [SerializeField] private Player player;
    private Vector3 lastEndPosition;

    private Vector3 lastEndBackgroundPosition;

    private LevelPart currentLevelPart;

    

    private void Awake()
    {

        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        lastEndBackgroundPosition = backgroundPart.Find("EndPosition").position;

        //TODO Refactor this
        currentLevelPart = levelPartList[0].GetComponent<LevelPart>();

        int startingSpawnLevelParts = 5;
        int startingBackgroundParts = 2;
        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }
        for (int i = 0; i < startingBackgroundParts; i++)
        {
            SpawnBackgroundPart();
        }
    }

    private void Update()
    {
        
        if (Vector3.Distance(player.GetPosition(), lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            SpawnLevelPart();
            Destroy(levelPartsParent.GetChild(0).gameObject);

        }
        if (Vector3.Distance(player.GetPosition(), lastEndBackgroundPosition) < PLAYER_DISTANCE_SPAWN_BACKGROUND_PART)
        {
            SpawnBackgroundPart();
            Destroy(backgroundParent.GetChild(0).gameObject);

        }
    }
    private void SpawnBackgroundPart()
    {
        Transform lastBackgroundPartTransfrom = SpawnPart(backgroundPart,lastEndBackgroundPosition,backgroundParent);
        lastEndBackgroundPosition = lastBackgroundPartTransfrom.Find("EndPosition").position;
    }
    private void SpawnLevelPart()
    {
        Transform nextLevelPart= currentLevelPart.SpawnLevelPart(levelPartsParent);
        lastEndPosition = nextLevelPart.position;
        currentLevelPart = nextLevelPart.GetComponent<LevelPart>();
    }
    private Transform SpawnPart(Transform levePart, Vector3 spawnPosition,Transform parent)
    {
        Transform partTransform = Instantiate(levePart, spawnPosition, Quaternion.identity, parent);
        return partTransform;
    }
    

    
}
