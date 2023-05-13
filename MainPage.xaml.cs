namespace PingTest;

using CommunityToolkit.Mvvm.ComponentModel;
using PingTest.ViewModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;


public partial class MainPage : ContentPage
{
    PingResponseViewModel prvm = new PingResponseViewModel();

    public MainPage(PingResponseViewModel prvm)
	{
		InitializeComponent();
        BindingContext = prvm;

        Debug.WriteLine("---------------------------------");
        Debug.WriteLine("Program started");
    }
}


