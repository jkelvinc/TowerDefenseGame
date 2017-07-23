using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour 
{
	[System.Serializable]
	public class WaveInfo
	{
		public int NumEnemiesToSpawn;
		public float MinSpawnDelayInSeconds;
		public float MaxSpawnDelayInSeconds;
	}

	[SerializeField]
	private List<GameObject> enemies;

	[SerializeField]
	private List<WaveInfo> waveInfo;

	[SerializeField]
	private Path path;

	private int currentWaveIndex;

	private UnityAction startWaveAction;

	private void Start()
	{
		this.startWaveAction = new UnityAction(StartWave);
		EventManager.Instance.StartListening(GameEvents.StartGame, this.startWaveAction);
	}

	private void OnDestroy()
	{
		if (EventManager.Exists)
		{
			EventManager.Instance.StopListening(GameEvents.StartGame, this.startWaveAction);
		}
	}

	private void StartWave()
	{
		StartCoroutine(StartWaveCoroutine());
	}

	private IEnumerator StartWaveCoroutine()
	{
		var waveInfo = this.waveInfo[this.currentWaveIndex];

		for (int i = 0; i < waveInfo.NumEnemiesToSpawn; ++i)
		{
			int randomEnemyIndex = Random.Range(0, enemies.Count);
			CreateEnemy(this.enemies[randomEnemyIndex]);

			float delay = Random.Range(waveInfo.MinSpawnDelayInSeconds, waveInfo.MaxSpawnDelayInSeconds);
			yield return new WaitForSeconds(delay);
		}
	}

	private void CreateEnemy(GameObject enemyPrefab)
	{
		var enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
		var followPath = enemy.GetComponent<FollowPath>();
		if (followPath != null)
		{
			followPath.Execute(this.path);
		}
	}
}
