using UnityEngine;

public class TileMouseOver : MonoBehaviour {

    public Color highlightColor;
    Color normalColor;

	void Start () {
        normalColor = GetComponent<Renderer>().material.color;
 	}
	

    /*Keep in mind this will call an update every frame which is not the most efficient.  Be careful how this is used!*/
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        /*ScreenPointToRay requires a Vector3.  Normally you would have to say Vector3 (x,y,z)
        but Input.mousePosition IS a Vector3 so there's no need.*/

        RaycastHit hitInfo;

        /*We're going to do a raycast specifically against our collider.*/
        if (GetComponent<Collider>().Raycast ( ray, out hitInfo, Mathf.Infinity))
        {
            GetComponent<Renderer>().material.color = highlightColor;
        }
        else
        {
            GetComponent<Renderer>().material.color = normalColor;
        }


        }
}
