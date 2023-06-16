using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHitbox : MonoBehaviour
{
    public List<Dummy> ins = new List<Dummy>();
    public bool playerIn = false;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject && other.gameObject.GetComponent<Dummy>())
            ins.Add(other.gameObject.GetComponent<Dummy>());
        if (other.gameObject && other.gameObject.GetComponent<Player>()) playerIn = true;
    }

    void OnTriggerExit(Collider other) {
        for (int i = 0; ins.Count > i; ++i) {
            if (other.gameObject && other.gameObject.GetComponent<Dummy>() == ins[i])
                ins.RemoveAt(i);
        }
        if (other.gameObject && other.gameObject.GetComponent<Player>()) playerIn = false;
    }

    public void Movement(float speed) {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
