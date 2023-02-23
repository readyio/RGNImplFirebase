using RGN.Modules.SignIn;
using UnityEngine;

namespace RGN.Impl.Firebase
{
    public class RGNUnityInitializer : MonoBehaviour
    {
        [SerializeField] private IInitializable[] _initializables;
        [SerializeField] private bool _autoGuestLogin = true;

        private bool _initialized = false;
        private async void Awake()
        {
            if (_initialized)
            {
                return;
            }

#if UNITY_STANDALONE_WIN
            EmailSignInModule.InitializeWindowsDeepLink();
#endif

            RGNCoreBuilder.CreateInstance(new Dependencies());
            RGNCore.I.AuthenticationChanged += OnAuthenticationChanged;
            await RGNCoreBuilder.BuildAsync();
            for (int i = 0; i < _initializables.Length; ++i)
            {
                await _initializables[i].InitAsync();
            }
            _initialized = true;
        }
        private void OnDestroy()
        {
            for (int i =0; i < _initializables.Length; ++i)
            {
                _initializables[i].Dispose();
            }
            RGNCoreBuilder.Dispose();
        }

        private void OnAuthenticationChanged(EnumLoginState enumLoginState, EnumLoginError error)
        {
            if (_autoGuestLogin && enumLoginState == EnumLoginState.NotLoggedIn)
            {
                Debug.Log("Automatically logging in as a guest");
                GuestSignInModule.I.TryToSignIn();
            }
        }
    }
}
