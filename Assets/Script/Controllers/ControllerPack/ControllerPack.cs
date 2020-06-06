using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerPack : MonoBehaviour, IControllerPack
{
    public Character controlledCharacter;
    public List<Controller> controllers = new List<Controller>();

    protected void InitializeController<C>() where C : Controller
    {
        Controller controller = gameObject.AddComponent<C>();

        controller.controllerPack = this;
        controller.controlledCharacter = controlledCharacter;
        controllers.Add(controller);

        controller.OnInitialize();
    }

    public abstract void OnInitialize();

    public T GetController<T>() where T : Controller
    {
        return ( T ) controllers.Find(x => x is T);
    }
}
