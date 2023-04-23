using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxDestruct : MonoBehaviour
{
    [SerializeField] bool _onlyDeactivate;
    [Range(0,5)]
    [SerializeField] float _deactivateTime=0.5f;


    void OnEnable()
    {
        StartCoroutine(CheckIfAlive());
    }

    IEnumerator CheckIfAlive()
    {
        yield return new WaitForSeconds(_deactivateTime);
        if (_onlyDeactivate)
        {
            this.gameObject.SetActive(false);
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
