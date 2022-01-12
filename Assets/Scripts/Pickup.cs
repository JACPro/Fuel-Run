using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    [SerializeField] protected UnityEvent onPickup;
    [SerializeField] private GameObject pickupModel;

    private void OnEnable() 
    {
        pickupModel.SetActive(true);
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            onPickup.Invoke();
        }
    }

    public void DeactivatePickup(float time)
    {
        if (pickupModel.activeSelf == false) return;
        StartCoroutine(DisableObject(time));
    }

    private IEnumerator DisableObject(float time)
    {
        pickupModel.SetActive(false);
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
