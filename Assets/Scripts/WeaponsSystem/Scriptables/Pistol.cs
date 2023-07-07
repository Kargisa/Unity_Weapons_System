using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponsSystem/Type/Pistol")]
public class Pistol : ScriptableObject, IWeaponType
{
    public GameObject bulletPrefab;

    public Camera Camera { get; set; }
    private float fieldOfView;
    private bool scopingIn = false;
    private bool scopingOut = false;

    public IEnumerator AnimateMain(Transform attackPoint, object data)
    {
        BulletData bulletData = (BulletData)data;

        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
        Bullet bl = bullet.GetComponent<Bullet>();
        
        bl.Initalize(data);

        yield return null;
    }

    public IEnumerator AnimateSecondary(object data)
    {
        if (data is not SecondarySettings.Scope scopeSettings)
            throw new System.InvalidCastException($"{nameof(data)} can not be cast into {nameof(SecondarySettings.Scope)}");
        scopingIn = true;
        fieldOfView = Camera.fieldOfView;
        float finalZoom = Camera.fieldOfView - scopeSettings.zoom;
        float time = 0f;
        while (time < scopeSettings.scopeinTime)
        {
            if (Camera.fieldOfView <= finalZoom)
                break;
            Camera.fieldOfView -= Time.deltaTime * scopeSettings.zoom / scopeSettings.scopeinTime;
            yield return null;
            time += Time.deltaTime;
        }
        if (!scopingOut)
            Camera.fieldOfView = finalZoom;
        scopingIn = false;
    }

    public IEnumerator AnimateReleaseSecondary(object data)
    {
        if (data is not SecondarySettings.Scope scopeSettings)
            throw new System.InvalidCastException($"{nameof(data)} can not be cast into {nameof(SecondarySettings.Scope)}");
        scopingOut = true;
        float time = 0f;
        while (time < scopeSettings.scopeinTime)
        {
            if (Camera.fieldOfView >= fieldOfView)
                break;
            Camera.fieldOfView += Time.deltaTime * scopeSettings.zoom / scopeSettings.scopeinTime;
            yield return null;
            time += Time.deltaTime;
        }
        if (!scopingIn)
            Camera.fieldOfView = fieldOfView;
        scopingOut = false;
    }

    public void Destroy()
    {
        
    }

    public void Initialize(Transform parent, Camera camera)
    {
        Camera = camera;
        if (!bulletPrefab.TryGetComponent(out Bullet bullet))
            bulletPrefab.AddComponent<Bullet>();
        if (!bulletPrefab.TryGetComponent(out Rigidbody rb)) 
            bulletPrefab.AddComponent<Rigidbody>();
    }
}
