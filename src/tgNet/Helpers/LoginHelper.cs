using System;
using System.Web;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Net.Interfaces;
using TreeGecko.Library.Net.Objects;

namespace TreeGecko.Library.Net.Helpers
{
    public static class LoginHelper
    {
        public static TGUser Login(IServerDataManager _sdm, Guid _userGuid, string _password)
        {
            TGUser user = _sdm.GetUser(_userGuid);

            if (user != null)
            {
                if (user.Active)
                {
                    if (user.IsVerified)
                    {
                        if (_sdm.ValidateUser(user, _password))
                        {
                            return user;
                        }

                        //Passwords don't match or they were not supplied.
                        _sdm.LogWarning(_userGuid, "Passwords don't match or they were not supplied.");
                    }
                    else
                    {
                        //User isn't verified
                        _sdm.LogWarning(_userGuid, "User isn't verified.");
                    }
                }
                else
                {
                    //User isn't active.
                    _sdm.LogWarning(_userGuid, "User isn't active.");
                }
            }
            else
            {
                //User not found
                _sdm.LogWarning(_userGuid, "User not found.");
            }

            return null;
        }

        public static TGUser LoginFromAuthorization(IServerDataManager _sdm, 
                                                    Guid _userGuid, 
                                                    string _authorizationToken)
        {
            TGUser user = _sdm.GetUser(_userGuid);

            if (user != null)
            {
                if (user.Active)
                {
                    if (user.IsVerified)
                    {
                        TGUserAuthorization userAuthorization = 
                            _sdm.GetUserAuthorization(_userGuid, _authorizationToken);

                        if (userAuthorization != null)
                        {
                            if (userAuthorization.ValidateAuthorizationToken(_authorizationToken))
                            {
                                _sdm.Persist(userAuthorization);

                                return user;
                            }
                            
                            //Passwords don't match or they were not supplied.
                            _sdm.LogWarning(_userGuid, "Passwords don't match or they were not supplied.");
                        }
                        else
                        {
                            _sdm.LogWarning(_userGuid, "User authorization not found.");
                        }
                    }
                    else
                    {
                        //User isn't verified
                        _sdm.LogWarning(_userGuid, "User isn't verified.");

                    }
                }
                else
                {
                    //User isn't active.
                    _sdm.LogWarning(_userGuid, "User isn't active.");
                }
            }
            else
            {
                //User not found
                _sdm.LogWarning(_userGuid, "User not found.");
            }

            return null;
        }

        public static void Logout(HttpContext _context,
                                  string _domainName)
        {
            HttpResponse response = _context.Response;
            response.Cookies["UserSettings"]["UserName"] = null;
            response.Cookies["UserSettings"]["AuthorizationToken"] = null;
            response.Cookies["UserSettings"].Expires = DateTime.Now.AddDays(-30);
            response.Cookies["UserSettings"].Domain = _domainName;

            _context.Session.Clear();
        }

        public static bool LoginAndCreateSession(IServerDataManager _sdm, HttpContext _context)
        {
            if (_context != null)
            {
                //Check to see if we already have a session
                TGUser user = (TGUser)_context.Session["User"];
                if (user != null)
                {
                    return true;
                }

                //Ok we don't have a session
                HttpRequest request = _context.Request;

                //First try the headers
                string tUserGuid = request.Headers["UserGuid"];
                if (GuidHelper.IsValidGuidString(tUserGuid))
                {
                    Guid userGuid = new Guid(tUserGuid);
                    string authorizationToken = request.Headers["AuthorizationToken"];

                    user = LoginFromAuthorization(_sdm, userGuid, authorizationToken);

                    //Did we get a user?
                    if (user != null)
                    {
                        //yup.  Store it in the server session
                        _context.Session["User"] = user;
                        return true;
                    }
                }
                else
                {
                    string authorizationToken;
                    Guid userGuid;

                    //No don't have headers, lets try for cookies
                    GetCookieValues(request, out userGuid, out authorizationToken);

                    if (!userGuid.Equals(Guid.Empty))
                    {
                        user = LoginFromAuthorization(_sdm, userGuid, authorizationToken);

                        //Did we get a user?
                        if (user != null)
                        {
                            //yup.  Store it in the server session
                            _context.Session["User"] = user;
                            return true;
                        }
                    }
                }
            }

            //Didn't find header, cookie, or it was a bad login.
            return false;
        }

        public static void GetCookieValues(HttpRequest _request, 
                                           out Guid _userGuid, 
                                           out string _authorizationToken)
        {
            _userGuid = Guid.Empty;
            _authorizationToken = null;

            if (_request.Cookies["UserSettings"] != null)
            {
                string temp = _request.Cookies["UserSettings"]["UserName"];
                if (GuidHelper.IsValidGuidString(temp))
                {
                    _userGuid = new Guid(temp);
                }

                _authorizationToken = _request.Cookies["UserSettings"]["AuthorizationToken"];
            }
        }

        public static void RememberLogin(HttpResponse _response, 
                                         TGUserAuthorization _userAuthorization,
                                         string _domainName)
        {
            if (_response != null)
            {
                _response.Cookies["UserSettings"]["UserGuid"] = _userAuthorization.ParentGuid.ToString();
                _response.Cookies["UserSettings"]["AuthorizationToken"] = _userAuthorization.AuthorizationToken;
                _response.Cookies["UserSettings"].Expires = DateTime.Now.AddDays(30);
                _response.Cookies["UserSettings"].Domain = _domainName;
            }
        }
    }
}
