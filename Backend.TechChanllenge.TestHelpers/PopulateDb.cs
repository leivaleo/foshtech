using Backend.TechChallenge.CrossCutting.Enums;
using Backend.TechChallenge.Infrastructure.Interfaces.Models;

namespace Backend.TechChallenge.TestHelpers
{
    public static class PopulateDb
    {
        public static readonly DateTime InitialDateTime = new DateTime(2023, 23, 3);

        public static void UserTable(TechCallengeDbContext context)
        {
            context.AddRange(
                new User
                {
                    Guid = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name = "Normal user to test",
                    Address = "Address of normal user",
                    Email = "Email of normal user",
                    Phone = "11111111",
                    UserType = UserTypeEnum.Normal,
                    Money = 100
                },
                new User
                {
                    Guid = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name = "SuperUser user to test",
                    Address = "Address of superUser",
                    Email = "Email of superUser",
                    Phone = "22222222",
                    UserType = UserTypeEnum.SuperUser,
                    Money = 200
                },
                new User
                {
                    Guid = new Guid("00000000-0000-0000-0000-000000000003"),
                    Name = "PremiumUser user to test",
                    Address = "Address of premiumUser",
                    Email = "Email of premiumUser",
                    Phone = "3333333",
                    UserType = UserTypeEnum.Premium,
                    Money = 1000
                }
            );
        }
    }

}
