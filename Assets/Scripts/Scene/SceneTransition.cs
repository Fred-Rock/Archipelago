using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SceneTransition : MonoBehaviour
{
    [SerializeField] private SceneName sceneNameGoTo = SceneName.Scene0_Camp;
    [SerializeField] private Vector3 scenePositionGoTo = new Vector3();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            float xPosition = Mathf.Approximately(scenePositionGoTo.x, 0f) ? player.transform.position.x : scenePositionGoTo.x;

            float yPosition = Mathf.Approximately(scenePositionGoTo.y, 0f) ? player.transform.position.y : scenePositionGoTo.y;

            float zPosition = 0f;

            SceneControllerManager.Instance.FadeAndLoadScene(sceneNameGoTo.ToString(), new Vector3(xPosition, yPosition, zPosition));
        }
    }
}
