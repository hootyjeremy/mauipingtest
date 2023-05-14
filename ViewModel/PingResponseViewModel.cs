using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PingTest.ViewModel;

public partial class PingResponseViewModel : ObservableObject
{
    public PingResponseViewModel() 
    {
        Responses = new ObservableCollection<string>();
        ResponseText = "default text";
        PingableIP = "8.8.8.8";

        timer.Elapsed += OnTimedEvent;
        timer.AutoReset = true;        
    }


    [ObservableProperty]
    string pingableIP;

    [ObservableProperty]
    string responseText;

    [ObservableProperty]
    ObservableCollection<string> responses;


    [RelayCommand]
    void ClearData()
    {
        Responses.Clear();
    }


    [RelayCommand]
    void TogglePing()
    {
        ResponseText = "Ping() RelayCommand called";

        // send an initial ping before turning on the loop timer
        if (!timer.Enabled) 
        {
            PingHost();
        }        

        timer.Enabled = !timer.Enabled;
        Debug.WriteLine("timer.Enabled=" + timer.Enabled);
    }


    System.Timers.Timer timer = new System.Timers.Timer(2000);

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        //Debug.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}", e.SignalTime);

        PingHost();
    }

    private void PingHost()
    {
        Ping pingSender = new();
        PingOptions options = new PingOptions();

        options.DontFragment = true;

        string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        byte[] buffer = Encoding.ASCII.GetBytes(data);
        int timeout = 1600;
        //PingReply reply = pingSender.Send(EntryIP.Text, timeout, buffer, options);
        PingReply reply = pingSender.Send(PingableIP, timeout, buffer, options);
        if (reply.Status == IPStatus.Success)
        {
            ResponseText = "Reply from " + reply.Address.ToString() + " bytes=" + reply.Buffer.Length + " time=" + reply.RoundtripTime + "ms" + " TTL=" + reply.Options.Ttl;
            //Responses.Add("Reply from " + reply.Address.ToString() + " bytes=" + reply.Buffer.Length + " time=" + reply.RoundtripTime + "ms" + " TTL=" + reply.Options.Ttl);
            Responses.Add("Reply from " + reply.Address.ToString() + " : " + reply.RoundtripTime + "ms");
            Debug.WriteLine("Reply from " + reply.Address.ToString() + " : " + reply.RoundtripTime + "ms");
            
            // ? how can i scroll to the end of ResponseCollectionView here?
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

            ResponseText = sbResults.ToString();
            Responses.Add("failed ping...");
            Debug.WriteLine("failed ping");
        }
    }



}

