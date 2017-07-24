using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour 
{
	public event System.Action OnAllWavesCompleted; 

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
	private float waveIntervalInSeconds;

	[SerializeField]
	private Path path;

	private int currentWaveIndex = -1;

	private UnityAction startWaveAction;

	private List<GameObject> enemiesCreated = new List<GameObject>();


	private void Start()
	{
		this.startWaveAction = new UnityAction(StartNextWave);
		EventManager.Instance.StartListening(GameEvents.StartGame, this.startWaveAction);
	}

	private void OnDestroy()
	{
		if (EventManager.Exists)
		{
			EventManager.Instance.StopListening(GameEvents.StartGame, this.startWaveAction);
		}
	}

	private void StartNextWave()
	{
		var waveInfo = GetNextWave();
		if (waveInfo == null)
		{
			if (this.OnAllWavesCompleted != null)
			{
				this.OnAllWavesCompleted();
			}
		}
		else
		{
			StartCoroutine(StartWaveCoroutine(waveInfo));
		}
	}

	private WaveInfo GetNextWave()
	{
		++this.currentWaveIndex;
		if (this.currentWaveIndex < this.waveInfo.Count)
		{
			return this.waveInfo[this.currentWaveIndex];
		}

		this.currentWaveIndex = -1;
		return null;
	}

	private IEnumerator StartWaveCoroutine(WaveInfo waveInfo)
	{
		yield return new WaitForSeconds(this.waveIntervalInSeconds);

		this.enemiesCreated.Clear();

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
		if (enemy != null)
		{
			this.enemiesCreated.Add(enemy);

			var enemyUnit = enemy.GetComponent<Unit>();
			if (enemyUnit != null)
			{
				enemyUnit.OnUnitDestroyed += HandleUnitDestroyed;		
			}

			var followPath = enemy.GetComponent<FollowPath>();
			if (followPath != null)
			{
				followPath.Execute(this.path);
			}
		}
	}

	private void HandleUnitDestroyed(Unit unit)
	{
		if (this.enemiesCreated.Contains(unit.gameObject))
		{
			this.enemiesCreated.Remove(unit.gameObject);
		}

		if (this.enemiesCreated.Count == 0)
		{
			StartNextWave();
		}
	}
}
