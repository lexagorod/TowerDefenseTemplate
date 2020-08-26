using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    [HideInInspector]
    public float maxHealth;
    [HideInInspector]
    public float currentHealth;

    private float originalScale;

    void Start()
    {
        originalScale = gameObject.transform.localScale.x;
    }

    void Update()
    {
        if (maxHealth != 0)
        {
            Vector3 tmpScale = gameObject.transform.localScale;
            tmpScale.x = currentHealth / maxHealth * originalScale;
            gameObject.transform.localScale = tmpScale;
        }
    }

}
