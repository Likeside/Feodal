using System.Collections.Generic;
using System.Globalization;
using Game.CoreGameplay.Effect;
using Game.CoreGameplay.Injections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.View {
    public class EffectMb: MonoBehaviour {

        [SerializeField] List<EffectModDisplay> _modificationImages;
        [SerializeField] Image _icon;
        [SerializeField] TextMeshProUGUI _turnsText;
        [SerializeField] GameObject _turnsObj;


        protected IDataHolder _holder;

        [Inject]
        public void SetHolder(IDataHolder holder) {
            _holder = holder;
        }
        
        public virtual void Display(Effect effect, int turns) {
            int modCount = effect.ModificationsName.Count;
            _turnsObj.SetActive(turns >= 0);
            _turnsText.text = turns.ToString();


            _icon.sprite = _holder.GetEffectIconByName(effect.Name);
            
            for (int i = 0; i < _modificationImages.Count; i++) {
                if (i < modCount) {
                    _modificationImages[i].gameObject.SetActive(true);
                    var mod = _holder.GetModificationByName(effect.ModificationsName[i]);
                    _modificationImages[i].Set(_holder.GetModificationIconByName(effect.ModificationsName[i]),
                        mod.ModificationValue
                            .ToString(CultureInfo.InvariantCulture));
                    if (mod is ModificationMission mission) {
                        _modificationImages[i].SetMissionPercent(mission.SuccessChanceValue.ToString(CultureInfo.InvariantCulture));
                    }
                }
                else {
                    _modificationImages[i].gameObject.SetActive(false);
                }
            }
        }
    }
}