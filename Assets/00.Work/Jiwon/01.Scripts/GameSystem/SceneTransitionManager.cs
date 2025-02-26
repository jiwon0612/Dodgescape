using System;
using UnityEngine;

[DefaultExecutionOrder(-20)]
public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private EntityFinderSO playerFinder;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Assert(player != null, "player != null");
        
        playerFinder.SetEntity(player.GetComponent<Entity>());
    }
}
