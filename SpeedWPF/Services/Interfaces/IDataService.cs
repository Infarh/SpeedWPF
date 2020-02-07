using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedWPF.Services.Interfaces
{
    public interface IDataService
    {
        byte[] Data { get; }
        event EventHandler DataChanged;

        public bool Enabled { get; }
        public void StartDataUpdate(TimeSpan Timeout);
        public void StopDataUpdate();

        long TimeDelta { get; }
    }
}
