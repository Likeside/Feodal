using Game.CoreGameplay.Effect;

namespace Game.CoreGameplay.Injections {
    public interface INumbersValueHolder {
        
        public float GetNumberValue(string numberName);

        public Number GetNumber(string numberName);
    }
}