using UnityEngine;

[DisallowMultipleComponent] // esto hace que solo pueda haber un componente PlayerShooting
public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private Light spotLight;
    [SerializeField] private Transform barrelEnd;
    [SerializeField] private LayerMask layerObstacle;
    [SerializeField] private AudioClip shootSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
        line.SetPosition(0, barrelEnd.position);
    }

    private void Fire()
    {
        audioSource.clip = shootSound;
        audioSource.Play();

        line.enabled = true;
        spotLight.enabled = true;

        Ray ray = new Ray(barrelEnd.position + transform.forward * -0.5f, transform.forward); // en el origen hacemos que el rayo salga desde más atrás del final del cañon para que haga daño aun estando en contacto.
        
        if (Physics.Raycast(ray, out RaycastHit hit, 20, layerObstacle))
        {
            line.SetPosition(1, hit.point);
            if (hit.collider.GetComponent<Enemy>() != null )
            {
                hit.collider.GetComponent<Enemy>().GetDamage();
            }
        }
        else
        {
            line.SetPosition(1, barrelEnd.position + transform.forward * 20);
        }

        Invoke("UnFire", 0.05f);
    }

    private void UnFire()
    {
        line.enabled = false;
        spotLight.enabled= false;
    }
}
