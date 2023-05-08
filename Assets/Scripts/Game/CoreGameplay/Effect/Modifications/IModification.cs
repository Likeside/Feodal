namespace Game.CoreGameplay.Effect {
    public interface IModification {

        public string Name { get; }
        public void Modify(int turns);
    }
}