namespace WebShopApp.Business.Operations.Setting
{
    // lifetime must be specified
    public interface ISettingService
    {
        Task ToggleMaintenence();

        bool GetMaintenanceState();
    }
}