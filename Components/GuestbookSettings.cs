using System.Collections;
using DotNetNuke.Collections;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Common.Utilities;

namespace WROX.Modules.Guestbook.Components
{
    public class GuestbookSettings
    {
        private int ModuleId { get; set; }
        private Hashtable AllSettings { get; set; }

        public bool AutoApprove { get; set; }

        public GuestbookSettings(int moduleId)
        {
            ModuleId = moduleId;
            AllSettings = (new ModuleController()).GetModuleSettings(moduleId);
            AutoApprove = AllSettings.GetValueOrDefault("AutoApprove", false);
        }

        public static GuestbookSettings GetGuestbookSettings(int moduleId)
        {
            var cacheKey = "WROX.Modules.Guestbook.Settings" + moduleId;
            var settings = DataCache.GetCachedData<GuestbookSettings>(new CacheItemArgs(cacheKey), args => new GuestbookSettings(moduleId));
            return settings;
        }

        public void SaveSettings()
        {
            var objModules = new ModuleController();
            objModules.UpdateModuleSetting(ModuleId, "AutoApprove", AutoApprove.ToString());
            var cacheKey = "WROX.Modules.Guestbook.Settings" + ModuleId;
            DataCache.SetCache(cacheKey, this);
        }

    }

}