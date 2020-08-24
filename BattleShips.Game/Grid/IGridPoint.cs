namespace BattleShips.Game.Grid
{
    public interface IGridPoint
    {
        int X { get; }
        int Y { get; }
        bool Alive { get; }
        void HitGridPoint();
    }
}