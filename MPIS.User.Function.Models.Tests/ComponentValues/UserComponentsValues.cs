using MPIS.User.AplicationService.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPIS.User.Function.Models.Tests.ComponentValues
{
    public class UserComponentsValues
    {
        public static Guid GuidAvailable { get; set; }
        static Guid GuidDefault { get; set; }
        public static Guid GetUserGuidAvailable() => GuidAvailable;
        public static Guid GetuserDefault() => default;
        public static Guid GetUserNewGuid() => Guid.NewGuid();
        public static Guid GetUserAviability() => UserComponentsValues.GuidAvailable;


        #region "Create"

        public static CreateUserRequest CreateUserRequestBasic() => new CreateUserRequest()
        {
            Name = "Luis",
            Surname = "Fernández",
            Office = "Oficina 1",
            Address = "C prueba",
            Phone = "1234567",
            Email = "ejemplo@gmail.com",
            Password = "passwd"

        };
        public static CreateUserRequest CreateUserRequestWithoutOffice() => new CreateUserRequest()
        {
            Name = "Luis",
            Surname = "Fernández",
            Address = "C prueba",
            Phone = "1234567",
            Email = "ejemplo@gmail.com",
            Password = "passwd"

        };

        public static CreateUserRequest CreateUserRequestEmptyOffice() => new CreateUserRequest()
        {
            Name = "Luis",
            Surname = "Fernández",
            Office = "",
            Address = "C prueba",
            Phone = "1234567",
            Email = "ejemplo@gmail.com",
            Password = "passwd"

        };

        public static CreateUserRequest CreateUserRequestEmpty()
        {
            CreateUserRequest val = new CreateUserRequest();
            return val;
        }

        public static CreateUserRequest CreateUserRequestWithoutName() => new CreateUserRequest()
        {
            Surname = "Fernández",
            Office = "Oficina 1",
            Address = "C prueba",
            Phone = "1234567",
            Email = "ejemplo@gmail.com",
            Password = "passwd"

        };
        public static CreateUserRequest CreateUserRequestEmptyName() => new CreateUserRequest()
        {
            Name = "",
            Surname = "Fernández",
            Office = "Oficina 1",
            Address = "C prueba",
            Phone = "1234567",
            Email = "ejemplo@gmail.com",
            Password = "passwd"

        };

        public static CreateUserRequest CreateUserRequestWithoutSurame() => new CreateUserRequest()
        {
            Name = "Luis",
            Office = "Oficina 1",
            Address = "C prueba",
            Phone = "1234567",
            Email = "ejemplo@gmail.com",
            Password = "passwd"

        };

        public static CreateUserRequest CreateUserRequestEmptySurame() => new CreateUserRequest()
        {
            Name = "Luis",
            Surname = "",
            Office = "Oficina 1",
            Address = "C prueba",
            Phone = "1234567",
            Email = "ejemplo@gmail.com",
            Password = "passwd"

        };

        public static CreateUserRequest CreateUserRequestWithoutAddress() => new CreateUserRequest()
        {
            Name = "Luis",
            Surname = "Fernández",
            Office = "Oficina 1",
            Phone = "1234567",
            Email = "ejemplo@gmail.com",
            Password = "passwd"

        };

        public static CreateUserRequest CreateUserRequestEmptyAddress() => new CreateUserRequest()
        {
            Name = "Luis",
            Surname = "Fernández",
            Office = "Oficina 1",
            Address = "",
            Phone = "1234567",
            Email = "ejemplo@gmail.com",
            Password = "passwd"

        };

        #endregion

        #region "Update"

        public static UpdateUserRequest UpdateUserRequest() => new UpdateUserRequest()
        {
            Id = GuidAvailable,
            Name = "Luis",
            Surname = "Fernández",
            Office = "Oficina 1",
            Address = "C prueba",
            Phone = "1234567",
            Email = "ejemplo@gmail.com",
            Password = "passwd"
        };

        public static UpdateUserRequest UpdateUserRequestWithoutName() => new UpdateUserRequest()
        {
            Id = GuidAvailable,
            Surname = "Fernández",
            Office = "Oficina 1",
            Address = "C prueba",
            Phone = "1234567",
            Email = "ejemplo@gmail.com",
            Password = "passwd"

        };

        public static UpdateUserRequest UpdateUserRequestEmptyName() => new UpdateUserRequest()
        {
            Id = GuidAvailable,
            Name = "",
            Surname = "Fernández",
            Office = "Oficina 1",
            Address = "C prueba",
            Phone = "1234567",
            Email = "ejemplo@gmail.com",
            Password = "passwd"

        };

        public static UpdateUserRequest UpdateUserRequestWithoutSurame() => new UpdateUserRequest()
        {
            Id = GuidAvailable,
            Name = "Luis",
            Office = "Oficina 1",
            Address = "C prueba",
            Phone = "1234567",
            Email = "ejemplo@gmail.com",
            Password = "passwd"

        };

        public static UpdateUserRequest UpdateUserRequestEmptySurame() => new UpdateUserRequest()
        {
            Id = GuidAvailable,
            Name = "Luis",
            Surname = "",
            Office = "Oficina 1",
            Address = "C prueba",
            Phone = "1234567",
            Email = "ejemplo@gmail.com",
            Password = "passwd"

        };

        public static UpdateUserRequest UpdateUserRequestWithoutOffice() => new UpdateUserRequest()
        {
            Id = GuidAvailable,
            Name = "Luis",
            Surname = "Fernández",
            Address = "C prueba",
            Phone = "1234567",
            Email = "ejemplo@gmail.com",
            Password = "passwd"

        };

        public static UpdateUserRequest UpdateUserRequestEmptyOffice() => new UpdateUserRequest()
        {
            Id = GuidAvailable,
            Name = "Luis",
            Surname = "Fernández",
            Office = "",
            Address = "C prueba",
            Phone = "1234567",
            Email = "ejemplo@gmail.com",
            Password = "passwd"

        };

        public static UpdateUserRequest UpdateUserRequestWithoutAddress() => new UpdateUserRequest()
        {
            Id = GuidAvailable,
            Name = "Luis",
            Surname = "Fernández",
            Office = "Oficina 1",
            Phone = "1234567",
            Email = "ejemplo@gmail.com",
            Password = "passwd"

        };

        public static UpdateUserRequest UpdateUserRequestEmptyAddress() => new UpdateUserRequest()
        {
            Id = GuidAvailable,
            Name = "Luis",
            Surname = "Fernández",
            Office = "Oficina 1",
            Address = "",
            Phone = "1234567",
            Email = "ejemplo@gmail.com",
            Password = "passwd"

        };
        #endregion

        #region "Get"

        public static GetUserByIdRequest GetUserByIdRequestAviability() => new GetUserByIdRequest()
        {
            Id = GuidAvailable
        };

        public static GetUserByIdRequest GetUserByIdRequestNewGuid() => new GetUserByIdRequest()
        {
            Id = GetUserNewGuid()
        };

        public static GetUserByIdRequest GetUserByIdRequestDefault() => new GetUserByIdRequest()
        {
            Id = GuidDefault
        };

        #endregion

        #region "Delete"

        public static DeleteUserByIdRequest DeleteUserRequestAviability() => new DeleteUserByIdRequest()
        {
            Id = GuidAvailable
        };

        public static DeleteUserByIdRequest DeleteUserRequestNewGuid() => new DeleteUserByIdRequest()
        {
            Id = GetUserNewGuid()
        };

        public static DeleteUserByIdRequest DeleteUserRequestDefault() => new DeleteUserByIdRequest()
        {
            Id = GuidDefault
        };

        #endregion
    }
}
