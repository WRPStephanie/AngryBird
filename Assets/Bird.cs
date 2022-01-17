using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    bool onClicked = false;
    public Transform centerPos;
    public float maxDistance = 2f;

    SpringJoint2D spj;
    public float releaseTime = 0.1f;

    public LineRenderer frontLine;
    public LineRenderer backLine;

    public Transform frontBond;
    public Transform backBond;


    Rigidbody2D rj;

    // Start is called before the first frame update
    void Start()
    {
        spj = GetComponent<SpringJoint2D>();
        rj = GetComponent<Rigidbody2D>();

        frontLine.SetPosition(0, frontBond.position);
        frontLine.SetPosition(1, transform.position);
        backLine.SetPosition(0, backBond.position);
        backLine.SetPosition(1, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (onClicked)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            frontLine.SetPosition(0, frontBond.position);
            frontLine.SetPosition(1, transform.position);
            backLine.SetPosition(0, backBond.position);
            backLine.SetPosition(1, transform.position);

            if (Vector3.Distance(transform.position, centerPos.position) > maxDistance)
            {
                Vector3 direction = (transform.position - centerPos.position).normalized * maxDistance;
                transform.position = centerPos.position + direction;

                frontLine.SetPosition(0, frontBond.position);
                frontLine.SetPosition(1, transform.position);
                backLine.SetPosition(0, backBond.position);
                backLine.SetPosition(1, transform.position);
            }
        }
    }
    private void OnMouseDown()
    {
        onClicked = true;
        rj.isKinematic = true;
    }
    private void OnMouseUp()
    {
        onClicked = false;
        rj.isKinematic = false;
        StartCoroutine(Release());
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);
        spj.enabled = false;
        frontLine.enabled = false;
        backLine.enabled = false;
    }
}
