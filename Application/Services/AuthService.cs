using MAUIPos.Application.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIPos.Application.Services
{
    public class AuthService
    {
        private readonly CacheSystemService _cacheSystemService;
        public AuthService(CacheSystemService cacheSystemService)
        {
            _cacheSystemService = cacheSystemService;
        }
        public async Task<AuthenticationStatus> GetToken()
        {
            try
            {
                var _cacheSystemService = MauiProgram.Services.GetService<CacheSystemService>();
                var _token = _cacheSystemService?.GetCacheString(ConstantString.TOKEN);
                if (string.IsNullOrEmpty(_token))
                {
                    return AuthenticationStatus.UnAuthenticated;
                }
                else
                {
                    return AuthenticationStatus.Authenticated;
                }
            }
            catch (Exception ex)
            {
                return AuthenticationStatus.Unknown;
            }
        }
        public async Task<bool> LoginStatus()
        {
            return false;
        }

        public async Task<bool> LogIn()
        {
            return false;
        }

        public async Task<bool> Register()
        {
            return false;
        }

        public void LogOut()
        {
            _cacheSystemService.CacheClearAll();
        }
    }

    public enum AuthenticationStatus
    {
        Unknown,
        Authenticated,
        UnAuthenticated
    }
}
