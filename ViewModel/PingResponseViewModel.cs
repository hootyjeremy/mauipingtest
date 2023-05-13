using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingTest.ViewModel;

public partial class PingResponseViewModel : ObservableObject
{
    [ObservableProperty]
    string responseText;

    void Ping()
    {
        ResponseText = string.Empty;
    }
}

