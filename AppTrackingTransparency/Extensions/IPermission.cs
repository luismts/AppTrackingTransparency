using System.Threading.Tasks;
using Xamarin.Essentials;

namespace XEssentialsTest.Extensions
{
    public interface IPermission
    {
        Task<PermissionStatus> CheckStatusAsync();
        Task<PermissionStatus> RequestAsync();
    }
}
