using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrVideoActive : MonoBehaviour
{
   
    public List<GameObject> VideoPanels;

    void Start()
    {
        StartCoroutine(ActivateAfterDelayCoroutine());
    }

    IEnumerator ActivateAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(0.1f);

        VideoPanels.ForEach(panel => panel.SetActive(true));
    }
}
