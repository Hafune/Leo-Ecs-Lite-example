using Reflex.Scripts.Attributes;
using UnityEngine;
using Container = Reflex.Container;

namespace Lib
{
    public abstract class MonoConstruct : MonoBehaviour
    {
        [Inject]
        protected abstract void Construct(Container context); 
    }
    
    public interface IMonoConstruct
    {
        [Inject]
        protected abstract void Construct(Container context); 
    }
}