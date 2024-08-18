using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // creamos el singleton para poder acceder a este script desde otros scripts
    [SerializeField] private Spawner[] spawner;
    [SerializeField] private Transform enemyBox;

    [Header("Text Mesh")]
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textWave;

    private AudioSource audioSource;
    private int wave = 1; public int GetWave() { return wave; } // esto es un get

    private int score;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        audioSource.Play();
        textWave.text = "Wave " + wave;
        for (float i = 0; i < 100; i++)
        {
            textWave.transform.localScale = new Vector3(0.5f + i/200, 0.5f + i/200, 0.5f + i/200);
            yield return new WaitForSeconds(0.001f);
        }
        
        audioSource.Play();
        textWave.text = "3";
        for (float i = 0; i < 100; i++)
        {
            textWave.transform.localScale = new Vector3(0.5f + i / 200, 0.5f + i / 200, 0.5f + i / 200);
            yield return new WaitForSeconds(0.001f);
        }
        
        audioSource.Play();
        textWave.text = "2";
        for (float i = 0; i < 100; i++)
        {
            textWave.transform.localScale = new Vector3(0.5f + i / 200, 0.5f + i / 200, 0.5f + i / 200);
            yield return new WaitForSeconds(0.001f);
        }
        
        audioSource.Play();
        textWave.text = "1";
        for (float i = 0; i < 100; i++)
        {
            textWave.transform.localScale = new Vector3(0.5f + i / 200, 0.5f + i / 200, 0.5f + i / 200);
            yield return new WaitForSeconds(0.001f);
        }
        
        audioSource.Play();
        textWave.text = "GO!";
        for (float i = 0; i < 100; i++)
        {
            textWave.transform.localScale = new Vector3(0.5f + i / 200, 0.5f + i / 200, 0.5f + i / 200);
            yield return new WaitForSeconds(0.001f);
        }

        textWave.text = null;
        NewWave();
    }

    private void NewWave()
    {
        foreach (Spawner _spawner in spawner)
        {
            _spawner.NewWave(1 + wave * 2);
        }

        wave++;
    }

    public void CheckEnemies() // este método comprueba si hay enemigos en la escena y si los spawners dejaron de invocar
    {
        foreach (Spawner _spawner in spawner) 
        {
            if (_spawner.CheckEnd() == false) // comprobamos si cada spawner continua invocando
                return; // si continua invocando, return hace que termine la función
        }

        if (enemyBox.childCount == 0) // con esto comprobamos que no haya enemigos dentro de la EnemyBox y si no hay invocamos una nueva oleada
            StartCoroutine(StartWave());
    }

    public void IncreaseScore()
    {
        score++;
        textScore.text = "Kill Count: " + score.ToString("000");
    }
}