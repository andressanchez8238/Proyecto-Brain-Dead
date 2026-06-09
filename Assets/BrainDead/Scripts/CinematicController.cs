using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CinematicController : MonoBehaviour
{
    public CinemachineCamera[] cinematicCameras;
    public CinemachineCamera gameplayCamera;

    public float timePerCamera = 2.5f;

    public PlayerMovement player;

    private void Start()
    {
        StartCoroutine(PlayCinematicSequence());
    }

    IEnumerator PlayCinematicSequence()
    {
        foreach (var cam in cinematicCameras)
            cam.Priority = 0;

        gameplayCamera.Priority = 0;

        for (int i = 0; i < cinematicCameras.Length; i++)
        {
            cinematicCameras[i].Priority = 20;
            yield return new WaitForSeconds(timePerCamera);
            cinematicCameras[i].Priority = 0;
        }

        gameplayCamera.Priority = 30;

        player.canMove = true;
    }
}
