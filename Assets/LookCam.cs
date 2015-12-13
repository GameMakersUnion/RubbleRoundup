using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LookCam : MonoBehaviour {

    public void SetCamera()
    {

        Vector3 target = Camera.main.transform.position;
        target.x = transform.position.x;
        transform.LookAt(target);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr) { sr.sortingOrder = 1; }
        if (transform.tag == "Player") sr.sortingOrder = sr.sortingOrder + 1;

    }
}
