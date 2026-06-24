using UnityEngine;

public class Punterocamara : MonoBehaviour
{
    [SerializeField] private float distanciaMaxima = 100f;
    [SerializeField] private LayerMask capasApunto; // Para filtrar qué objetos puede detectar

    void Update()
    {
        // El rayo sale desde la posición de la cámara física (manejada por Cinemachine) hacia el frente
        Ray rayo = new Ray(transform.position, transform.forward);
        RaycastHit impacto;

        // Lanzamos el rayo en el espacio 3D
        if (Physics.Raycast(rayo, out impacto, distanciaMaxima, capasApunto))
        {
            Vector3 puntoExacto = impacto.point;

            // Imprime las coordenadas X, Y, Z exactas en la Consola
            Debug.Log("La cámara está mirando exactamente a: " + puntoExacto);

            // Dibuja una línea roja en la ventana de Escena para que lo veas visualmente
            Debug.DrawLine(transform.position, puntoExacto, Color.red);
        }
    }
}
