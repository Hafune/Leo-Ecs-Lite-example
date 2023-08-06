using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.UI;
using Voody.UniLeo.Lite;

namespace Core
{
    public class SpawnButton : MonoConstruct
    {
        [SerializeField] private Button _button;
        [SerializeField] private ConvertToEntity _unit;

        private Container _context;

        protected override void Construct(Container context) => _context = context;

        private void Awake() => _button.onClick.AddListener(OnClick);

        private void OnClick() => _context.Instantiate(_unit);
    }
}