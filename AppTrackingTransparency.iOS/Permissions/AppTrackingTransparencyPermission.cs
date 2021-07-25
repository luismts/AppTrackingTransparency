using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XEssentialsTest.Extensions;
using static Xamarin.Essentials.Permissions;

namespace AppTrackingTransparency.iOS
{
    public class AppTrackingTransparencyPermission : BasePlatformPermission, IAppTrackingTransparencyPermission
    {
        protected override Func<IEnumerable<string>> RequiredInfoPlistKeys => () =>  new string[] { "NSUserTrackingUsageDescription" };

        // Requests the user to accept or deny a permission
        public void RequestAsync(Action<PermissionStatus> completion)
        {
            ATTrackingManager.RequestTrackingAuthorization((result) => completion(Convert(result)));
        }

        // This method checks if current status of the permission
        public override Task<PermissionStatus> CheckStatusAsync()
        {
            return Task.FromResult(Convert(ATTrackingManager.TrackingAuthorizationStatus));
        }

        private PermissionStatus Convert(ATTrackingManagerAuthorizationStatus status)
        {
            switch (status)
            {
                case ATTrackingManagerAuthorizationStatus.NotDetermined:
                    return PermissionStatus.Disabled;
                case ATTrackingManagerAuthorizationStatus.Restricted:
                    return PermissionStatus.Restricted;
                case ATTrackingManagerAuthorizationStatus.Denied:
                    return PermissionStatus.Denied;
                case ATTrackingManagerAuthorizationStatus.Authorized:
                    return PermissionStatus.Granted;
                default:
                    return PermissionStatus.Unknown;
            }
        }

       
    }
}