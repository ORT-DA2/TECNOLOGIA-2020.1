using System;

namespace Moodle.BusinessLogic.Interface
{
    public interface ISessionLogic
    {
        Guid Login(string credential, string password);
        bool IsValidToken(Guid token);
    }
}