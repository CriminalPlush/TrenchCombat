using UnityEngine;


public static class TrenchFinder
{

    // Update is called once per frame
    public static Vector3? FindNext(Unit unit, int index)
    {
        Vector3 unitPos = unit.gameObject.transform.position;
        Vector3 destination = new Vector3(int.MaxValue, int.MaxValue, int.MaxValue);
        foreach (TrenchSlot trenchSlot in GameObject.FindObjectsOfType<TrenchSlot>())
        {
            if (trenchSlot.unit == null)
            {
                if ((Vector3.Distance(unitPos, destination) > Vector3.Distance(unitPos, trenchSlot.transform.position) || destination == null)
                    && (trenchSlot.gameObject.GetComponentInParent<Trench>().index == index))
                {
                    destination = trenchSlot.transform.position;
                }
            }
        }
        if (destination == new Vector3(int.MaxValue, int.MaxValue, int.MaxValue))
        {
            return null;
        }
        else
        {
            return destination;
        }
    }
    public static Trench FindTrenchByIndex(int index)
    {
        Trench trench = null;
        foreach (Trench _trench in GameObject.FindObjectsOfType<Trench>())
        {
            if (_trench.index == index)
            {
                trench = _trench;
            }
        }
        return trench;
    }
}
