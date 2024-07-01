using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private GameObject[] towerPrefabs;

    private int selectedTower = 0;

    private void Awake()
    {
        main = this;
    }

    // GameObject here is a return type of the method.
    public GameObject GetSelectedTower()
    {
        return towerPrefabs[selectedTower];
    }
}
