using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Product Added";
        public static string ProductNameInvalid = "Product name invalid";
        public static string MaintanenceTime = "System in maintanence";
        public static string ProductCountOfCategoryInvalid = "You cannot added";
        public static string AuthorizationDenied = "No Permission";
        public static string UserRegistered = "User successfully registered";
        //public static User UserNotFound = Messages.UserNotFound;
        //public static User PasswordError = Messages.PasswordError;
        public static string SuccessfulLogin = "Successfull login";
        public static string UserAlreadyExist = "This account already exist";
        public static string AccessTokenCreated = "You can use your new token";
        internal static string UserAlreadyExists = "User is already exist";
        internal static string ProductNameAlreadyExists;
        internal static string ProductUpdated;
    }
}
