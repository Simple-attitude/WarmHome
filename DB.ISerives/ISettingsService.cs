using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmHome.DTO;

namespace DB.IService
{
    public interface ISettingsService
    {
        string GetValue(string name);
        void SetValue(string name, string value);
        int ? GetIntValue(string name);
        void SetIntValue(string name, string value);
        bool? GetBoolValue(string name);
        void SetBoolValue(string name, string value);
        IEnumerable<SettingsDTO> GetSettings();
    }
}
