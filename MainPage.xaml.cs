namespace PingTest;

using CommunityToolkit.Mvvm.ComponentModel;
using PingTest.ViewModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;

public partial class MainPage : ContentPage
{
    System.Timers.Timer timer = new System.Timers.Timer(2000);

    PingResponseViewModel prvm = new PingResponseViewModel();

    public MainPage()
	{
		InitializeComponent();

        timer.Elapsed += OnTimedEvent;
        timer.AutoReset = true;

        Debug.WriteLine("Started");
    }

    private void PingBtn_Clicked(object sender, EventArgs e)
    {
        prvm.ResponseText = "button clicked";

        //timer.Enabled = !timer.Enabled;

        //ToggleTimer();
    }

    private void ToggleTimer() 
    {
        timer.Enabled = !timer.Enabled;

        Debug.WriteLine("timer.Enabled=" + timer.Enabled);
    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        Debug.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}", e.SignalTime);

        // ? why is this in a different thread than PingBtn_Clicked?

        PingHost();
    }

    private void PingHost()
    {
        Ping pingSender = new Ping();
        PingOptions options = new PingOptions();

        options.DontFragment = true;

        string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        byte[] buffer = Encoding.ASCII.GetBytes(data);
        int timeout = 1600;
        PingReply reply = pingSender.Send(EntryIP.Text, timeout, buffer, options);
        if (reply.Status == IPStatus.Success)
        {
            StringBuilder sbResults = new StringBuilder();

            //sbResults.AppendLine("Address: " + reply.Address.ToString());
            //sbResults.AppendLine("Status: " + reply.Status.ToString());
            //sbResults.AppendLine("RoundTrip time: " + reply.RoundtripTime + "ms");
            //sbResults.AppendLine("Time to live: " + reply.Options.Ttl);
            //sbResults.AppendLine("Don't fragment: " + reply.Options.DontFragment);
            //sbResults.AppendLine("Buffer size: " + reply.Buffer.Length);
            //lblResults.Text = sbResults.ToString();

            prvm.ResponseText = "Reply from " + reply.Address.ToString() + " bytes=" + reply.Buffer.Length + " time=" + reply.RoundtripTime + "ms" + " TTL=" + reply.Options.Ttl;

            // $ does this need data binding instead of accessing a ui thread object from another thread?
            //lblResults.Text = responseText;
        }
        else
        {
            StringBuilder sbResults = new StringBuilder();

            sbResults.AppendLine("Address: " + reply.Address.ToString());
            sbResults.AppendLine("Status: " + reply.Status.ToString());
            //sbResults.AppendLine("RoundTrip time: " + reply.RoundtripTime + "ms");
            //sbResults.AppendLine("Time to live: " + reply.Options.Ttl);
            //sbResults.AppendLine("Don't fragment: " + reply.Options.DontFragment);
            //sbResults.AppendLine("Buffer size: " + reply.Buffer.Length);

            lblResults.Text = sbResults.ToString();
        }
    }
}


//public partial class PingResponseViewModel : ObservableObject
//{
//    //private string _reply;

//    //public string Reply
//    //{
//    //    get => _reply;
//    //    set
//    //    {
//    //        _reply = value;

//    //    }
//    //}

//    //public event PropertyChangedEventHandler PropertyChanged;
//    //public void OnPropertyChanged([CallerMemberName] string name = "") =>
//    //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

//    [ObservableProperty]
//    string responseText;

//    void Ping() 
//    {
//        responseText = string.Empty;
//    }

//}

