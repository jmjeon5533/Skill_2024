using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance => instance ??= FindObjectOfType<Player>();

    [SerializeField] private Transform orientation;
    [SerializeField] private Transform model;
    [SerializeField] private List<TrailRenderer> driftTrails;
    [SerializeField] private Camera minimapCam;
    [SerializeField] private LayerMask modelAlignLayer;
    [SerializeField] private float acc;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float steerSpeed;
    [SerializeField] private float driftSteerSpeed;
    [SerializeField] private float friction;
    [SerializeField] private float driftFriction;

    private float curSteerSpeed;
    private new Rigidbody rigidbody;
    private new Collider collider;
    private Camera cam;

    public Transform Orientation => orientation;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        cam = Camera.main;

        Debug.Log(driftTrails.Count);
    }

    private void Update()
    {
        if (!GameManager.Instance.isgame) return;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            driftTrails.ForEach(t => t.gameObject.SetActive(true));

            collider.sharedMaterial.staticFriction =
                Mathf.Lerp(collider.sharedMaterial.staticFriction, driftFriction, Time.deltaTime * 2);
            collider.sharedMaterial.dynamicFriction =
                Mathf.Lerp(collider.sharedMaterial.dynamicFriction, driftFriction, Time.deltaTime * 2);

            curSteerSpeed = Mathf.Lerp(curSteerSpeed, driftSteerSpeed, Time.deltaTime * 2f);
        }
        else
        {
            driftTrails.ForEach(t => t.gameObject.SetActive(false));

            collider.sharedMaterial.staticFriction =
                Mathf.Lerp(collider.sharedMaterial.staticFriction, friction, Time.deltaTime * 2);
            collider.sharedMaterial.dynamicFriction =
                Mathf.Lerp(collider.sharedMaterial.dynamicFriction, friction, Time.deltaTime * 2);

            curSteerSpeed = Mathf.Lerp(curSteerSpeed, steerSpeed, Time.deltaTime * 2f);
        }
        orientation.Rotate(0, Input.GetAxis("Horizontal") * curSteerSpeed * Time.deltaTime, 0);

        Physics.Raycast(model.position, Vector3.down, out var hit, 10f, modelAlignLayer);
        model.rotation = Quaternion.Lerp(model.rotation, Quaternion.FromToRotation(Vector3.up, hit.normal) * orientation.rotation, Time.deltaTime * 8);

        var velY = rigidbody.velocity.y;
        rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z), maxSpeed);
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, velY, rigidbody.velocity.z);
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isgame) return;
        rigidbody.AddForce(Input.GetAxisRaw("Vertical") * model.forward * acc, ForceMode.Acceleration);
        cam.fieldOfView 
        = Mathf.Lerp(cam.fieldOfView,60 + (Vector3.Magnitude(rigidbody.velocity) * 1.5f),Time.deltaTime);
        minimapCam.transform.eulerAngles = new Vector3(45,model.eulerAngles.y,0);
        var targetPos = Orientation.TransformPoint(new Vector3(0,23.42f,-13.8f));
        minimapCam.transform.position = targetPos;
    }
}
