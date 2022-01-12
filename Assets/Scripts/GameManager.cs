using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform pickups;
    [SerializeField] private GameObject finalUI;
    private CarLocomotion carMover;
    private ScoreTracker scorer;
    private Fuel fuel;
    private PlayerSwipeControls swipeControls;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        carMover = player.GetComponent<CarLocomotion>();
        scorer = player.GetComponent<ScoreTracker>();
        fuel = player.GetComponent<Fuel>();
        swipeControls = player.GetComponent<PlayerSwipeControls>();
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }
    }

    public void EndPlay()
    {
        finalUI.SetActive(true);
    }

    public void StartPlaying()
    {
        carMover.MessageDriveForward();
    }

    public void ResetLevel()
    {
        ResetPickups();
        carMover.ResetPosition();
        carMover.StopAllCoroutines();
        scorer.ResetScore();
        fuel.MessageResetFuel();
        swipeControls.ResetHorizPos();
    }

    private void ResetPickups()
    {
        foreach (Transform pickupGroup in pickups)
        {
            foreach(Transform pickup in pickupGroup)
            {
                pickup.gameObject.SetActive(true);
                pickup.gameObject.GetComponent<Pickup>().StopCoroutine("DisableObject");
            }
        }
    }
}
