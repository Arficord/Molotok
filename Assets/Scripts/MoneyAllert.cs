using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyAllert : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(apearenceAnicmation());
    }

    private IEnumerator apearenceAnicmation()
    {
        const int WIPE_TICKS = 15;
        float start_y = transform.localPosition.y;
        for (int i=0;i<WIPE_TICKS;i++)
        {
            transform.localPosition = new Vector3(0, Mathf.Lerp(start_y, start_y-200, i/ (float)WIPE_TICKS));
            yield return new WaitForEndOfFrame();
        }
        for(int i=0; i<100; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        for (int i = 0; i < WIPE_TICKS; i++)
        {
            transform.localPosition = new Vector3(0, Mathf.Lerp(start_y - 200, start_y, i/ (float)WIPE_TICKS));
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
}
