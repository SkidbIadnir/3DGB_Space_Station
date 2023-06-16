using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStat
{
    [SerializeField]int hp = 300;
    [SerializeField]int max_hp = 300;
    [SerializeField]int def;
    [SerializeField]int att;

    [SerializeField]int spdAtt;

    [SerializeField]int range;
    // [SerializeField]
    // [SerializeField]
    // Start is called before the first frame update

    public int HP {
        get {return hp;}
        set {hp = (value > max_hp) ? value : max_hp;}
    }

    public int MAXHP {
        get {return max_hp;}
        set {max_hp = value;}
    }

    public int DEF {
        get {return def;}
        set {def = value;}
    }

    public int ATT {
        get {return att;}
        set {att = value;}
    }

    public int ATTSPEED {
        get {return spdAtt;}
        set {spdAtt = value;}
    }

    public int RANGE {
        get {return range;}
        set {range = value;}
    }
}
