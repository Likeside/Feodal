using Game.CoreGameplay.Effect;
using TMPro;
using UnityEngine;

namespace Game.View {
    public class CardMb: EffectMb {
        [SerializeField] TextMeshProUGUI _description;
        public override void Display(Effect effect, int turns) {
            _description.text = _holder.GetCardDescriptionByName(effect.Name);
            base.Display(effect, turns);
        }
    }
}