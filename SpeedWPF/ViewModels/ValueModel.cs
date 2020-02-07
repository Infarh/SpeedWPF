using System;
using SpeedWPF.ViewModels.Base;

namespace SpeedWPF.ViewModels
{
    public class ValueModel : ViewModel
    {
        private byte[] _Data;
        private readonly int _ValueByteIndex;
        private const int __ValueLength = 8;

        public double Value => BitConverter.ToDouble(_Data, _ValueByteIndex);

        public ValueModel(byte[] Data, int ValueIndex)
        {
            _Data = Data;
            _ValueByteIndex = ValueIndex * __ValueLength;
        }

        private static readonly string __PropertyNameValue = nameof(Value);
        public void Update(byte[] Data)
        {
            _Data = Data;
            OnPropertyChanged(__PropertyNameValue);
        }
    }
}