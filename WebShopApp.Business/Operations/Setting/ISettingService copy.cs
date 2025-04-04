namespace WebShopApp.Business.Operations.Setting
{
    // lifetime belirtmek gerekiyor
    public interface ISettingService
    {
        Task ToggleMaintenence();

        bool GetMaintenanceState();
    }
}