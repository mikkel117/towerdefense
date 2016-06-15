using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{

    public int PlayerHealth = 20;
    public int PlayerGold = 20;

    public UnityEngine.UI.Text Text_Health;
    public UnityEngine.UI.Text Text_Gold;

    public List<WaveInfo> Waves = new List<WaveInfo>();
    public int WaveNumber = 0;
    public GameObject EnemyPrefab;
    public Transform StartSpawnLocation;
    public List<Transform> WayPoints = new List<Transform>();

    public GameObject DeathPanel;
    public UnityEngine.UI.Button NextWaveButton;

    public void NextWave()
    {
        if (WaveNumber >= Waves.Count)
        {
            print("You beat the game");
        }
        else
        {
            StartCoroutine(SpawnEnemies());
            NextWaveButton.interactable = false;
            StartCoroutine(TimerNextWaveInteractable());
        }

    }

    IEnumerator TimerNextWaveInteractable()
    {
        yield return new WaitForSeconds(3);
        NextWaveButton.interactable = true;
    }

    public void ModifyGold(int modifier)
    {
        PlayerGold += modifier;
      
    }

    public void TakeDamage(int dmg)
    {
       

        if(PlayerHealth > 0)
        {
            PlayerHealth -= dmg;
            print("Player took " + dmg + " damage");
        }

        if(PlayerHealth <= 0)
        {
            PlayerHealth = 0;
            DeathPanel.SetActive(true);
        }
    }

    void Update()
    {
        Text_Health.text = PlayerHealth.ToString();
        Text_Gold.text = PlayerGold.ToString();
    }

    IEnumerator SpawnEnemies()
    {
        // WaveNumber er stadig 0 her, derfor får den første enemy fra listen


        for(int i = 0; i < Waves[WaveNumber].AmountOfEnemiesToSpawn; i++)
        {
            GameObject tmp = Instantiate(EnemyPrefab) as GameObject;
            tmp.transform.position = StartSpawnLocation.position;
            tmp.tag = "Enemy";

            Enemy tmpEnemy = tmp.GetComponent<Enemy>();
            tmpEnemy.WayPoints = WayPoints;
            tmpEnemy.AttackPower = Waves[WaveNumber].AttackPower;
            tmpEnemy.Speed = Waves[WaveNumber].Speed;
            tmpEnemy.Health = Waves[WaveNumber].EnemyHealth;

            yield return new WaitForSeconds(Waves[WaveNumber].SpawnRate);
        }

        WaveNumber++;
    }

    // Use this for initialization
    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;

        for (int i = 0; i < WayPoints.Count; i++)
        {
            if (i == 0)
            {
                Gizmos.DrawLine(WayPoints[0].position, WayPoints[1].position);
            }
            else if (i == WayPoints.Count - 1)
            {

            }
            else
            {
                int nextNumber = i + 1;
                Gizmos.DrawLine(WayPoints[i].position, WayPoints[nextNumber].position);
            }


        }


    }

}

[System.Serializable]
public class WaveInfo
{
    // Er ikke lavet dynamisk til forskellige enemies per runde (med vilje)
    public int AmountOfEnemiesToSpawn = 1;
    public Sprite EnemySprite;
    public int EnemyHealth = 1;
    public float SpawnRate = 0.2f;
    public int AttackPower = 1;
    public float Speed = 1;
    public int GoldGainedWhenKilled = 1;

    public WaveInfo()
    {
        AttackPower = 1;
        SpawnRate = 0.2f;
        Speed = 1;
        EnemyHealth = 1;
        AmountOfEnemiesToSpawn = 1;
        GoldGainedWhenKilled = 1;
    }


}
