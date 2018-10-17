using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour {
	[SerializeField] private float spawnTimeOut = 0;
	[SerializeField] private int maxWave = 10;
	[SerializeField] private Transform target;
	[SerializeField] private List<EnemyInfo> enemies = new List<EnemyInfo>();
	

	private BaseEnemy[] waveEnemies;
	private int waveNumber = 0;
	private float lastSpawnTime = float.MinValue;
	private IEnumerator<IEnumerable<BaseEnemy>> waves; // Lazily generated waves

	// Use this for initialization
	void Start () {
		enemies.ForEach(info => info.enemyType.target = target);
		//waveEnemies = makeWaveEnumerable(waveNumber, enemies).ToArray();
		waves = 
			Enumerable
				.Range(0, maxWave)
				.Select(makeWaveMaker(enemies))
				.GetEnumerator();
		NextWave();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastSpawnTime > spawnTimeOut){
			var randomIndex = Random.Range(0, waveEnemies.Length);
			Instantiate(waveEnemies[randomIndex], transform.position, transform.rotation);
			lastSpawnTime = Time.time;
		}
	}

	bool NextWave(){
		var done = waves.MoveNext();
		waveEnemies = waves.Current.ToArray(); // Finally evaluate this wave
		return done;
	}

	/*static IEnumerable<BaseEnemy> makeWaveEnumerable(int wave, List<EnemyInfo> enemies) => 
		from enemy in enemies
		from _ in Enumerable.Range(0, enemy.spawnRate)
		where enemy.startWave <= wave
		select enemy.enemyType;*/

	static System.Func<int, IEnumerable<BaseEnemy>> makeWaveMaker(List<EnemyInfo> enemies) => (int wave) => 
		from enemy in enemies
		from _ in Enumerable.Range(0, enemy.spawnRate)
		where enemy.startWave <= wave
		select enemy.enemyType;

	[System.Serializable]
	public class EnemyInfo{
		public BaseEnemy enemyType;
		public int startWave;
		public int spawnRate;
	}

}
