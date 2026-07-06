using UnityEngine;
using DG.Tweening;

public class LevitacionArma : MonoBehaviour
{
    [Header("Configuración de Levitación")]
    [SerializeField] private float distanciaFlote = 0.3f;
    [SerializeField] private float duracionCiclo = 2f;
    [SerializeField] private float velocidadRotacion = 30f;

    [Header("Configuración de Interacción")]
    [SerializeField] private float escalaObjetivo = 1.2f;
    [SerializeField] private float duracionAnimacion = 0.3f;

    private Vector3 posicionInicial;
    private Vector3 escalaInicial;
    private Tween miTweenLevitacion;

    void Start()
    {
        posicionInicial = transform.localPosition;
        escalaInicial = transform.localScale;

        IniciarLevitacion();
    }

    void Update()
    {
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }

    void IniciarLevitacion()
    {
        miTweenLevitacion = transform.DOLocalMoveY(posicionInicial.y + distanciaFlote, duracionCiclo).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    public void MostrarIntencionInteractuar(bool estaCerca)
    {
        if (estaCerca)
        {
            miTweenLevitacion.Pause();

            transform.DOScale(escalaInicial * escalaObjetivo, duracionAnimacion).SetEase(Ease.OutBack);
            transform.DOLocalMoveY(posicionInicial.y + (distanciaFlote * 1.5f), duracionAnimacion).SetEase(Ease.OutCubic);
        }
        else
        {
            transform.DOScale(escalaInicial, duracionAnimacion).SetEase(Ease.InCubic);
            transform.DOLocalMoveY(posicionInicial.y, duracionAnimacion).SetEase(Ease.InCubic).OnComplete(() =>
            {
                miTweenLevitacion.Play();
            });
        }
    }
    void OnDestroy()
    {
        if (miTweenLevitacion != null && miTweenLevitacion.IsActive())
        {
            miTweenLevitacion.Kill();
        }
    }
}