using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerHealth : MonoBehaviour
{

    [Header("Health")]
    [Range(5, 15)]
    [SerializeField] private int health;
    [SerializeField] private Slider sliderHealth;
    private bool inmunity = false;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Audio")]
    [Tooltip("Este es el clip que suena cuando el personaje recibe daño")]
    [SerializeField] private AudioClip hurtSound;
    [Tooltip("Este es el clip que suena cuando el personaje muere")]
    [SerializeField] private AudioClip deathSound;
    private AudioSource audioSource;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        sliderHealth.value = sliderHealth.maxValue = health;
    }

    private void OnCollisionStay(Collision collision)
    {
        if ( !inmunity && collision.gameObject.GetComponent<Enemy>() != null)
        {
            audioSource.clip = hurtSound;
            audioSource.Play();

            health--;
            sliderHealth.value = health;
            StartCoroutine(Inmunity());

            if (health <= 0)
            {
                audioSource.clip = deathSound;
                audioSource.Play();

                animator.SetTrigger("death");
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerShooting>().enabled = false;
                GetComponent<Collider>().enabled = false;
                GetComponent <Rigidbody>().isKinematic = true;

                gameOverPanel.SetActive(true);
            }
        }
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.Contains("Heart"))
        {
            StartCoroutine(Healing(3));
            Destroy(collision.gameObject);
        }
    }

    IEnumerator Inmunity()
    {
        inmunity = true;
        for (int  i = 0; i < 20; i++)
        {
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = !GetComponentInChildren<SkinnedMeshRenderer>().enabled;   
            yield return new WaitForSeconds(0.05f);
        }
        inmunity = false;
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // con esto recargamos la escena actual
    }

    IEnumerator Healing(int amount)
    {
        transform.GetChild(3).GetComponent<ParticleSystem>().Play();
        for (int i = 0; i < amount; i++)
        {
            if (health < sliderHealth.maxValue)
            {
                health++;
                sliderHealth.value = health;
            }
            yield return new WaitForSeconds(1);
        }
        transform.GetChild(3).GetComponent<ParticleSystem>().Stop();
    }
}
