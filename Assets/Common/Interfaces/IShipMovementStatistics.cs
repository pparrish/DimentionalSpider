namespace Common
{
    public interface IShipMovementStatistics
    {
        public float GetAcceleration();
        public float GetMaxVelocity();
        
        public float GetStopAcceleration();
    }
}