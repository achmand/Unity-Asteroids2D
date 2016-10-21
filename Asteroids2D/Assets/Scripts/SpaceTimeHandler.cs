using UnityEngine;

namespace Assets.Scripts
{
    public sealed class SpaceTimeHandler : MonoBehaviour {
        
        void Update () {
            var aspectRatio = Screen.width / (float)Screen.height;
            var xPos = transform.position.x;
            var yPos = transform.position.y;
            if (xPos > Camera.main.orthographicSize * aspectRatio)
            {
                transform.position = new Vector3(-Camera.main.orthographicSize * aspectRatio, yPos);
            }
            if (xPos < -Camera.main.orthographicSize * aspectRatio)
            {
                transform.position = new Vector3(Camera.main.orthographicSize * aspectRatio, yPos);
            }

            if (yPos > Camera.main.orthographicSize)
            {
                transform.position = new Vector3(xPos, -Camera.main.orthographicSize);
            }
            if (yPos < -Camera.main.orthographicSize)
            {
                transform.position = new Vector3(xPos, Camera.main.orthographicSize);
            }
        }
    }
}
