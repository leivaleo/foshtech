using Microsoft.AspNetCore.Mvc;

namespace Backend.TechChallenge.TestHelpers
{
    public static class ObjectHelpes
    {
        public static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}
