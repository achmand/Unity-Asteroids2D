using UnityEngine;

namespace Assets.Scripts
{
    public sealed class Destroyer : MonoBehaviour {

        void OnEnable ()
        {
            Invoke("Destroy", 1f);
        }
	
        void Destroy ()
        {
            gameObject.SetActive(false);
        }

        void OnDisable()
        {
            CancelInvoke();
        }
    }
}
