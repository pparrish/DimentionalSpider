using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Weapons;

namespace Common
{
    public class ControlEventBus : ScriptableObject
    {
        // Events
        public UnityEvent<Vector2> OnMove { get; } =  new UnityEvent<Vector2>();
        public UnityEvent<bool> OnTurbo { get; } = new UnityEvent<bool>();
        
        // Weapons
        [SerializeField] protected WeaponEventBus weaponEventBus;
        protected bool ShootActive;
        
        //Loops
        [SerializeField] public int buttonEventUpdateTime = 16;
        protected delegate void LoopEventDelegate();
        protected LoopEventDelegate LoopEvent;
        
        private async void ManageButtonEvents()
        {
            while (buttonEventUpdateTime > 0)
            {
                await Task.Delay(buttonEventUpdateTime);
                LoopEvent();
            }
        }
        
        protected virtual void OnEnable()
        {
            ManageButtonEvents();
            LoopEvent += () =>
            {
                if (ShootActive) weaponEventBus.Shoot();
            };
        }
        
    }
}