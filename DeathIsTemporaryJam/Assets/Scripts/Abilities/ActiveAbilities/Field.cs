using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Field : MonoBehaviour
{
    public float fieldTime;
    public float fieldSize;

    private void Start()
    {
        StartCoroutine(DestroyField(fieldTime));
        SetFieldSize(fieldSize);
    }

    IEnumerator DestroyField(float _FieldTime)
    {
        yield return new WaitForSeconds(_FieldTime);
        Destroy(gameObject);
    }

    void SetFieldSize(float fieldSize)
    {
        gameObject.transform.localScale = new Vector3(fieldSize, fieldSize, fieldSize);
    }

}
