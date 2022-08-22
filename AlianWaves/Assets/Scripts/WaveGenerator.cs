using UnityEngine;

[CreateAssetMenu(fileName ="WaveGenerator", menuName ="WaveGenerator")]
public class WaveGenerator : ScriptableObject
{
    public int WavesCount;
    public int SpaceShipsMultiplier;
    public float SpawnDelayMultiplier;
    public Transform EnemyPrefab;
}