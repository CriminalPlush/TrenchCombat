using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpelUnitButton : MonoBehaviour
{
    [SerializeField]
    private ExpelUnitCommand expelUnitCommand;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnClick()
    {
        expelUnitCommand.Expel(gameObject.GetComponent<UnitInfoSlot>().unitInfo);
    }
}
