using Backend.TechChallenge.CrossCutting.Base;
using Backend.TechChallenge.CrossCutting.Enums;

namespace Backend.TechChallenge.Infrastructure.Interfaces.Models
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserTypeEnum UserType { get; set; }
        public decimal Money { get; set; }
    }
}
