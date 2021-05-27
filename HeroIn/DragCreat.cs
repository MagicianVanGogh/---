using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using UnityEngine.AI;

public class DragCreat : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{
    GameObject go;
    bool isPlayer = true;
    public GameObject vec;//默认前进点

    public GameObject prefab;
    public GameObject poss;

    public Text costUI;
    public int cost;

    private void Start()
    {
        costUI.text = cost.ToString();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (prefab != null)
        {
            if (go == null)
            {
                go = Instantiate(prefab);
                go.GetComponent<HeroController>().vec = vec;
                go.transform.position = poss.transform.position;
            }
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (go != null)
        {
            if (go.transform.position == poss.transform.position)
            {
                Destroy(go);
            }
            else
            {
                if (!GameManager.instance.UseCost(-cost))
                {
                    Destroy(go);
                }                
            }
        }       
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (go != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)&&!hit.collider.gameObject.CompareTag("wall"))
            {
                if (hit.collider.gameObject.CompareTag("enemy"))
                {
                    isPlayer = false;
                }
                else
                {
                    isPlayer = true;
                }
                Vector3 pos = hit.point;
                go.transform.position = pos;
            }
            else
            {
                //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                go.transform.position = poss.transform.position;
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (go != null)
        {
            
            go.GetComponent<NavMeshAgent>().enabled = true;
            go.GetComponent<HeroController>().enabled = true;
            go.GetComponent<CapsuleCollider>().enabled = true;
            go.GetComponent<AttackedController1>().SetTarget();
            go.GetComponent<HeroController>().ResetInfo(isPlayer);
            go = null;
        }        
    }
}
