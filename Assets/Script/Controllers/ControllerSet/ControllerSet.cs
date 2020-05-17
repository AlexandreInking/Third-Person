using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerSet : MonoBehaviour
{
    public Character controlledCharacter;
    public List<Controller> controllers = new List<Controller>();

    protected void InitializeController<C>() where C : Controller
    {
        Controller controller = gameObject.AddComponent<C>();
        controller.controllerSet = this;
        controller.controllerCharacter = controlledCharacter;
        controllers.Add(controller);
    }

    public T GetController<T>() where T : Controller
    {
        return ( T ) controllers.Find(x => x is T);
    }
}
