using static VehicleReservation.Test.Testing;
namespace VehicleReservation.Test
{
    public class TestBase
    {
        [SetUp]
        public async Task SetUp()
        {
            await ResetState();
        }
    }
}
