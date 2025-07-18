using System;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private GameManager manager;

    private void Awake()
    {
        manager = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        manager.EndGame();
        Destroy(other.gameObject);
    }
}
