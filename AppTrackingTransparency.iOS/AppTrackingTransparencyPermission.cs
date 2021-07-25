using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Essentials;
using XEssentialsTest.Extensions;
using static Xamarin.Essentials.Permissions;

namespace AppTrackingTransparency.iOS
{
    public class AppTrackingTransparencyPermission : BasePlatformPermission, IPermission
    {
        protected override Func<IEnumerable<string>> RequiredInfoPlistKeys => () =>  new string[] { "NSUserTrackingUsageDescription" };

        // Requests the user to accept or deny a permission
        public override Task<PermissionStatus> RequestAsync()
        {
            PermissionStatus status = PermissionStatus.Unknown;

            ATTrackingManager.RequestTrackingAuthorization((result) =>
            {
                status = Convert(result);
            });

            return Task.FromResult(status);
        }

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