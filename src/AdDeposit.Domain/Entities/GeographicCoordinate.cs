namespace AdDeposit.Domain.Entities
{
    public sealed class GeographicCoordinate
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public GeographicCoordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}