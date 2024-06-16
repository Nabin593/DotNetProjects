namespace DatingApp.Extensions
{
    public  static class DateTimeExtension
    {
        public static int CalculateAge(this DateOnly dob)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var age = today.Year - today.Month;
            if (dob > today.AddYears(-age)) age--;
            return age;
        }
    }
}
