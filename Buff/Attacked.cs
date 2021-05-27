using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacked : MonoBehaviour
{
    public bool isPlayer;
    public float damage;
    public HeroController hero;
    public List<AttackedController1> atts;
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        Invoke("AddBuff", 0.1f);
        atts.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        AttackedController1 at = other.gameObject.GetComponent<AttackedController1>();
        if (at != null && at.isPlayer != isPlayer)
        {
            atts.Add(at);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        AttackedController1 at = other.gameObject.GetComponent<AttackedController1>();
        if (at != null && at.isPlayer != isPlayer)
        {
            atts.Remove(at);
        }
    }
    void AddBuff()
    {
        for (int i = 0; i < atts.Count; i++)
        {
            if (atts[i] != null)
            {
                atts[i].attackedWithDamage(damage * 1.2f,hero);                
                Playback pl = atts[i].gameObject.AddComponent<Playback>();
                pl.SetMessage(1f, this.transform);
            }
        }
    }
}
