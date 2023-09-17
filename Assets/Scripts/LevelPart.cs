using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPart : MonoBehaviour
{
    [SerializeField]
    private List<LevelPart> validLevelParts;
    [SerializeField]
    Transform endPoint;

    public Transform SpawnLevelPart(Transform parent)
    {
        Transform chosenLevelPart = validLevelParts[Random.Range(0, validLevelParts.Count)].transform;
        Transform lastLevelPartTransform = SpawnPart(chosenLevelPart,parent);

        return lastLevelPartTransform;
    }

    private Transform SpawnPart(Transform levePart,  Transform parent)
    {
        Transform partTransform = Instantiate(levePart, endPoint.position, Quaternion.identity, parent);
        return partTransform;
    }
}
