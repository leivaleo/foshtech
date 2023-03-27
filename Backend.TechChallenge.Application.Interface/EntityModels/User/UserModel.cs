using Backend.TechChallenge.CrossCutting.Base;
using Backend.TechChallenge.CrossCutting.Enums;

namespace Backend.TechChallenge.Application.Interfaces.EntityModels.User
{
    public class UserModel : EntityModelBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserTypeEnum UserType { get; set; }
        public decimal Money { get; set; }


        public static string ValidateErrors(UserModel user)
        {
            if (user == null) 
                return "User data is null";

            var errors = "";

            if (String.IsNullOrEmpty(user.Name))
                //Validate if Name is null
                errors = "The name is required";
            if (String.IsNullOrEmpty(user.Email))
                //Validate if Email is null
                errors = errors + " The email is required";
            if (String.IsNullOrEmpty(user.Address))
                //Validate if Address is null
                errors = errors + " The address is required";
            if (String.IsNullOrEmpty(user.Phone))
                //Validate if Phone is null
                errors = errors + " The phone is required";

            return errors;
        }
    }
}
