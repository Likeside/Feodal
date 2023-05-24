using System.Collections.Generic;
using System.Linq;
using Game.CoreGameplay.Effect;
using UnityEngine;

namespace Game.View {
    public class EffectBlockMb: PoolerMonoInjection {
        EffectType _type;
        List<Effect> _effectsToDisplay;
        List<EffectMb> _currentEffectsMb;
        
        public void DisplayEffects(List<Effect> allEffects) {
            _effectsToDisplay = allEffects.Where(e => e.Type == _type && e.TurnsToCompleteList.Count > 0)
                .OrderBy(_ => _.Name).ToList(); //среди всех эффектов находим те, которые должны быть отображены в ЭТОМ БЛОКЕ и сортируем по алфавиту
            int totalEffectsToDisplay = 0;
            foreach (var eff in _effectsToDisplay) {
                totalEffectsToDisplay += eff.TurnsToCompleteList.Distinct().Count(); //подсчитываем количество вью, учитывая, что стакаем одинаковые эффекты с одинаковым количеством ходов
            }
            int countDifference = totalEffectsToDisplay - _currentEffectsMb.Count; // вычисляем разницу между текущим вью и количеством вью, которое должно быть отображено
            AddOrRemoveEffectsMb(countDifference); //добавляем или удаляем вьюшки соответственно
            
            if (totalEffectsToDisplay != _currentEffectsMb.Count) {
                Debug.LogError("Mb count != effect count"); 
            }
            int i = 0;
            foreach (var eff in _effectsToDisplay) { //на каждый эффект, который должен быть отражен, мы: 
                foreach (var distinctTurn in eff.TurnsToCompleteList.Distinct()) { 
                    int count = eff.TurnsToCompleteList.Count(_ => _ == distinctTurn); // подсчитали, сколько каждого уникального количества ходов в эффекте
                    if (_currentEffectsMb[i].CurrentEffect == eff) {
                        _currentEffectsMb[i].RefreshTurnsAndCount(distinctTurn.Value, count); //если во вьюшке уже тот же самый эффект отображается, то только обновляем количество ходов и количество эффектов
                    }
                    else {
                        _currentEffectsMb[i].Display(eff, distinctTurn.Value, count); //если другой эффект, то заменяем эффект
                    }
                    i++;
                }
            }
        }
        
        void AddEffectsMb(int count) {
            for (int i = 0; i < count; i++) {
                _currentEffectsMb.Add(_pooler.SpawnFromPoolComp<EffectMb>()); //TODO: решит вопрос с пулингом
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
        
    }
}