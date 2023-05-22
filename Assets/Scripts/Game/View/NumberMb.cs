using System.Globalization;
using Game.CoreGameplay.Effect;
using TMPro;
using UniRx;
using UnityEngine;

namespace Game.View {
    public class NumberMb: DisposableMonoInjection {
        [SerializeField] TextMeshProUGUI _text;

        
        public void SubscribeToNumber(Number number) {
            number.Value.Subscribe( _ => _text.text = _.ToString(CultureInfo.InvariantCulture) ).AddTo(_disposable);
        }
    }
}