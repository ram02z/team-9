using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePlayer : MonoBehaviour
{
    public ThirdPersonMovement thirdPersonMovement;
    public bool cooldownOver;
    public const float FreezeTime = 3f;
    public const float CooldownTime = 12f;

    // Start is called before the first frame update
    void Start()
    {
        cooldownOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownOver && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Freeze());
        }
    }

    private IEnumerator Freeze()
    {
        if (!cooldownOver) yield break;
        cooldownOver = false;
        thirdPersonMovement.LockPositionAndCamera();
        Debug.Log("FROZEN");
        yield return new WaitForSeconds(FreezeTime);
        thirdPersonMovement.UnlockPositionAndCamera();
        Debug.Log("UNFROZEN AND ON COOLDOWN");
        yield return new WaitForSeconds(CooldownTime); //The cooldown
        cooldownOver = true;
        Debug.Log("READY TO USE");
    }
}
