using System.Collections.Generic;
using System.Linq;
using Game.CoreGameplay.Effect;
using Game.CoreGameplay.Injections;
using GameScripts;
using UnityEngine;

namespace Game.View {
    public class EffectBlockMb: MonoBehaviour {

        [SerializeField] Transform _effectsMbParent;
        
        
        EffectType _type;
        List<Effect> _effectsToDisplay;
        List<EffectMb> _currentEffectsMb = new ();
        ObjectPooler _pooler;
        IDataHolder _holder;


        public void Initialize(EffectType type, ObjectPooler pooler, IDataHolder holder) { //TODO: inject holder somewhere else
            _type = type;
            _pooler = pooler;
            _holder = holder;
        }
        
        public void DisplayEffects(List<Effect> allEffects) {
            _effectsToDisplay = allEffects.Where(e => e.Type == _type && e.TurnsToCompleteList.Count > 0)
                .OrderBy(_ => _.Name).ToList(); //среди всех эффектов находим те, которые должны быть отображены в ЭТОМ БЛОКЕ и сортируем по алфавиту
            Debug.Log("Effects to display count: " + _effectsToDisplay.Count);
            int totalEffectsToDisplay = 0;
            foreach (var eff in _effectsToDisplay) {
                totalEffectsToDisplay += eff.GetDistinctTurns().Count;  //подсчитываем количество вью, учитывая, что стакаем одинаковые эффекты с одинаковым количеством ходов
            }
            Debug.Log("TOTAL EFFECTSMB TO DISPLAY: " + totalEffectsToDisplay);
            int countDifference = totalEffectsToDisplay - _currentEffectsMb.Count; // вычисляем разницу между текущим вью и количеством вью, которое должно быть отображено
            AddOrRemoveEffectsMb(countDifference); //добавляем или удаляем вьюшки соответственно
            
            if (totalEffectsToDisplay != _currentEffectsMb.Count) {
                Debug.LogError("Mb count != effect count"); 
            }
            int i = 0;
            foreach (var eff in _effectsToDisplay) { //на каждый эффект, который должен быть отражен, мы: 
                foreach (var distinctTurn in eff.GetDistinctTurns()) { 
                    int count = eff.TurnsToCompleteList.Count(_ => _.Value == distinctTurn); // подсчитали, сколько каждого уникального количества ходов в эффекте
                    if (_currentEffectsMb[i].CurrentEffect == eff) {
                        _currentEffectsMb[i].RefreshTurnsAndCount(distinctTurn, count); //если во вьюшке уже тот же самый эффект отображается, то только обновляем количество ходов и количество эффектов
                    }
                    else {
                        _currentEffectsMb[i].Display(eff, distinctTurn, count); //если другой эффект, то заменяем эффект
                    }
                    i++;
                }
            }
            DisplayInBlock();
        }
        
        void AddEffectsMb(int count) {
            for (int i = 0; i < count; i++) {
                var effectMb = _pooler.SpawnFromPoolComp<EffectMb>();
                effectMb.Initialize(_holder);
                _currentEffectsMb.Add(effectMb); //TODO: решит вопрос с пулингом
            }
        }

        void RemoveEffectsMb(int count) {
            for (int i = _currentEffectsMb.Count - 1; i > _currentEffectsMb.Count - 1 - count ; i--) {
                _pooler.ReturnToPool(_currentEffectsMb[i].gameObject);
                _currentEffectsMb.RemoveAt(i);
            }
        }

        void AddOrRemoveEffectsMb(int countDifference) {
            if (countDifference > 0) {
                AddEffectsMb(countDifference);
            }
            else if (countDifference < 0) {
                RemoveEffectsMb(-countDifference);
            }
        }

        void DisplayInBlock() {
          _currentEffectsMb = _currentEffectsMb.OrderBy(_ => _.CurrentEffect.Name).ToList();
            foreach (var effectMb in _currentEffectsMb) {
                effectMb.transform.SetParent(_effectsMbParent);
            }
        }
        
    }
}