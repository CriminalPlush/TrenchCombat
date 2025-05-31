using UnityEngine;

public static class TrenchFinder
{

    // Update is called once per frame
    public static Vector3? Find(Vector3 unitPos)
    {
        Vector3 destination = new Vector3(int.MaxValue, int.MaxValue, int.MaxValue);
        foreach (TrenchSlot trench in GameObject.FindObjectsOfType<TrenchSlot>())
        {
            if (trench.unit == null)
            {
                if (Vector3.Distance(unitPos, destination) > Vector3.Distance(unitPos, trench.transform.position) || destination == null)
                {
                    destination = trench.transform.position;
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
}
