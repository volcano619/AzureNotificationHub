using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App5
{
    public interface ICrop
    {
        void CropImage();
        void GetDevices();

        Task<string> SendRegistrationToken();
    }
}
