using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float lerp;
    private Vector3 cameraOffSet;   

    private void Start()
    {
        cameraOffSet = transform.position - player.position; // calculo de la distancia entre la c�mara y el jugador
    }

    private void Update()
    {
         transform.position = Vector3.Lerp(transform.position, cameraOffSet + player.position, lerp); // esto mantiene siempre la misma distancia entre la c�mara y usando Lerp conseguimos hacer un peque�o delay al iniciar el movimiento.
    }
}
