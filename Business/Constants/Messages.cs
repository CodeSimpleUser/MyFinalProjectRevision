using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static readonly Base64FormattingOptions Random;
        public static readonly string AccessSessionCreated = "SessionId is created";
        public static string ProductAdded = "Product Added";
        public static string ProductNameInvalid = "Product name invalid";
        public static string MaintanenceTime = "System in maintanence";
        public static string ProductCountOfCategoryInvalid = "You cannot added";
        public static string AuthorizationDenied = "No Permission";
        public static string UserRegistered = "User successfully registered";
        public static string UserNotFound = "User not found";
        public static string PasswordError = "Password is wrong";
        public static string SuccessfulLogin = "Successfull login";
        public static string UserAlreadyExist = "This account already exist";
        public static string AccessTokenCreated = "You can use your new token";
        public static string UserAlreadyExists = "User is already exist";
        public static string ProductNameAlreadyExists = "Product name is already exist";
        public static string ProductUpdated = "Product updated";
        public static string AllProducts = "All Products are displayed";

        public static string AllUsers = "All users showed successfully";
        public static string AllUsersError = "All users cannot be showed";
        public static object? InvalidRequest;
    }
}
