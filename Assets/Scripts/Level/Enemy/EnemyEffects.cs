using System.Collections;
using UnityEngine;

public class EnemyEffects : MonoBehaviour
{
    private Coroutine slownessCoroutine;
    private SlownessData currentSlownessData;
    private float slownessEndTime;

    private Coroutine tarredCoroutine;
    private bool tarred;
    private float tarredEndTime;

    public float SlownessCoefficient =>
        currentSlownessData == null
            ? 1f
            : Mathf.Abs(currentSlownessData.Coefficient - 1f);

    public bool Tarred => tarred;

    public void StartSlownessEffect(SlownessData newData)
    {
        float newEndTime = Time.time + newData.Duration;

        if (slownessCoroutine != null && newEndTime <= slownessEndTime)
            return;

        currentSlownessData = newData;
        slownessEndTime = newEndTime;

        if (slownessCoroutine == null)
            slownessCoroutine = StartCoroutine(SlownessRoutine());
    }

    private IEnumerator SlownessRoutine()
    {
        while (Time.time < slownessEndTime)
            yield return null;

        currentSlownessData = null;
        slownessCoroutine = null;
    }

    public void StartTarredEffect(float duration)
    {
        float newEndTime = Time.time + duration;

        if (tarredCoroutine != null && newEndTime <= tarredEndTime)
            return;

        tarred = true;
        tarredEndTime = newEndTime;

        if (tarredCoroutine == null)
            tarredCoroutine = StartCoroutine(TarredRoutine());
    }

    private IEnumerator TarredRoutine()
    {
        while (Time.time < tarredEndTime)
            yield return null;

        tarred = false;
        tarredCoroutine = null;
    }
}
