using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float lerp;
    private Vector3 cameraOffSet;   

    private void Start()
    {
        cameraOffSet = transform.position - player.position; // calculo de la distancia entre la cámara y el jugador
    }

    private void Update()
    {
         transform.position = Vector3.Lerp(transform.position, cameraOffSet + player.position, lerp); // esto mantiene siempre la misma distancia entre la cámara y usando Lerp conseguimos hacer un pequeño delay al iniciar el movimiento.
    }
}
