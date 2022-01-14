using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
	public Material defaultMaterial;

	// Damage Material
	[Header("Damage Material")]
	public bool damageActivated;
	public float damageDuration = 2.0f;
	public Material damageMaterial;
	float damageTimer = 0.0f;

	// Camera Shake Variables
	[Header("Camera Shake")] 
	public float shakeDuration = 10f; // How long the object should shake for
	public float shakeAmount = 0.1f;
	public float decreaseFactor = 1.0f;
	Transform camTransform;

	private void Awake()
    {
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}

		damageActivated = false;
	}

    void Start()
    {
        
    }

    void Update()
    {
        if (damageActivated)
        {
			DamageEffect();
        }
    }


	// Render Effect
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Material material = defaultMaterial;
		if (damageActivated)
        {
			material = damageMaterial;
        }

		Graphics.Blit(source, destination, material);
	}

	// Damage Effect
	private void DamageEffect()
    {
		if (damageTimer <= 0)
		{
			ShakeCamera();
		}
		else if (damageTimer >= damageDuration)
		{
			damageTimer = 0;
			damageActivated = false;
			return;
		}

		damageTimer += Time.deltaTime;
	}


	// Camera Shake Functions
	public void ShakeCamera()
	{
		StartCoroutine(ShakeCoroutine(shakeDuration));
	}

	IEnumerator ShakeCoroutine(float shakeDuration)
	{
		Vector3 originalPos = camTransform.localPosition;
		float currentShake = shakeDuration;

		while (currentShake > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			currentShake -= Time.deltaTime * decreaseFactor;

			yield return null;
		}

		camTransform.localPosition = originalPos;
	}
}
