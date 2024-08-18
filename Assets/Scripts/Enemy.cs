using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;

    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;
    private AudioSource audioSource;

    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject heart;
    [SerializeField] private int maxHealth;
    private int health;
    private bool death = false; 

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        health = maxHealth;
        agent.speed += 1 + (float)GameManager.Instance.GetWave() / 5;
        agent.acceleration += 1 + (float)GameManager.Instance.GetWave() / 5;
    }

    private void Update()
    {
        if (!death)
        {
            agent.SetDestination(player.position);
        }
        else if (name.Contains("Helle"))
        {
            transform.position += Vector3.down * 0.005f;
        }
        else
        {
            transform.position += Vector3.down * 0.001f;
        }

        //transform.GetChild(1).rotation = Quaternion.LookRotation(new Vector3(0, 0, 0)); // con esto mantenemos la barra de vida siempre en la direccion de la cámara
        transform.GetChild(1).LookAt(Camera.main.transform); // con esto hacemos que el hijo en la posición 1 (healtbar) siempre mire a cámara aunque el objeto esté girando
    }

    public void GetDamage()
    {
        health--;
        healthBar.fillAmount = (float)health / (float)maxHealth; // con esto hacemos que la barra de vida vaya disminuyendo

        audioSource.clip = hurtSound;
        audioSource.Play();

        if (health <= 0)
        {
            death = true;
            audioSource.clip = deathSound;
            audioSource.Play();

            animator.SetTrigger("death");
            agent.enabled = false;
            GetComponent<Collider>().enabled = false;
            transform.GetChild(1).gameObject.SetActive(false); // cogemos el canvas que es el hijo en la posicion 1
            Destroy(gameObject, 5);
            transform.SetParent(null);

            GameManager.Instance.CheckEnemies();
            GameManager.Instance.IncreaseScore();

            if(Random.Range(0,100) < 10) // con esto le damos al enemigo un 10% de dropear un corazón
            {
                Instantiate(heart, transform.position + Vector3.up *1.5f, Quaternion.identity);
            }
        }
    }
}
