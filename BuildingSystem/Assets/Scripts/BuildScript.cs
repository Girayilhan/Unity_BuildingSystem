using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildScript : MonoBehaviour {
    public static BuildScript instance;
    public CursorMode cursorMode = CursorMode.Auto;
    public GameObject[] list = new GameObject[3];

    public GameObject hand;
    public GameObject gui;
    public LayerMask layer;
    public Material handMat;
    public Material readyMat;
    public Material placeMatWall;
    public Material placeMatFoundation;
    public MeshCollider box;
    int index = 0;
    public float foundationX;
    public float foundationY;
    public float foundationZ;
    public float wallX;
    public float wallY;
    public float wallZ;
    public float wallXRotation, wallYRotation,wallZRotation;
    public bool activeGui;
   
    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        if (instance == null)
        {
            instance = this;
        }
        activeGui = false;
        gui.SetActive(activeGui);
        hand = Instantiate(list[index]);
        hand.GetComponent<MeshRenderer>().material=readyMat;
        
        box = hand.GetComponent<MeshCollider>();
        box.enabled = false;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
       
        RaycastHit hit = new RaycastHit();
        
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 20, layer))
        {   
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red);
            if (hit.collider.transform != hand.transform)
            {   // if we look ground 
                if (hit.collider.name == "Ground")
                {
                    if (hit.collider.transform != hand.transform && hand.tag == "Foundation")
                    {
                        Debug.Log(hit.transform.name);
                        Debug.Log(hit.collider.transform.position - hit.point);
                        hand.transform.position = hit.point + new Vector3(0, foundationY/2, 0);
                    }
                    else
                    {
                        Debug.Log("You cant place it without foundation");
                    }
                }
                // if we look foundation
                else if (hit.collider.tag == "Foundation")
                {
                    if (hand.tag == "Foundation")
                    {
                        if (hit.collider.transform != hand.transform)
                            Debug.Log(hit.transform.name);
                        Debug.Log(hit.collider.transform.position - hit.point);

                        float x = Mathf.Abs(hit.collider.transform.position.x - hit.point.x);
                        float z = Mathf.Abs(hit.collider.transform.position.z - hit.point.z);
                        Vector3 offset = hit.collider.transform.position - hit.point;
                        if (x > z)
                        {

                            if (offset.x > 0)
                            {
                                hand.transform.position = hit.collider.transform.position + new Vector3(-foundationX, 0, 0);
                            }
                            else
                            {
                                hand.transform.position = hit.collider.transform.position + new Vector3(foundationX, 0, 0);

                            }
                        }
                        else
                        {
                            if (offset.z > 0)
                            {

                                hand.transform.position = hit.collider.transform.position + new Vector3(0, 0, -foundationZ);
                            }
                            else
                            {
                                hand.transform.position = hit.collider.transform.position + new Vector3(0, 0, foundationZ);

                            }

                        }
                    }
                    else if (hand.tag == "Wall")
                    {
                        float x = Mathf.Abs(hit.collider.transform.position.x - hit.point.x);
                        float z = Mathf.Abs(hit.collider.transform.position.z - hit.point.z);
                        Vector3 offset = hit.collider.transform.position - hit.point;
                        if (x > z)
                        {

                            if (offset.x > 0)
                            {
                                hand.transform.rotation = Quaternion.Euler(wallXRotation, wallYRotation, wallZRotation);
                                hand.transform.position = hit.collider.transform.position + new Vector3(-foundationX/2 +wallZ/2, 0, 0);
                            }
                            else
                            {
                                hand.transform.rotation = Quaternion.Euler(wallXRotation, wallYRotation, wallZRotation);
                                hand.transform.position = hit.collider.transform.position + new Vector3(foundationX / 2 - wallZ / 2, 0, 0);

                            }
                        }
                        else
                        {
                            if (offset.z > 0)
                            {
                                hand.transform.rotation = Quaternion.Euler(wallXRotation, 0, wallZRotation);

                                hand.transform.position = hit.collider.transform.position + new Vector3(0, 0, -foundationZ/2+wallZ/2);
                            }
                            else
                            {
                                hand.transform.rotation = Quaternion.Euler(wallXRotation, 0, wallZRotation);
                                hand.transform.position = hit.collider.transform.position + new Vector3(0, 0, foundationZ / 2 - wallZ / 2);

                            }

                        }
                    }
                    else if (hand.tag == "Ladder")
                    {
                        float x = Mathf.Abs(hit.collider.transform.position.x - hit.point.x);
                        float z = Mathf.Abs(hit.collider.transform.position.z - hit.point.z);
                        Vector3 offset = hit.collider.transform.position - hit.point;
                        if (x > z)
                        {

                            if (offset.x > 0)
                            {
                                hand.transform.rotation = Quaternion.Euler(0,-90, 0);
                                hand.transform.position = hit.collider.transform.position + new Vector3(0,0, 0);
                            }
                            else
                            {
                                hand.transform.rotation = Quaternion.Euler(0, 90, 0);
                                hand.transform.position = hit.collider.transform.position + new Vector3(0, 0, 0);

                            }
                        }
                        else
                        {
                            if (offset.z > 0)
                            {
                                hand.transform.rotation = Quaternion.Euler(0, 180, 0);
                                hand.transform.position = hit.collider.transform.position + new Vector3(0, 0, 0);
                            }
                            else
                            {
                                hand.transform.rotation = Quaternion.Euler(0, 0, 0);
                                hand.transform.position = hit.collider.transform.position + new Vector3(0, 0, 0);
                            }

                        }
                    }


                }
                //if we look wall
                else if (hit.collider.tag == "Wall")
                {
                    Debug.Log(hit.collider.transform.position - hit.point);
                    if (hand.tag == "Foundation")
                    {
                        
                        Debug.Log(hit.transform.name);
                        Debug.Log(hit.collider.transform.position - hit.point);
                        float x = Mathf.Abs(hit.collider.transform.position.x - transform.position.x);
                        float z = Mathf.Abs(hit.collider.transform.position.z - transform.position.z);
                        Vector3 way=hit.collider.transform.position.normalized - transform.position.normalized;
                        if (x > z)
                        {
                             
                            if (way.x > 0)
                            {
                                hand.transform.position = hit.collider.transform.position + new Vector3(wallZ / 2 - foundationX / 2, wallY , 0);
                            }
                            else
                            {
                              
                                hand.transform.position = hit.collider.transform.position + new Vector3(-(wallZ / 2 - foundationX / 2), wallY , 0);
                            }
                        }
                        else
                        {
                             
                            if (way.z > 0)
                            {
                                hand.transform.position = hit.collider.transform.position + new Vector3(0, wallY , wallZ / 2 - foundationX / 2);

                            }
                            else
                            {
                                hand.transform.position = hit.collider.transform.position + new Vector3(0, wallY, -(wallZ / 2 - foundationX / 2));
                               
                            }
                        }
                        


                    }

                }
               
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                if (activeGui)
                {
                    if (hand.tag == "Foundation")
                    {
                        hand.GetComponent<MeshRenderer>().material = placeMatFoundation;
                    }
                    else if (hand.tag == "Wall")
                    {
                        hand.GetComponent<MeshRenderer>().material = placeMatWall;
                    }


                    hand = Instantiate(list[index]);
                    hand.GetComponent<MeshRenderer>().material = readyMat;
                    box.enabled = true;
                    box = hand.GetComponent<MeshCollider>();
                    box.enabled = false;
                }
               
            }
            if (Input.GetMouseButtonDown(1))
            {
                /*index = (index + 1) % 3;
                Destroy(hand);
                hand = Instantiate(list[index]);
                box = hand.GetComponent<BoxCollider>();
                if (hand.tag == "Foundation")
                {
                    hand.GetComponent<MeshRenderer>().material = readyMat;
                }
                else if (hand.tag == "Wall")
                {
                    hand.GetComponent<MeshRenderer>().material = readyMat;
                }
                box.enabled = false;*/
                UiOn();
               
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (hit.collider.name != "Ground")
                    Destroy(hit.collider.gameObject);
            }
        }
       
    }
    public void button1()
    {
        index = 0;
        Destroy(hand);
        hand = Instantiate(list[index]);
        box = hand.GetComponent<MeshCollider>();
        if (hand.tag == "Foundation")
        {
            hand.GetComponent<MeshRenderer>().material = readyMat;
        }
        else if (hand.tag == "Wall")
        {
            hand.GetComponent<MeshRenderer>().material = readyMat;
        }
        box.enabled = false;
    }
    public void button2()
    {
        index = 1;
        Destroy(hand);
        hand = Instantiate(list[index]);
        box = hand.GetComponent<MeshCollider>();
        if (hand.tag == "Foundation")
        {
            hand.GetComponent<MeshRenderer>().material = readyMat;
        }
        else if (hand.tag == "Wall")
        {
            hand.GetComponent<MeshRenderer>().material = readyMat;
        }
        box.enabled = false;
    }
    public void button3()
    {
        index = 2;
        Destroy(hand);
        hand = Instantiate(list[index]);
        box = hand.GetComponent<MeshCollider>();
        if (hand.tag == "Foundation")
        {
            hand.GetComponent<MeshRenderer>().material = readyMat;
        }
        else if (hand.tag == "Wall")
        {
            hand.GetComponent<MeshRenderer>().material = readyMat;
        }
        box.enabled = false;
    }
    public void UiOn()
    {
        
         
        gui.SetActive(activeGui);
        Cursor.visible = activeGui;
        if (activeGui)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        activeGui = !activeGui;
      

    }
}
