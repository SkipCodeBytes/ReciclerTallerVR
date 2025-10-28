using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpUtils
{
    //StartCoroutine(LerpFloat( setter: v => speed = v, origin: 0f, target: 10f, duration: 2f, callback: () => Debug.Log("Interpolaci�n completada")));

    public static IEnumerator LerpFloat(Action<float> setter, float origin, float target, float duration, Action callback = null)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float value = Mathf.Lerp(origin, target, t / duration);
            setter(value);
            yield return null;
        }
        setter(target);
        callback?.Invoke();
    }

    public static IEnumerator LerpVector2(Action<Vector2> setter, Vector2 origin, Vector2 target, float duration, Action callback = null)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            Vector2 value = Vector2.Lerp(origin, target, t / duration);
            setter(value);
            yield return null;
        }
        setter(target);
        callback?.Invoke();
    }

    public static IEnumerator LerpVector3(Action<Vector3> setter, Vector3 origin, Vector3 target, float duration, Action callback = null)
    {
        float t = 0;
        float invDuration = 1f / duration; // Pre-calcular para evitar divisiones
        
        while (t < duration)
        {
            t += Time.deltaTime;
            float normalizedTime = t * invDuration;
            // Usar una interpolación más eficiente
            Vector3 value = origin + (target - origin) * normalizedTime;
            setter(value);
            yield return null;
        }
        setter(target);
        callback?.Invoke();
    }

    public static IEnumerator LerpColor(Action<Color> setter, Color origin, Color target, float duration, Action callback = null)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            Color value = Color.Lerp(origin, target, t / duration);
            setter(value);
            yield return null;
        }
        setter(target);
        callback?.Invoke();
    }
}
