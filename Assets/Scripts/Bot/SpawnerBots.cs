using UnityEngine;

public class SpawnerBots : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private Bot _prefabBot;
    [SerializeField] private BaseCreator _creatorBase;

    public Bot SpawnBot()
    {
        SpawnPoint spawnPoint = GetRandomPoint();

        Bot bot = Instantiate(_prefabBot, spawnPoint.transform.position, Quaternion.identity);
        bot.SetBaseCreator(_creatorBase);

        return bot;
    }

    private SpawnPoint GetRandomPoint()
    {
        int minNumber = 0;
        int maxNumber = _spawnPoints.Length;

        return _spawnPoints[Random.Range(minNumber, maxNumber)];
    }
}