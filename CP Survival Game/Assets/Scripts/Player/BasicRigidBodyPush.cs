using UnityEngine;

public class BasicRigidBodyPush : MonoBehaviour
{
	public LayerMask pushLayers;
	public bool canPush;
	[Range(0.5f, 5f)] public float strength = 1.1f;
	public GameObject controls;
	public bool mobile;

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (canPush) PushRigidBodies(hit);
		//else CheckInteractable(hit);
	}

	private void PushRigidBodies(ControllerColliderHit hit)
	{
		// https://docs.unity3d.com/ScriptReference/CharacterController.OnControllerColliderHit.html

		// make sure we hit a non kinematic rigidbody
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic) return;

		// make sure we only push desired layer(s)
		var bodyLayerMask = 1 << body.gameObject.layer;
		if ((bodyLayerMask & pushLayers.value) == 0) return;

		// We dont want to push objects below us
		if (hit.moveDirection.y < -0.3f) return;

		// Calculate push direction from move direction, horizontal motion only
		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);

		// Apply the push and take strength into account
		body.AddForce(pushDir * strength, ForceMode.Impulse);
	}

	private void OnTriggerEnter(Collider other)
	{
		Interactable interactable = other.GetComponent<Interactable>();
		interactable.pickup = true;
		interactable.canvas.SetActive(true);
		if(mobile == false)
        {
			if (interactable == null) return;
			else
			{
				StarterAssets.StarterAssetsInputs.Instance.EnableCollection(true);
			}
		}
		Debug.Log("Collided with " + other.transform.name);
		if (interactable.canInteract)
		{
			interactable.OnInteract();
		}
	}

    private void OnTriggerStay(Collider other)
    {
		if(other.tag == "Inventory")
        {
			Interactable interactable = other.GetComponent<Interactable>();
			if (controls.activeInHierarchy)
			{
				interactable.canvas.SetActive(true);
			}
			else
			{
				interactable.canvas.SetActive(false);
			}
		}
		
	}

    private void OnTriggerExit(Collider other)
	{
		Interactable interactable = other.GetComponent<Interactable>();
		interactable.pickup = false;
		interactable.canvas.SetActive(false);
		StarterAssets.StarterAssetsInputs.Instance.DisableCollection(true);
		if (interactable == null) return;
		else
		{
			interactable = null;
		}
	}


	/*private void CheckInteractable(ControllerColliderHit hit)
    {
		Interactable interactable = hit.transform.GetComponent<Interactable>();
		if (interactable == null) return;
		Debug.Log("Collided with " + hit.transform.name);
		if(interactable.canInteract)
        {
			interactable.OnInteract();
        }
    }*/

}