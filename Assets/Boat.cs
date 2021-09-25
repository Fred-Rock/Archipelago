using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private GameObject boat;


    // TODO: Fix this awful code
    private void Update()
    {
        if (SceneControllerManager.Instance.GetActiveScene() == SceneName.Scene1_World.ToString())
        {
            boat.SetActive(true);

            Transform boatParent = GetComponentInParent<Transform>();

            boatParent.localScale = new Vector2(.5f, .5f);
        }
        else
        {
            boat.SetActive(false);

            Transform boatParent = GetComponentInParent<Transform>();

            boatParent.localScale = new Vector2(1f, 1f);
        }

        if (Player.Instance.PlayerFacingDirection == Direction.left)
        {
            boat.transform.localScale = new Vector2(-1, boat.transform.localScale.y);
        }
        else
        {
            boat.transform.localScale = new Vector2(1, boat.transform.localScale.y);
        }
    }
}
