using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardCooldown : MonoBehaviour
{
    [Header("Cooldown UI")]
    public Image cooldownOverlay;   // รูป overlay ที่ใช้เติมเต็ม
    public float cooldownTime = 5f; // เวลา cooldown

    private bool onCooldown = false;
    private float cooldownTimer = 0f;

    public bool IsOnCooldown => onCooldown;

    private void Awake()
    {
        if (cooldownOverlay != null)
        {
            cooldownOverlay.type = Image.Type.Filled;
            cooldownOverlay.fillMethod = Image.FillMethod.Vertical;
            cooldownOverlay.fillOrigin = (int)Image.OriginVertical.Bottom;
            cooldownOverlay.fillAmount = 0f;
        }
    }

    private void Update()
    {
        if (onCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownOverlay != null)
                cooldownOverlay.fillAmount = Mathf.Clamp01(cooldownTimer / cooldownTime);

            if (cooldownTimer <= 0f)
            {
                onCooldown = false;
                if (cooldownOverlay != null)
                    cooldownOverlay.fillAmount = 0f;
            }
        }
    }

    public void StartCooldown(float time)
    {
        cooldownTime = time;
        cooldownTimer = cooldownTime;
        onCooldown = true;

        if (cooldownOverlay != null)
            cooldownOverlay.fillAmount = 1f; // เริ่มเต็ม
    }
}