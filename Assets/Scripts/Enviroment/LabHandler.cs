using System.Collections;
using UnityEngine;

public class LabHandler : MonoBehaviour
{
    public GameObject[] alarms; // Array de GameObjects para las alarmas

    public AudioSource audioSource; // Componente AudioSource para reproducir sonidos


    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Vector3 originalPositionOne;
    private Vector3 originalPositionTwo;

    private GameObject activeAlarm; // Almacena la alarma activa actualmente

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalScale = transform.localScale;


        if (alarms == null || alarms.Length == 0)
        {
            Debug.LogError("Assign all objects in the Unity editor.");
            return;
        }

        StartCoroutine(StartVibrationAfterDelay(11f, 3f));
    }

    IEnumerator StartVibrationAfterDelay(float delay, float vibrationDuration)
    {
        yield return new WaitForSeconds(delay);
        
        StartCoroutine(ActivateAlarms(delay));

        audioSource.Play();
        yield return StartCoroutine(VibrateObjects(vibrationDuration));

        float smoothTransitionDuration = 0.5f;
        float startTime = Time.time;

        while (Time.time - startTime <= smoothTransitionDuration)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, (Time.time - startTime) / smoothTransitionDuration);

            yield return null;
        }

        transform.position = originalPosition;
        transform.localScale = originalScale;
        audioSource.Stop();
    }

    IEnumerator VibrateObjects(float duration)
    {
        float startTime = Time.time;

        while (Time.time - startTime <= duration)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-0.03f, 0.03f), Random.Range(-0.03f, 0.03f), 0f);

            transform.position += randomOffset;

            yield return null;
        }
    }

    IEnumerator ActivateAlarms(float delay)
    {
        yield return new WaitForSeconds(2f);
        int alarmIndex = 0; // Índice para iterar a través de las alarmas

        while (true)
        {
            // Desactiva la alarma anterior
            if (activeAlarm != null)
            {
                DeactivateAlarm(activeAlarm);
            }

            // Activa la alarma actual
            activeAlarm = alarms[alarmIndex];
            ActivateAlarm(activeAlarm, originalScale);

            yield return new WaitForSeconds(1f);

            // Incrementa el índice y asegúrate de que esté dentro del rango
            alarmIndex = (alarmIndex + 1) % alarms.Length;
        }
    }

    void ActivateAlarm(GameObject alarm, Vector3 scale)
    {
        alarm.transform.position = new Vector3(0f, 0f, 0f);
    }

    void DeactivateAlarm(GameObject alarm)
    {
        alarm.transform.position = new Vector3(-1000f, 0f, 0f);
    }
}
