using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {
    private float speed = 10f;
    private float timeToDisappear = 10f;

    void Update() {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }

    void Start() {
        //moving the object up and down
        //StartCoroutine(moveUpAndDown());

        //StartCoroutine(Disappear());
    }

    public void moveUp() {
        transform.position += new Vector3(0, 1, 0);
    }

    public void moveDown() {
        transform.position += new Vector3(0, -1, 0);
    }

    IEnumerator moveUpAndDown() {
        var top = transform.position.y + 5;
        var bottom = transform.position.y + 2;
        while (true) {
            yield return new WaitForSeconds(1.0f);
            while (transform.position.y < top) {
                transform.position += Vector3.up * Time.deltaTime * 2;
                yield return null;
            }
            while (transform.position.y > bottom) {
                transform.position -= Vector3.up * Time.deltaTime * 2;
                yield return null;
            }
        }
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(timeToDisappear);
        Destroy(gameObject);
    }
}