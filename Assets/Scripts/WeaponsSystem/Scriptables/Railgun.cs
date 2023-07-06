using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(menuName="WeaponsSystem/Type/Railgun")]
public class Railgun : ScriptableObject, IWeaponType
{
    [Header("Main")]
    public Gradient color;
    [Min(0f)]
    public float startWidth;
    [Min(0f)]
    public float endWidth;
    public Material lineMaterial;
    [Header("Secondary")]


    [HideInInspector] public GameObject railgunLine;

    private LineRenderer _line;
    private float fieldOfView;
    public Camera Camera { get; set; }
    private bool scopingIn = false;
    private bool scopingOut = false;


    public void Initialize(Transform parent, Camera camera)
    {
        Camera = camera;
        if (railgunLine == null)
            railgunLine = new GameObject("Railgun_Line");
        Instantiate(railgunLine);

        _line = railgunLine.AddComponent<LineRenderer>();
        _line.useWorldSpace = true;
        _line.colorGradient = color;
        _line.startWidth = startWidth;
        _line.endWidth = endWidth;
        _line.positionCount = 2;

        if (lineMaterial == null)
        {
            Debug.LogWarning($"{nameof(lineMaterial)} set to null, using default material");
            _line.material = new Material(Shader.Find("Universal Render Pipeline/Particles/Unlit"));
        }
        else
            _line.material = lineMaterial;

        _line.enabled = false;

        railgunLine.transform.parent = parent;
        railgunLine.transform.localPosition = Vector3.zero;
            
    }

    public IEnumerator Animate(Transform attackPoint, object data)
    {
        Vector3 hitPoint = (Vector3)data;

        _line.enabled = true;
        _line.SetPosition(0, attackPoint.position);
        _line.SetPosition(1, hitPoint);
        yield return new WaitForSeconds(0.1f);
        _line.enabled = false;
    }

    public void Destroy()
    {
        Destroy(railgunLine);
    }

    public IEnumerator AnimateSecondary(object data)
    {
        SecondarySettings.Scope scopeSettings = data as SecondarySettings.Scope;
        if (scopeSettings == null)
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
        SecondarySettings.Scope scopeSettings = data as SecondarySettings.Scope;
        if (scopeSettings == null)
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
}
